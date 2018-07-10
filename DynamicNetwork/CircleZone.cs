using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace DynamicNetwork
{
    class CircleZone
    {
        private float mRadius;
        private Vector2f mPosition;
        private RenderWindow mWindow;
        private CircleShape mCircleRegionShape;
        private CircleShape mCircleCenterPositionShape;

        public CircleZone(RenderWindow window,float radius, Vector2f position)
        {
            mWindow = window;
            mPosition = position;
            mRadius = radius;

            //Creating a drawable circle for the region
            mCircleRegionShape = new CircleShape(mRadius);
            mCircleRegionShape.Position = mPosition;
            mCircleRegionShape.OutlineColor = Color.Black;
            mCircleRegionShape.OutlineThickness = 2f;
            mCircleRegionShape.FillColor = new Color(188,188,188,150);
            mCircleRegionShape.Origin = new Vector2f(mRadius,mRadius);

            //Creating a circle which represents the origin of the zone
            mCircleCenterPositionShape = new CircleShape(2);
            mCircleCenterPositionShape.Origin = new Vector2f(2,2);
            mCircleCenterPositionShape.FillColor = Color.Black;
        }

        public void Draw()
        {
            mCircleRegionShape.Position = mPosition;
            mWindow.Draw(mCircleRegionShape);

            mCircleCenterPositionShape.Position = mPosition;
            mWindow.Draw(mCircleCenterPositionShape);
        }

        
        public Intersection GetIntersections(CircleZone other)
        {
            Vector2f direction = other.mPosition - mPosition;
            float distanceAway = MathUtil.Length(direction);

            // Both Circles are to far away from ech other - no intersections
            if (distanceAway > mRadius + other.mRadius)
                return null;
            // No intersection because one circle is contained within the other.
            if (distanceAway < Math.Abs(mRadius - other.mRadius))
                return null;
            // coincident - infinite number of solutions.
            if (Math.Abs(distanceAway) < 0.001f && Math.Abs(mRadius - other.mRadius) < 0.001f)
                return null;


            // Calculation based on
            // https://stackoverflow.com/questions/3349125/circle-circle-intersection-points
            // TODOO: Optimization

            double r0_squ = Math.Pow(mRadius, 2);
            double r1_squ = Math.Pow(other.mRadius, 2);
            double d_squ  = Math.Pow(distanceAway, 2);
            Vector2f p0 = mPosition;

            float a = (float) (r0_squ - r1_squ + d_squ) / (2 * distanceAway);
            float h = (float) Math.Sqrt(r0_squ - Math.Pow(a, 2));

            Vector2f p2 = p0 + a * MathUtil.Normalize(direction);

            float p3_0_x = p2.X + h * (other.mPosition.Y - mPosition.Y) / distanceAway;
            float p3_0_y = p2.Y - h * (other.mPosition.X - mPosition.X) / distanceAway;

            float p3_1_x = p2.X - h * (other.mPosition.Y - mPosition.Y) / distanceAway;
            float p3_1_y = p2.Y + h * (other.mPosition.X - mPosition.X) / distanceAway;

            Vector2f p3_0 = new Vector2f(p3_0_x, p3_0_y);
            Vector2f p3_1 = new Vector2f(p3_1_x, p3_1_y);

            Intersection intersection = new Intersection(p3_0, p3_1);
            intersection.mZoneA = this;
            intersection.mZoneB = other;

            return intersection;
        }

        public bool PointInZone(Vector2f point)
        {
            Vector2f direction = point - mPosition;

            if (MathUtil.SquaredLength(direction) < mRadius * mRadius)
                return true;
            else
                return false;
        }

        public void MoveTo(Vector2f position)
        {
            mPosition = position;
        }

        public void Move(Vector2f add)
        {
            mPosition += add;
        }
    }
}
