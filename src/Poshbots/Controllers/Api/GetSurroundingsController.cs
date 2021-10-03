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
    public class GetSurroundingsController : ApiController
    {
        public MapCell[,] Get(string id, string playerName)
        {
            var memCache = MemoryCache.Default;
            var battle = (Battle)memCache.Get("battle-" + id);

            var movingPlayer = battle.Players.First(player => player.Bot.Name == playerName);
            return battle.GetSurroundings(movingPlayer);
        }
    }
}