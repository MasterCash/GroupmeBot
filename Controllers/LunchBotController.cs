using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GroupmeBot.Models;
using GroupmeBot.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GroupmeBot.Controllers
{
    [Route("api/lunch")]
    [ApiController]
    public class LunchBotController : ControllerBase
    {
        private readonly GroupmeBotResponseHandler _responseHandler;
        private readonly IConfiguration _config;
        private static LeaderboardHandler _leaderboardHandler;
        private static readonly object _lock = new object();

        public LunchBotController(GroupmeBotResponseHandler responseHandler, IConfiguration config)
        {
            _config = config;
            _responseHandler = responseHandler;
            _responseHandler.Init(_config["token"]);
            lock (_lock)
            {
                if (_leaderboardHandler == null)
                    _leaderboardHandler = new LeaderboardHandler(responseHandler, _config["tacoBotId"], _config["tacoGroupId"], 5);
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
