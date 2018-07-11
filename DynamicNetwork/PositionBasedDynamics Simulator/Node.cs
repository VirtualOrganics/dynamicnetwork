using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace DynamicNetwork.PositionBasedDynamics_Simulator
{
    class Node
    {
        public Vector2f mVelocity { get; set; }
        public float mVelocityClamp { get; set; }
        public Vector2f mForces { get; set; }
        public Vector2f mOldPosition { get; set; }
        public Vector2f mNewPosition { get; set; }
        public float mVelocityDamping { get; set; }
        public float mInvMass { get; private set; }
        public float mMass
        {
            get => 1f / mInvMass;
            set
            {
                float mass = value;
                if (mass <= 0)
                {
                    mInvMass = 0;
                }
                else
                {
                    mInvMass = 1 / mass;
                }
            }
        }

        public Node(Vector2f position, float mass, float velocityDamping, float velocityClamp)
        {
            mMass = mass;
            mOldPosition = position;
            mNewPosition = position;
            mVelocityDamping = velocityDamping;
            mVelocityClamp = velocityClamp;
            mVelocity = new Vector2f();
            mForces = new Vector2f();
        }

        public void Render(RenderWindow window, Color color)
        {
            CircleShape circle = new CircleShape(4, 10);
            circle.FillColor = color;
            circle.Origin = new Vector2f(2, 2);
            circle.Position = mNewPosition;
            window.Draw(circle);
        }

        public void IntegrateExternalForces(float deltaTime)
        {
            mVelocity += deltaTime * mInvMass * mForces;
            mForces = new Vector2f();
        }

        public void DampVelocity()
        {
            mVelocity *= mVelocityDamping;
        }

        public void IntegrateVelocity(float deltaTime)
        {
            mNewPosition = mOldPosition + mVelocity * deltaTime;
        }

        public void MoveToEstimatesPositions(float deltaTime)
        {
            
            mVelocity = (mNewPosition - mOldPosition) / deltaTime;
            if (MathUtil.Length(mVelocity) > mVelocityClamp)
            {
                mVelocity = MathUtil.Normalize(mVelocity) * mVelocityClamp;
            }

            mOldPosition = mNewPosition;
        }
    }
}
