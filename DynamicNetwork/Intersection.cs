using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace DynamicNetwork
{
    class Intersection
    {
        public Vector2f PointA { get; set; }
        public Vector2f PointB { get; set; }

        public Intersection(Vector2f pA, Vector2f pB)
        {
            PointA = pA;
            PointB = pB;
        }
    }
}
