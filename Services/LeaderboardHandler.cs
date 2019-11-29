using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupmeBot.Models;

namespace GroupmeBot.Services
{
    public class LeaderboardHandler
    {
        private readonly GroupmeBotResponseHandler _handler;
        private readonly object _leaderboardLock = new object();
        private Dictionary<string, (uint Has, uint Given, uint Self, uint NumMessages)> _leaderboard;
        private readonly object _spamLock = new object();
        private Dictionary<string, DateTime> _spamFilter;
        private Dictionary<string, string> _ids;
        private DateTime _lastUpdated;
        private readonly string _botId;
        private string _groupId;
        private readonly float _spamDelay;
        private bool UpdateWarn = false;
        private readonly object warnLock = new object();

        public LeaderboardHandler(GroupmeBotResponseHandler handler, string botId, string groupId, float spamDelay)
        {
            _handler = handler;
            _leaderboard = new Dictionary<string, (uint Has, uint Given, uint Self, uint NumMessages)>();
            _spamFilter = new Dictionary<string, DateTime>();
            _ids = new Dictionary<string, string>();
            _lastUpdated = DateTime.MinValue;
            _botId = botId;
            _groupId = groupId;
            _spamDelay = spamDelay;
            Update().Wait();
        }

        private bool SpamCheck(string userId)
        {
            lock (_spamLock)
            {
                if (_spamFilter.ContainsKey(userId))
                {
                    if ((DateTime.Now - _spamFilter[userId]).TotalMinutes < _spamDelay)
                    {
                        _spamFilter[userId] = DateTime.Now;
                        return true;
                    }
                }
                _spamFilter[userId] = DateTime.Now;
                return false;
            }
        }

        public async Task Retarget(string groupId)
        {
            await SendMessage("Retargeting...");
            _groupId = groupId;
            await Update(true);
            await SendMessage("Finished");
        }

        public async Task<bool> RunCommand(GroupmeMessage response)
        {
            await Update();
            if (response.Text.StartsWith("!"))
            {
                if (SpamCheck(response.UserId))
                    return false;
                var message = "";
                var cmd = response.Text.Substring(1);
                if (cmd.ToLower() == "totallikes")
                {
                    var list = GetTopHasLikes();
                    message = GenerateLeaderboard("Top Likes", list);
                }
                else if (cmd.ToLower() == "givenlikes")
                {
                    var list = GetTopGivenLikes();
                    message = GenerateLeaderboard("Top Given Likes", list);
                }
                else if (cmd.ToLower() == "ratiolikes")
                {
                    var list = GetTopRatioLikes();
                    message = GenerateLeaderboard("Top Has/Given ratio Likes", list);
                }
                else if (cmd.ToLower() == "selflikes")
                {
                    var list = GetTopSelfLikes();
                    message = GenerateLeaderboard("Top Self Likes", list);
                }
                else if (cmd.ToLower() == "perlikes")
                {
                    var list = GetTopLikesPerMessage();
                    message = GenerateLeaderboard("Top Likes per Message", list);
                }
                else if (cmd.ToLower() == "me")
                {
                    message = GetMeStats(response.UserId);
                }
                else if (cmd.ToLower() == "update")
                {
                    if (response.Name.EndsWith("Cash"))
                        await Update();
                    else
                        message =
                            $"Minutes till Update: {new TimeSpan(0, 1, 0, 0).Subtract((DateTime.Now - _lastUpdated)).TotalMinutes.ToString("F2")}";

                }
                else if (cmd.ToLower() == "updatewarn")
                {
                    lock (warnLock)
                    {
                        UpdateWarn = !UpdateWarn;
                    }

                    message = $"Update toggled {(UpdateWarn ? "On" : "Off")}";
                }
                else if (cmd.ToLower() == "help")
                {
                    message = "Cmds:\n" +
                              "!totallikes: leaderboard of who has most likes\n" +
                              "!givenlikes: leaderboard of who has given most likes\n" +
                              "!ratiolikes: leaderboard of who has best ratio of has/given likes\n" +
                              "!selflikes: leaderboard of who has the most self likes\n" +
                              "!perlikes: leaderboard of who has the best likes per message\n" +
                              "!me: individual stats\n" +
                              "!update: time in minutes till leaderboard update\n" +
                              "!updatewarn: toggle on or off updating messages\n" +
                              "!help: list commands\n";
                }
                if (message == "") return false;
                await SendMessage(message);
                return true;
            }

            return false;
        }

        private string GenerateLeaderboard(string header, List<(string Name, float Value)> list)
        {
            var message = header + "\n\n";
            var count = 1;
            foreach (var item in list)
            {
                if (item.Name.Contains("UnknownUser"))
                    continue;
                var nextLine = $"{count++}: {item.Name} - {item.Value:f2}\n";
                if (message.Length + nextLine.Length > 1000)
                    break;
                message += nextLine;
            }

            return message;
        }

        private string GenerateLeaderboard(string header, List<(string Name, uint Value)> list)
        {
            var message = header + "\n\n";
            var count = 1;
            foreach (var item in list)
            {
                if (item.Name.Contains("UnknownUser"))
                    continue;
                var nextLine = $"{count++}: {item.Name} - {item.Value}\n";
                if (message.Length + nextLine.Length > 1000)
                    break;
                message += nextLine;
            }

            return message;
        }

        private async Task SendMessage(string message)
        {
            await Task.Delay(2500);
            var botMessage = new BotMessageRequest()
            {
                Text = message,
                Id = _botId,
            };
            await _handler.SendMessageAsBot(botMessage);
        }

