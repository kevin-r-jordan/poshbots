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

namespace Poshbots.Controllers
{
    [Authorize]
    public class BotsController : BaseController
    {
        private BotService _botService;
        public BotsController()
        {
            _botService = Container.Resolve<BotService>();
        }
        
        public ActionResult Index()
        {
            var model = new ListBotsViewModel()
            {
                Bots = _botService.GetBotsByUserId(User.Identity.GetUserId())
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult New()
        {
            var model = new NewBotViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult New(NewBotViewModel model)
        {
            if (ModelState.IsValid)
            {
                var bot = new Bot()
                {
                    UserId = User.Identity.GetUserId(),
                    Name = model.Name,
                    Code = model.Code
                };
                bot = _botService.Create(bot);
                return new RedirectResult("/Bots/Edit/" + bot.BotId);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var bot = _botService.GetById(id);
            if (bot.UserId != User.Identity.GetUserId()) throw new SecurityException("Cannot edit another persons bot.");

            var model = new EditBotViewModel()
            {
                BotId = bot.BotId,
                Name = bot.Name,
                Code = bot.Code
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, EditBotViewModel model)
        {
            var bot = _botService.GetById(id);
            if (bot.UserId != User.Identity.GetUserId()) throw new SecurityException("Cannot edit another persons bot.");

            if (ModelState.IsValid)
            {
                bot.Name = model.Name;
                bot.Code = model.Code;
                bot = _botService.Update(bot);
                return new RedirectResult("/Bots/Edit/" + bot.BotId);
            }

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var bot = _botService.GetById(id);
            if (bot.UserId != User.Identity.GetUserId()) throw new SecurityException("Cannot edit another persons bot.");

            _botService.Delete(id);
            return new RedirectResult("/Bots");
        }
    }
}