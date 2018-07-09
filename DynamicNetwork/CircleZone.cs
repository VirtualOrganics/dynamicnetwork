using System;
using System.Collections.Generic;
using System.Linq;
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

        // https://math.stackexchange.com/questions/256100/how-can-i-find-the-points-at-which-two-circles-intersect
        // Somewhere is a bug
        public Intersection GetIntersections(CircleZone other)
        {
            Vector2f intersection0 = new Vector2f(0,0);
            Vector2f intersection1 = new Vector2f(0,0);
            Intersection intersection = null;
            Vector2f direction = other.mPosition - mPosition;
            float distance = MathUtil.Length(direction);

            if (distance < mRadius + other.mRadius)
            {
                Vector2f term0 = (mPosition + other.mPosition) / 2f;
                Vector2f term1 = ((mRadius * mRadius - other.mRadius * other.mRadius)
                                  / (2 * distance * distance)) * direction;

                double sqrtTerm = 0.5 * Math.Sqrt(
                                      2 * ((mRadius * mRadius + other.mRadius * other.mRadius) / distance * distance)
                                      - Math.Pow(mRadius * mRadius - other.mRadius * other.mRadius, 2) /
                                      Math.Pow(distance, 4)
                                      - 1);
                Vector2f term3 = (float) sqrtTerm *
                                 new Vector2f(other.mPosition.Y - mPosition.Y, mPosition.X - other.mPosition.X);

                Vector2f term01 = term0 + term1;

                intersection0 = term01 + term3;
                intersection1 = term01 - term3;

                Console.WriteLine(intersection0);
                Console.WriteLine(intersection1);

                intersection = new Intersection(intersection0, intersection1);

            }
            else
            {
                return null;
            }




            return intersection;
        }
    }
}
