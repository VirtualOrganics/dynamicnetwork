using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace DynamicNetwork.PositionBasedDynamics_Simulator
{
    class Simulator
    {
        private RenderWindow mWindow;
        private Dictionary<int, Node> mNodes;
        private List<AConstraint> mConstraints;
        private int mCorrectionIterations;

        public Simulator(RenderWindow window,int correctionIterations)
        {
            mWindow = window;
            mCorrectionIterations = correctionIterations;
            mConstraints = new List<AConstraint>(100);
            mNodes = new Dictionary<int, Node>(100);
        }

        public void Update(float deltaTime)
        {
            foreach (var node in mNodes)
            {
                node.Value.IntegrateExternalForces(deltaTime);
                node.Value.DampVelocity();
                node.Value.IntegrateVelocity(deltaTime);
            }

            for (int i = 0; i < mCorrectionIterations; i++)
            {
                for (int j = 0; j < mConstraints.Count;j++)
                {
                    mConstraints[j].ProjectConstraint();
                }                   
            }

            foreach (var node in mNodes)
            {
                node.Value.MoveToEstimatesPositions(deltaTime);
            }
        }

        public void Draw()
        {
            for (int i = 0; i < mConstraints.Count; i++)
                mConstraints[i].RenderConstraint(mWindow);

            foreach (var node in mNodes)
                node.Value.Render(mWindow,Color.Green);               
        }

        public void AddConstraint(AConstraint constraint)
        {
            mConstraints.Add(constraint);
        }

        public void AddConstraints(List<AConstraint> constraints)
        {
            mConstraints.AddRange(constraints);
        }

        public void AddNode(int nodeIdx, Vector2f position, float mass, float damping = 1f, float velocityClamp = 200f)
        {
            if (mNodes.ContainsKey(nodeIdx))
                throw new ArgumentException("Node with index " + nodeIdx + " already exists!");

            mNodes.Add(nodeIdx,new Node(position,mass,damping,velocityClamp));
        }

        public Node GetNode(int nodeIdx)
        {
            if (!mNodes.ContainsKey(nodeIdx))
                throw new ArgumentException("Node with index " + nodeIdx + " does not exist!");

            return mNodes[nodeIdx];
        }

        public Vector2f GetPositionOfNode(int nodeIdx)
        {
            return GetNode(nodeIdx).mNewPosition;
        }
    }
}
