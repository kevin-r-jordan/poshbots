using Poshbots.Core.Entities;
using Poshbots.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Poshbots.Controllers.Api
{
    public class GetScoreController : ApiController
    {
        public List<Player> Get(string id)
        {
            var memCache = MemoryCache.Default;
            var battle = (Battle)memCache.Get("battle-" + id);
            var tempPlayers = new List<Player>();
            foreach (var player in battle.Players)
            {
                tempPlayers.Add(new Player()
                {
                    Bot = new Bot() {  Name = player.Bot.Name },
                    Score = player.Score
                });
            }
            return tempPlayers;
        }
    }
}