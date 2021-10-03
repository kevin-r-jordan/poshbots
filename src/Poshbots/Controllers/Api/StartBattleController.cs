using Poshbots.Core.Entities;
using Poshbots.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Caching;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace Poshbots.Controllers.Api
{
    public class StartBattleController : BaseApiController
    {
        public HttpResponseMessage Get(string id)
        {
            var memCache = MemoryCache.Default;
            var battle = (Battle)memCache.Get("battle-" + id);

            if (!battle.Started)
            {
                battle.Started = true;
                Container.Resolve<StartBattleService>().Begin(battle);

                memCache.Set("battle-" + id, battle, DateTime.UtcNow.AddMinutes(5));
            }
            return Request.CreateResponse(System.Net.HttpStatusCode.OK);
        }
    }
}