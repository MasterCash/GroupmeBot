using System.Threading.Tasks;
using GroupmeAPIHandler.Models;
using GroupmeAPIHandler.Services;
using GroupmeBot.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GroupmeBot.Controllers
{
    [Route("api/lunch")]
    [ApiController]
    public class LunchBotController : ControllerBase
    {
        private static LeaderboardHandler _leaderboardHandler;
        private static readonly object Lock = new object();

        public LunchBotController(GroupmeBotResponseHandler responseHandler, IConfiguration config)
        {
            responseHandler.Init(config["token"]);
            lock (Lock)
            {
                if (_leaderboardHandler == null)
                    _leaderboardHandler = new LeaderboardHandler(responseHandler, config["tacoBotId"], config["tacoGroupId"], 5);
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
            if(response.Text.StartsWith('!'))
                await _leaderboardHandler.RunCommand(response);
        }
    }

}
