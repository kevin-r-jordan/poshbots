using Microsoft.AspNet.Identity;
using Poshbots.Core.Entities;
using Poshbots.Core.Services;
using Poshbots.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using System.Security;
using System.Runtime.Caching;

namespace Poshbots.Controllers
{
    public class BattleController : BaseController
    {
        private BotService _botService;
        private StartBattleService _startBattleService;

        public BattleController()
        {
            _botService = Container.Resolve<BotService>();
            _startBattleService = Container.Resolve<StartBattleService>();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var model = new StartBattleViewModel()
            {
                PlayerBots = _botService.GetBotsByUserId(User.Identity.GetUserId()),
                AllBots = _botService.GetAllBots()
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(StartBattleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var players = new List<Player>();
                players.Add(new Player() { Bot = _botService.GetById(model.Player1BotId)});
                players.Add(new Player() { Bot = _botService.GetById(model.Player2BotId) });
                var battle = new Battle(players, 30, 30, 100000);
                var memCache = MemoryCache.Default;
                memCache.Add("battle-" + battle.BattleId, battle, DateTimeOffset.UtcNow.AddMinutes(5));

                _startBattleService.Setup(battle);

                return new RedirectResult("/Battle/Id/" + battle.BattleId);
            }
            return View(model);
        }

        public ActionResult Id(string id)
        {
            if(String.IsNullOrEmpty(id)) return new RedirectResult("/Battle");

            var memCache = MemoryCache.Default;
            var battle = (Battle)memCache.Get("battle-" + id);
            if (battle == null) return new RedirectResult("/Battle");

            var model = new ViewBattleViewModel()
            {
                Battle = battle
            };
            return View(model);
        }
    }
}