        private async Task Update(bool forceUpdate = false)
        {
            var shouldUpdate = false;
            lock (_leaderboardLock)
            {
                shouldUpdate = (DateTime.Now - _lastUpdated).TotalHours > 1;
            }

            if (shouldUpdate || forceUpdate)
            {
                if(UpdateWarn)
                    await SendMessage("Updating Database Info please wait...");
                lock (_leaderboardLock)
                {
                    _lastUpdated = DateTime.Now;
                    var task = GetLikes();
                    task.Wait();
                    _leaderboard = task.Result;
                    var task2 = GetNickNames();
                    task2.Wait();
                    _ids = task2.Result;
                }
                if(UpdateWarn)
                    await SendMessage("Finished");
            }


        }

        private string GetMeStats(string id)
        {
            lock (_leaderboardLock)
            {
                if (_leaderboard.ContainsKey(id))
                {
                    var stat = _leaderboard[id];
                    var name = _ids.ContainsKey(id) ? _ids[id] : "UnknownUser:" + id;
                    return
                        $"{name} - has: {stat.Has}, given: {stat.Given}, self-liked: {stat.Self}, Ratio: {(float)stat.Has / MathF.Max(stat.Given, 1):f3}, AvgPerMessage: {(float)stat.Has / MathF.Max(stat.NumMessages, 1):f3}";
                }
            }
            return "Invalid user, did you join recently? Try again in a bit.";
        }

        private List<(string Name, uint Value)> GetTopHasLikes()
        {
            lock (_leaderboardLock)
            {
                var items = (from pair in _leaderboard
                             orderby pair.Value.Has descending
                             select (_ids.ContainsKey(pair.Key) ? _ids[pair.Key] : "UnknownUser:" + pair.Key, pair.Value.Has));

                return items.ToList();
            }
        }
        private List<(string Name, uint Value)> GetTopGivenLikes()
        {
            lock (_leaderboardLock)
            {
                var items = (from pair in _leaderboard
                             orderby pair.Value.Given descending
                             select (_ids.ContainsKey(pair.Key) ? _ids[pair.Key] : "UnknownUser:" + pair.Key, pair.Value.Given));

                return items.ToList();
            }
        }

        private List<(string Name, uint Value)> GetTopSelfLikes()
        {
            lock (_leaderboardLock)
            {
                var items = (from pair in _leaderboard
                             orderby pair.Value.Self descending
                             select (_ids.ContainsKey(pair.Key) ? _ids[pair.Key] : "UnknownUser:" + pair.Key, pair.Value.Self));

                return items.ToList();
            }
        }
        private List<(string Name, float Value)> GetTopRatioLikes()
        {
            lock (_leaderboardLock)
            {
                var items = (from pair in _leaderboard
                             orderby (pair.Value.Has / MathF.Max(1, pair.Value.Given)) descending
                             select (_ids.ContainsKey(pair.Key) ? _ids[pair.Key] : "UnknownUser:" + pair.Key,
                                 pair.Value.Has / MathF.Max(1, pair.Value.Given)));

                return items.ToList();
            }
        }
        private List<(string Name, float Value)> GetTopLikesPerMessage()
        {
            lock (_leaderboardLock)
            {
                var items = (from pair in _leaderboard
                             orderby pair.Value.Has / MathF.Max(1, pair.Value.NumMessages) descending
                             select (_ids.ContainsKey(pair.Key) ? _ids[pair.Key] : "UnknownUser:" + pair.Key,
                                 pair.Value.Has / MathF.Max(1, pair.Value.NumMessages)));

                return items.ToList();
            }
        }

        private async Task<Dictionary<string, string>> GetNickNames()
        {
            var dict = new Dictionary<string, string>();
            var groupInfo = await _handler.GetGroup(_groupId);

            foreach (var member in groupInfo.Members)
            {
                dict[member.UserId] = member.Nickname;
            }

            return dict;
        }

        private async Task<Dictionary<string, (uint Has, uint Given, uint Self, uint NumMessages)>> GetLikes()
        {
            var dict = new Dictionary<string, (uint Has, uint Given, uint Self, uint NumMessages)>();
            var request = new MessageIndexRequest()
            {
                Limit = 100,
            };
            var indexResponse = await _handler.IndexMessages(_groupId, request);
            while (indexResponse.Messages.Count > 0)
            {
                foreach (var message in indexResponse.Messages)
                {
                    if (!dict.ContainsKey(message.UserId))
                    {
                        dict.Add(message.UserId, (0, 0, 0, 1));
                    }
                    else
                    {
                        var score = dict[message.UserId];
                        score.NumMessages++;
                        dict[message.UserId] = score;
                    }
                    foreach (var person in message.FavoritedBy)
                    {

                        var score = dict[message.UserId];
                        score.Has++;
                        dict[message.UserId] = score;

                        if (!dict.ContainsKey(person))
                        {
                            dict.Add(person, (0, 1, 0, 0));
                        }
                        else
                        {
                            score = dict[person];
                            score.Given++;
                            dict[person] = score;
                        }

                        if (message.UserId == person)
                        {
                            score = dict[person];
                            score.Self++;
                            dict[person] = score;
                        }

                    }
                }

                request.BeforeId = indexResponse.Messages[indexResponse.Messages.Count - 1].Id;
                indexResponse = await _handler.IndexMessages(_groupId, request);
            }

            return dict;
        }

    }
}
