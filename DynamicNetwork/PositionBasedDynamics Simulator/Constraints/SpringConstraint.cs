using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace DynamicNetwork.PositionBasedDynamics_Simulator
{
    class SpringConstraint : AConstraint
    {
        public float mRestLength { get; set; }
        public float mStiffness { get; set; }
        

        public SpringConstraint(Node nodeA, Node nodeB, float restLength) : base(nodeA, nodeB)
        {
            mRestLength = restLength;
            mStiffness = 1f;
        }

        public SpringConstraint(Node nodeA, Node nodeB) : base(nodeA, nodeB)
        {
            mRestLength = MathUtil.Length(nodeA.mNewPosition - mNodeB.mNewPosition);
            mStiffness = 1;
        }

        public override void ProjectConstraint()
        {
            float p1_invMass = mNodeA.mInvMass;
            float p2_invMass = mNodeB.mInvMass;

            float weight_p1 = p1_invMass / (p1_invMass + p2_invMass);
            float weight_p2 = p2_invMass / (p1_invMass + p2_invMass);

            Vector2f direction = mNodeA.mNewPosition - mNodeB.mNewPosition;
            float currentLength = MathUtil.Length(direction);
            Vector2f nDirection = MathUtil.Normalize(direction);

            Vector2f deltaP1 = -weight_p1 * (currentLength - mRestLength) * nDirection;
            Vector2f deltaP2 =  weight_p2 * (currentLength - mRestLength) * nDirection;

            mNodeA.mNewPosition += deltaP1 * mStiffness;
            mNodeB.mNewPosition += deltaP2 * mStiffness;
        }

        public override void RenderConstraint(RenderWindow window)
        {
            MathUtil.Line(window, mNodeA.mNewPosition, mNodeB.mNewPosition, Color.Black);
        }

        public Node GetNodeA()
        {
            return mNodeA;
        }

        public Node GetNodeB()
        {
            return mNodeB;
        }
    }
}
