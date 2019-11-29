using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GroupmeBot.Models;
using GroupmeBot.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace GroupmeBot.Controllers
{
    [Route("api/lunch/test")]
    [ApiController]
    public class LunchBotTestController : ControllerBase
    {
        private readonly GroupmeBotResponseHandler _responseHandler;
        private readonly IConfiguration _config;
        private static LeaderboardHandler _leaderboardHandler;
        private static object _lock = new object();

        public LunchBotTestController(GroupmeBotResponseHandler responseHandler, IConfiguration config)
        {
            _config = config;
            _responseHandler = responseHandler;
            _responseHandler.Init(_config["token"]);
            lock (_lock)
            {
                if(_leaderboardHandler == null)
                    _leaderboardHandler = new LeaderboardHandler(responseHandler, _config["testBotId"],_config["testGroupId"], 0);
            }
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "hi";
        }

        [HttpPost]
        public async Task<JArray> Post(GroupmeMessage response)
        {
            var request = new MessageIndexRequest()
            {
                Limit = 100,
            };
            var indexResponse = await _responseHandler.Index2Messages(_config["tacoGroupId"], request);
            var attachments = new JArray();
            while (indexResponse != null)
            {
                var indexMessage = indexResponse.ToObject<MessageIndexResponse>();
                var messages = ((JArray) indexResponse["messages"]);
                foreach (var message in messages)
                {
                    foreach (var item in ((JArray)message["attachments"]))
                    {
                        attachments.Add(item);
                    }
                }
                request.BeforeId = indexMessage.Messages[indexMessage.Messages.Count - 1].Id;
                indexResponse = await _responseHandler.Index2Messages(_config["tacoGroupId"], request);
            }

            return attachments;
            var lists = await _responseHandler.ListBots();
            /*
            if (response.Text.StartsWith('!'))
            {
                if (response.Text.StartsWith("!groups"))
                {
                    var groups = await _responseHandler.GetGroups(new GroupsRequest()
                        {Page = 1, Omit = "memberships", PerPage = 100});
                    var message = "Groups:\n";
                    foreach (var groupResponse in groups)
                    {
                        var temp = $"{groupResponse.Name}: {groupResponse.Id}\n";
                        if (message.Length + temp.Length > 1000)
                            break;
                        message += temp;
                    }

                    await _responseHandler.SendMessageAsBot(new BotMessageRequest(){Text = message, Id = _config["testBotId"]});
                }
                if (response.Text.StartsWith("!target"))
                {
                    var groupId = response.Text.Split(' ');
                    if (groupId.Length > 1)
                        await _leaderboardHandler.Retarget(groupId[1]);
                    else await _leaderboardHandler.Retarget(_config["testGroupId"]);
                }
                else 
                    await _leaderboardHandler.RunCommand(response);
            }
            */
        }
    }

}
