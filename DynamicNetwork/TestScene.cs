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
        private const float SPRING_STIFFNESS = 1f;

        private RenderWindow mWindow;
        private Simulator mSimulator;
        private List<CircleZone> mCircleZones;
        private List<AConstraint> mSpringConstraints;
        private List<Node> mNodes;
        private List<TriangleMidpoint> mTriangleMidPoints;
        private List<Vector2f> mCentroids;
        private List<CurrentConnection> mCurrentConnections;

        private Vector2f mousePos;
        private CircleZone mCurrentSelected;
        private CircleShape mCentroidMarker;

        private LinkedList<CurrentConnection> mNetwork;

        //Temp
        private Random rnd;

        public TestScene(RenderWindow window)
        {
            mWindow = window;
            mSimulator = new Simulator(mWindow,SOLVER_ITERATIONS);
            mCircleZones = new List<CircleZone>(100);
            mSpringConstraints = new List<AConstraint>(300);
            mTriangleMidPoints = new List<TriangleMidpoint>(24);
            mNodes = new List<Node>(100);
            mCentroids = new List<Vector2f>(100);
            mCurrentConnections = new List<CurrentConnection>(200);
            mNetwork = new LinkedList<CurrentConnection>();
            mCentroidMarker = new CircleShape(2);
            mCentroidMarker.FillColor = Color.Black;
            mCentroidMarker.Origin = new Vector2f(1,1);
            //Temp
            rnd = new Random();

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
            
            mTriangleMidPoints.Add(new TriangleMidpoint(mCircleZones[0], mCircleZones[4], mCircleZones[3]));
            mTriangleMidPoints.Add(new TriangleMidpoint(mCircleZones[0], mCircleZones[1], mCircleZones[4]));
            mTriangleMidPoints.Add(new TriangleMidpoint(mCircleZones[1], mCircleZones[4], mCircleZones[5]));
            mTriangleMidPoints.Add(new TriangleMidpoint(mCircleZones[1], mCircleZones[5], mCircleZones[2]));
            mTriangleMidPoints.Add(new TriangleMidpoint(mCircleZones[2], mCircleZones[5], mCircleZones[6]));

            mTriangleMidPoints.Add(new TriangleMidpoint(mCircleZones[3], mCircleZones[7], mCircleZones[8]));
            mTriangleMidPoints.Add(new TriangleMidpoint(mCircleZones[3], mCircleZones[4], mCircleZones[8]));
            mTriangleMidPoints.Add(new TriangleMidpoint(mCircleZones[4], mCircleZones[8], mCircleZones[9]));
            mTriangleMidPoints.Add(new TriangleMidpoint(mCircleZones[4], mCircleZones[5], mCircleZones[9]));
            mTriangleMidPoints.Add(new TriangleMidpoint(mCircleZones[5], mCircleZones[9], mCircleZones[10]));
            mTriangleMidPoints.Add(new TriangleMidpoint(mCircleZones[5], mCircleZones[6], mCircleZones[10]));
            mTriangleMidPoints.Add(new TriangleMidpoint(mCircleZones[6], mCircleZones[10], mCircleZones[11]));

            mTriangleMidPoints.Add(new TriangleMidpoint(mCircleZones[7], mCircleZones[8], mCircleZones[12]));
            mTriangleMidPoints.Add(new TriangleMidpoint(mCircleZones[12], mCircleZones[8], mCircleZones[13]));
            mTriangleMidPoints.Add(new TriangleMidpoint(mCircleZones[8], mCircleZones[13], mCircleZones[9]));
            mTriangleMidPoints.Add(new TriangleMidpoint(mCircleZones[13], mCircleZones[9], mCircleZones[14]));
            mTriangleMidPoints.Add(new TriangleMidpoint(mCircleZones[9], mCircleZones[10], mCircleZones[14]));
            mTriangleMidPoints.Add(new TriangleMidpoint(mCircleZones[14], mCircleZones[10], mCircleZones[15]));
            mTriangleMidPoints.Add(new TriangleMidpoint(mCircleZones[10], mCircleZones[11], mCircleZones[15]));

            mTriangleMidPoints.Add(new TriangleMidpoint(mCircleZones[12], mCircleZones[13], mCircleZones[16]));
            mTriangleMidPoints.Add(new TriangleMidpoint(mCircleZones[16], mCircleZones[13], mCircleZones[17]));
            mTriangleMidPoints.Add(new TriangleMidpoint(mCircleZones[13], mCircleZones[17], mCircleZones[14]));
            mTriangleMidPoints.Add(new TriangleMidpoint(mCircleZones[17], mCircleZones[14], mCircleZones[18]));
            mTriangleMidPoints.Add(new TriangleMidpoint(mCircleZones[14], mCircleZones[15], mCircleZones[18]));

            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[0],mTriangleMidPoints[1]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[1], mTriangleMidPoints[2]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[2], mTriangleMidPoints[3]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[3], mTriangleMidPoints[4]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[0], mTriangleMidPoints[6]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[2], mTriangleMidPoints[8]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[10], mTriangleMidPoints[4]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[5], mTriangleMidPoints[6]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[6], mTriangleMidPoints[7]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[7], mTriangleMidPoints[8]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[9], mTriangleMidPoints[8]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[9], mTriangleMidPoints[10]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[10], mTriangleMidPoints[11]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[5], mTriangleMidPoints[12]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[14], mTriangleMidPoints[7]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[16], mTriangleMidPoints[9]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[18], mTriangleMidPoints[11]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[12], mTriangleMidPoints[13]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[13], mTriangleMidPoints[14]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[14], mTriangleMidPoints[15]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[15], mTriangleMidPoints[16]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[17], mTriangleMidPoints[16]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[17], mTriangleMidPoints[18]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[13], mTriangleMidPoints[19]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[15], mTriangleMidPoints[21]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[23], mTriangleMidPoints[17]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[19], mTriangleMidPoints[20]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[20], mTriangleMidPoints[21]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[21], mTriangleMidPoints[22]));
            mCurrentConnections.Add(new CurrentConnection(mTriangleMidPoints[22], mTriangleMidPoints[23]));


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
                mTriangleMidPoints[i].CalculateMidPoint();
                mCentroids.Add(mTriangleMidPoints[i].GetMidPoint());
            }



            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                SpringConstraint spring = (SpringConstraint) mSpringConstraints[rnd.Next(mSpringConstraints.Count)];
                spring.mRestLength += - 5 + rnd.Next(10);
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

            foreach (CurrentConnection connection in mCurrentConnections)
            {
                connection.DrawConnection(mWindow);
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
