using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DudelkaBot.Bot;
using DudelkaBot.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;

namespace DudelkaBot.Controllers
{
    public class APIController : ControllerBase
    {
        public APIController(IOptions<Config> config) : base(config) { }

        public string GetBotStatus()
        {
            var user = WebUsers.Get(HttpContext);

            if (user == null)
                return BotState.None.ToString();

            var ls = TwitchBot.Get(user.Channel);
            return (ls != null ? ls.State : BotState.Stoped).ToString();
        }

        public void BotRun()
        {
            var user = WebUsers.Get(HttpContext);

            if (user != null)
                TwitchBot.Run(user.Channel);
        }

        public void BotStop()
        {
            TwitchBot.Get(WebUsers.Get(HttpContext)?.Channel)?.Stop();
        }

        [HttpGet("BotBrodcast/{message}")]
        public void BotBrodcast(string message)
        {
            TwitchBot.Get(WebUsers.Get(HttpContext)?.Channel)?.Stop();
        }
    }
}