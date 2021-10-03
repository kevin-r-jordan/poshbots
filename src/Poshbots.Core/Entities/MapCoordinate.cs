using System;
using System.Collections.Generic;
using System.Text;

namespace Poshbots.Core.Entities
{
    public class MapCoordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public MapCoordinate(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        public MapCoordinate Random(int maxX, int maxY, Random randomSeed)
        {
            var x = randomSeed.Next(0, maxX - 1);
            var y = randomSeed.Next(0, maxY - 1);
            return new MapCoordinate(x, y);
        }

    }
}
