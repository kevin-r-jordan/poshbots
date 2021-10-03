using System;
using System.Collections.Generic;
using System.Text;

namespace Poshbots.Core.Entities
{
    public class MapCell
    {
        public int Points { get; set; }
        public string Owner { get; set; }
        public string Occupied { get; set; }
    }
}
