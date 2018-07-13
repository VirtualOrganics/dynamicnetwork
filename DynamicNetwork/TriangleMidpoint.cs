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
        private int mCircleZoneIdx0;
        private int mCircleZoneIdx1;
        private int mCircleZoneIdx2;
        private Vector2f mMidPoint;

        public TriangleMidpoint(int circleZoneIdx0, int circleZoneIdx1, int circleZoneIdx2)
        {
            mCircleZoneIdx0 = circleZoneIdx0;
            mCircleZoneIdx1 = circleZoneIdx1;
            mCircleZoneIdx2 = circleZoneIdx2;
        }

        public void CalculateMidPoint(List<CircleZone> circleZones)
        {
            Vector2f position0 = circleZones[mCircleZoneIdx0].GetPosition();
            Vector2f position1 = circleZones[mCircleZoneIdx1].GetPosition();
            Vector2f position2 = circleZones[mCircleZoneIdx2].GetPosition();

            mMidPoint = (position0 + position1 + position2) / 3f;
        }

        public Vector2f GetMidPoint()
        {
            return mMidPoint;
        }
    }
}
