using Poshbots.Core.Services;
using System;
using System.Linq;
using System.Net.Http;
using System.Runtime.Caching;
using System.Web.Http;

namespace Poshbots.Controllers.Api
{
    public class MoveRightController : ApiController
    {
        public HttpResponseMessage Get(string id, string playerName)
        {
            var memCache = MemoryCache.Default;
            var battle = (Battle)memCache.Get("battle-" + id);

            var movingPlayer = battle.Players.First(player => player.Bot.Name == playerName);
            battle.MoveRight(movingPlayer);

            memCache.Set("battle-" + id, battle, DateTime.UtcNow.AddMinutes(5));
            return Request.CreateResponse(System.Net.HttpStatusCode.OK);
        }
    }
}