using System.Threading.Tasks;
using GroupmeAPIHandler.Models;
using GroupmeAPIHandler.Services;
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
        private static readonly object Lock = new object();

        public LunchBotTestController(GroupmeBotResponseHandler responseHandler, IConfiguration config)
        {
            _config = config;
            _responseHandler = responseHandler;
            _responseHandler.Init(_config["token"]);
            lock (Lock)
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
        public async Task Post(GroupmeMessage response)
        {
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
            else
            {
                await _responseHandler.ListBots();
            }
        }
    }

}
