using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace DynamicNetwork.PositionBasedDynamics_Simulator
{
    abstract class AConstraint
    {
        protected Node mNodeA;
        protected Node mNodeB;

        protected AConstraint(Node nodeA,Node nodeB)
        {
            mNodeA = nodeA;
            mNodeB = nodeB;
        }

       public abstract void ProjectConstraint();
       public abstract void RenderConstraint(RenderWindow window);

        public Node getNodeA()
        {
            return mNodeA;
        }

        public Node getNodeB()
        {
            return mNodeB;
        }
    }
}
