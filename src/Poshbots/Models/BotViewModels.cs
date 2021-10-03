using Poshbots.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Poshbots.Models
{
    public class ListBotsViewModel
    {
        public List<Bot> Bots { get; set; }
    }

    public class NewBotViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class EditBotViewModel
    {
        public int BotId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Code { get; set; }
    }

}