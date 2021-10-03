using Poshbots.Core.Entities;
using Poshbots.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Poshbots.Models
{
    public class StartBattleViewModel
    {
        [Required]
        public int Player1BotId { get; set; }

        [Required]
        public int Player2BotId { get; set; }


        public List<Bot> PlayerBots { get; set; }
        public IEnumerable<SelectListItem> PlayerBotsList
        {
            get
            {
                return new SelectList(PlayerBots, "BotId", "Name");
            }
        }

        public List<Bot> AllBots { get; set; }
        public IEnumerable<SelectListItem> AllBotsList
        {
            get
            {
                return new SelectList(AllBots, "BotId", "Name");
            }
        }
    }

    public class ViewBattleViewModel
    {
        public Battle Battle { get; set; }
    }
    
}