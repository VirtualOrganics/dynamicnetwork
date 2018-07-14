using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace DynamicNetwork
{
    class TriangleMidpoint
    {
        private CircleZone mCircleZone0;
        private CircleZone mCircleZone1;
        private CircleZone mCircleZone2;
        private Vector2f mMidPoint;

        public TriangleMidpoint(CircleZone circleZone0, CircleZone circleZone1, CircleZone circleZone2)
        {
            mCircleZone0 = circleZone0;
            mCircleZone1 = circleZone1;
            mCircleZone2 = circleZone2;
        }

        public void CalculateMidPoint()
        {
            Vector2f position0 = mCircleZone0.GetPosition();
            Vector2f position1 = mCircleZone1.GetPosition();
            Vector2f position2 = mCircleZone2.GetPosition();

            mMidPoint = (position0 + position1 + position2) / 3f;
        }

        public Vector2f GetMidPoint()
        {
            return mMidPoint;
        }
    }
}
