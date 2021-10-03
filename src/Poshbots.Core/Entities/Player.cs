using System;
using System.Collections.Generic;
using System.Text;

namespace Poshbots.Core.Entities
{
    public class Player
    {
        public Bot Bot { get; set; }
        public int Score { get; set; }
        public MapCoordinate Position { get; set; }
        public int ActionCount { get; set; }
    }
}
