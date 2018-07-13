using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DynamicNetwork.PositionBasedDynamics_Simulator;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace DynamicNetwork
{
    class TestScene
    {
        private const int SOLVER_ITERATIONS = 10;
        private const float ZONE_INITIAL_RADIUS = 30;
        private const float ZONE_OFFSET = 20;
        private const float SPRING_STIFFNESS = 0.05f;

        private RenderWindow mWindow;
        private Simulator mSimulator;
        private List<CircleZone> mCircleZones;
        private List<AConstraint> mSpringConstraints;
        private List<Node> mNodes;
        private List<TriangleMidpoint> mTriangleMidPoints;
        private List<Vector2f> mCentroids;
        private Vector2f mousePos;
        private CircleZone mCurrentSelected;
        private CircleShape mCentroidMarker;

        private LinkedList<CurrentConnection> mNetwork;


        public TestScene(RenderWindow window)
        {
            mWindow = window;
            mSimulator = new Simulator(mWindow,SOLVER_ITERATIONS);
            mCircleZones = new List<CircleZone>(100);
            mSpringConstraints = new List<AConstraint>(300);
            mTriangleMidPoints = new List<TriangleMidpoint>(24);
            mNodes = new List<Node>(100);
            mCentroids = new List<Vector2f>(100);
            mNetwork = new LinkedList<CurrentConnection>();
            mCentroidMarker = new CircleShape(2);
            mCentroidMarker.FillColor = Color.Black;
            mCentroidMarker.Origin = new Vector2f(1,1);
            //

            #region initZones

            mCircleZones.Add(new CircleZone(mWindow, ZONE_INITIAL_RADIUS, new Vector2f(400, 200)));
            mCircleZones.Add(new CircleZone(mWindow, ZONE_INITIAL_RADIUS, new Vector2f(450, 200)));
            mCircleZones.Add(new CircleZone(mWindow, ZONE_INITIAL_RADIUS, new Vector2f(500, 200)));

            mCircleZones.Add(new CircleZone(mWindow, ZONE_INITIAL_RADIUS, new Vector2f(375, 240)));
            mCircleZones.Add(new CircleZone(mWindow, ZONE_INITIAL_RADIUS, new Vector2f(425, 240)));
            mCircleZones.Add(new CircleZone(mWindow, ZONE_INITIAL_RADIUS, new Vector2f(475, 240)));
            mCircleZones.Add(new CircleZone(mWindow, ZONE_INITIAL_RADIUS, new Vector2f(525, 240)));

            mCircleZones.Add(new CircleZone(mWindow, ZONE_INITIAL_RADIUS, new Vector2f(350, 280)));
            mCircleZones.Add(new CircleZone(mWindow, ZONE_INITIAL_RADIUS, new Vector2f(400, 280)));
            mCircleZones.Add(new CircleZone(mWindow, ZONE_INITIAL_RADIUS, new Vector2f(450, 280)));
            mCircleZones.Add(new CircleZone(mWindow, ZONE_INITIAL_RADIUS, new Vector2f(500, 280)));
            mCircleZones.Add(new CircleZone(mWindow, ZONE_INITIAL_RADIUS, new Vector2f(550, 280)));

            mCircleZones.Add(new CircleZone(mWindow, ZONE_INITIAL_RADIUS, new Vector2f(375, 320)));
            mCircleZones.Add(new CircleZone(mWindow, ZONE_INITIAL_RADIUS, new Vector2f(425, 320)));
            mCircleZones.Add(new CircleZone(mWindow, ZONE_INITIAL_RADIUS, new Vector2f(475, 320)));
            mCircleZones.Add(new CircleZone(mWindow, ZONE_INITIAL_RADIUS, new Vector2f(525, 320)));

            mCircleZones.Add(new CircleZone(mWindow, ZONE_INITIAL_RADIUS, new Vector2f(400, 360)));
            mCircleZones.Add(new CircleZone(mWindow, ZONE_INITIAL_RADIUS, new Vector2f(450, 360)));
            mCircleZones.Add(new CircleZone(mWindow, ZONE_INITIAL_RADIUS, new Vector2f(500, 360)));

            for (int i=0;i<mCircleZones.Count;i++)
            {
                mCircleZones[i].SetNodeIndex(i);
                mSimulator.AddNode(i,mCircleZones[i].GetPosition(),1);
                mNodes.Add(mSimulator.GetNode(i));
            }

            mSpringConstraints.Add(new SpringConstraint(mNodes[0],mNodes[1]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[1], mNodes[2]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[0], mNodes[3]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[0], mNodes[4]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[1], mNodes[4]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[5], mNodes[1]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[5], mNodes[2]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[6], mNodes[2]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[3], mNodes[4]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[4], mNodes[5]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[6], mNodes[5]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[7], mNodes[3]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[3], mNodes[8]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[7], mNodes[8]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[8], mNodes[4]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[9], mNodes[4]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[9], mNodes[5]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[8], mNodes[9]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[10], mNodes[5]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[9], mNodes[10]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[6], mNodes[10]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[6], mNodes[11]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[10], mNodes[11]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[7], mNodes[12]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[8], mNodes[12]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[8], mNodes[13]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[12], mNodes[13]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[9], mNodes[13]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[9], mNodes[14]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[13], mNodes[14]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[14], mNodes[10]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[15], mNodes[10]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[14], mNodes[15]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[15], mNodes[11]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[16], mNodes[12]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[16], mNodes[13]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[16], mNodes[17]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[14], mNodes[17]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[17], mNodes[18]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[15], mNodes[18]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[14], mNodes[18]));
            mSpringConstraints.Add(new SpringConstraint(mNodes[13], mNodes[17]));

            for (int i=0;i<mSpringConstraints.Count;i++)
            {
                SpringConstraint spring = (SpringConstraint) mSpringConstraints[i];
                spring.mStiffness = (float)(1d - Math.Pow(1f - SPRING_STIFFNESS, 1f / 10f));

            }
            mSimulator.AddConstraints(mSpringConstraints);
            
            mTriangleMidPoints.Add(new TriangleMidpoint(0, 4, 3));
            mTriangleMidPoints.Add(new TriangleMidpoint(0, 1, 4));
            mTriangleMidPoints.Add(new TriangleMidpoint(1, 4, 5));
            mTriangleMidPoints.Add(new TriangleMidpoint(1, 5, 2));
            mTriangleMidPoints.Add(new TriangleMidpoint(2, 5, 6));

            mTriangleMidPoints.Add(new TriangleMidpoint(3, 7, 8));
            mTriangleMidPoints.Add(new TriangleMidpoint(3, 4, 8));
            mTriangleMidPoints.Add(new TriangleMidpoint(4, 8, 9));
            mTriangleMidPoints.Add(new TriangleMidpoint(4, 5, 9));
            mTriangleMidPoints.Add(new TriangleMidpoint(5, 9, 10));
            mTriangleMidPoints.Add(new TriangleMidpoint(5, 6, 10));
            mTriangleMidPoints.Add(new TriangleMidpoint(6, 10, 11));

            mTriangleMidPoints.Add(new TriangleMidpoint(7, 8, 12));
            mTriangleMidPoints.Add(new TriangleMidpoint(12,8, 13));
            mTriangleMidPoints.Add(new TriangleMidpoint(8, 13, 9));
            mTriangleMidPoints.Add(new TriangleMidpoint(13, 9, 14));
            mTriangleMidPoints.Add(new TriangleMidpoint(9, 10, 14));
            mTriangleMidPoints.Add(new TriangleMidpoint(14, 10, 15));
            mTriangleMidPoints.Add(new TriangleMidpoint(10, 11, 15));

            mTriangleMidPoints.Add(new TriangleMidpoint(12, 13, 16));
            mTriangleMidPoints.Add(new TriangleMidpoint(16, 13, 17));
            mTriangleMidPoints.Add(new TriangleMidpoint(13, 17, 14));
            mTriangleMidPoints.Add(new TriangleMidpoint(17, 14, 18));
            mTriangleMidPoints.Add(new TriangleMidpoint(14, 15, 18));

            mNetwork.ad


            #endregion
        }

        public void Update(float deltaTime)
        {
            Input();
            mSimulator.Update(deltaTime);


            for (int i=0;i<mCircleZones.Count;i++)
            {
                mCircleZones[i].MoveTo(mSimulator.GetPositionOfNode(i));
            }

            mCentroids.Clear();

            for (int i=0;i<mTriangleMidPoints.Count;i++)
            {
                mTriangleMidPoints[i].CalculateMidPoint(mCircleZones);
                mCentroids.Add(mTriangleMidPoints[i].GetMidPoint());
            }





        }

        public void Draw()
        {
            mSimulator.Draw();

            for (int i=0;i< mCircleZones.Count;i++)
            {
               // mCircleZones[i].Draw();
            }

            if(mCurrentSelected != null)
            MathUtil.Line(mWindow,mCurrentSelected.GetPosition(),mousePos,Color.Blue);

            for (int i=0;i<mCentroids.Count;i++)
            {
                mCentroidMarker.Position = mCentroids[i];
                mWindow.Draw(mCentroidMarker);
            }
        }
        
        private void Input()
        {
            mousePos = (Vector2f)Mouse.GetPosition(mWindow);

            if (Mouse.IsButtonPressed(0) && mCurrentSelected == null)
            {
                foreach (CircleZone zone in mCircleZones)
                {
                    if (zone.PointInZone(mousePos))
                    {
                        mCurrentSelected = zone;
                    }
                }
            }
            else if (Mouse.IsButtonPressed(0) && mCurrentSelected != null)
            {
                // mCurrentSelected.MoveTo(mousePos);
                Node selectedNode = mSimulator.GetNode(mCurrentSelected.GetNodeIdx());
                Vector2f direction = mousePos - selectedNode.mNewPosition;

                mSimulator.GetNode(mCurrentSelected.GetNodeIdx()).mForces = direction;

            }
            else
            {
                mCurrentSelected = null;
            }
        }
        
    }
}
