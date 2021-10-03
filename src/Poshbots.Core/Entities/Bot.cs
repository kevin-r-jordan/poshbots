using System;
using System.Collections.Generic;
using System.Text;

namespace Poshbots.Core.Entities
{
    public class Bot
    {
        public int BotId { get; set; }
        public string UserId { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }

        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
    }
}
