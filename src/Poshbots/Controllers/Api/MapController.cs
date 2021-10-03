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
    public class MapController : ApiController
    {
        public Battle Get(string id)
        {
            var memCache = MemoryCache.Default;
            if (!memCache.Contains("battle-" + id))
            {
                var players = new List<Player>()
                {
                    new Player() { Bot = new Bot() { Name = "Player" } },
                    new Player() { Bot = new Bot() { Name = "Training" } }
                };

                var newMap = new Battle(players, 30, 30, 100000);
                memCache.Add("battle-" + id, newMap, DateTimeOffset.UtcNow.AddMinutes(5));
            }

            return ((Battle)memCache.Get("battle-" + id));
        }

        public void Delete(string id)
        {
            var memCache = MemoryCache.Default;
            memCache.Remove("battle-" + id);
        }
    }
}