using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using DynamicNetwork.PositionBasedDynamics_Simulator;
using SFML.Window;

namespace DynamicNetwork
{
    class Core
    {
        private RenderWindow mWindow;
        private Vector2u mScreenSize;
        private Clock mClock;
        private float deltaTime;

        private List<CircleZone> mCircleZones;
        private List<Intersection> mAllIntersections;

        private CircleShape mIntersectionMarker;
        private CircleZone mCurrentSelected;
        private Vector2f mousePos;

        private Simulator mSimulator;
        private List<AConstraint> mSpringConstraints;

        public Core()
        {
            mScreenSize = new Vector2u(1280,720);
            // Using ContextSettings only for setting AntialiasingLevel 
            ContextSettings settings = new ContextSettings();
            settings.AntialiasingLevel = 4;
            mWindow = new RenderWindow(new SFML.Window.VideoMode(mScreenSize.X,mScreenSize.Y), "", Styles.Default, settings);
            mWindow.SetFramerateLimit(40);
            mClock = new Clock();
            mSimulator = new Simulator(mWindow,10);
            mSpringConstraints = new List<AConstraint>(100);

            //Setting method "OnClose()" for the Closed-Event (for example: when clicking of the X button of the window)
            mWindow.Closed += OnClose;

            mIntersectionMarker = new CircleShape(2);
            mIntersectionMarker.FillColor = Color.Red;
            mIntersectionMarker.Position = new Vector2f(-100,-100);
            mIntersectionMarker.Origin = new Vector2f(2,2);

            mCircleZones = new List<CircleZone>();

            // 3 Circles in the middle
            mCircleZones.Add(new CircleZone(mWindow, 20f, new Vector2f(300, 300)));
            mCircleZones.Add(new CircleZone(mWindow, 20f, new Vector2f(340, 300)));
            mCircleZones.Add(new CircleZone(mWindow, 20f, new Vector2f(380, 300)));
            // 2 circles at the bottom
            mCircleZones.Add(new CircleZone(mWindow, 20f, new Vector2f(320, 340)));
            mCircleZones.Add(new CircleZone(mWindow, 20f, new Vector2f(360, 340)));
            // 2 circles at the top
            mCircleZones.Add(new CircleZone(mWindow, 20f, new Vector2f(320, 260)));
            mCircleZones.Add(new CircleZone(mWindow, 20f, new Vector2f(360, 260)));

            mAllIntersections = new List<Intersection>();
            mCurrentSelected = null;
            deltaTime = 1f;


            for (int i=0;i<mCircleZones.Count;i++)
            {
                mSimulator.AddNode(i,mCircleZones[i].GetPosition(),1f);
                mCircleZones[i].SetNodeIndex(i);
            }

            mSpringConstraints.Add(new SpringConstraint(mSimulator.GetNode(0), mSimulator.GetNode(1)));
            mSpringConstraints.Add(new SpringConstraint(mSimulator.GetNode(1), mSimulator.GetNode(2)));
            mSpringConstraints.Add(new SpringConstraint(mSimulator.GetNode(0), mSimulator.GetNode(3)));
            mSpringConstraints.Add(new SpringConstraint(mSimulator.GetNode(3), mSimulator.GetNode(1)));
            mSpringConstraints.Add(new SpringConstraint(mSimulator.GetNode(3), mSimulator.GetNode(4)));
            mSpringConstraints.Add(new SpringConstraint(mSimulator.GetNode(4), mSimulator.GetNode(1)));
            mSpringConstraints.Add(new SpringConstraint(mSimulator.GetNode(4), mSimulator.GetNode(2)));
            mSpringConstraints.Add(new SpringConstraint(mSimulator.GetNode(5), mSimulator.GetNode(6)));
            mSpringConstraints.Add(new SpringConstraint(mSimulator.GetNode(5), mSimulator.GetNode(0)));
            mSpringConstraints.Add(new SpringConstraint(mSimulator.GetNode(5), mSimulator.GetNode(1)));
            mSpringConstraints.Add(new SpringConstraint(mSimulator.GetNode(6), mSimulator.GetNode(1)));
            mSpringConstraints.Add(new SpringConstraint(mSimulator.GetNode(6), mSimulator.GetNode(2)));

            mSimulator.AddConstraints(mSpringConstraints);

            float desiredSpingStiffness = 0.05f;
            foreach (AConstraint constraint in mSpringConstraints)
            {
                SpringConstraint spring = (SpringConstraint) constraint;
                // see paper of PBD about springStiffness
                spring.mStiffness = (float) (1d - Math.Pow(1f - desiredSpingStiffness, 1f / 10f)); 
                Console.WriteLine(spring.mStiffness);
            }
        }

        public void Run()
        {
            while (mWindow.IsOpen)
            {
                mClock.Restart();
                mWindow.DispatchEvents();

                Input();
                Update(deltaTime);
                Render();

                deltaTime = mClock.ElapsedTime.AsSeconds();
            }
        }

        public void Update(float dt)
        {
            mWindow.SetTitle("FPS: "+1/dt);
            mSimulator.Update(dt);


            for (int i = 0; i < mCircleZones.Count; i++)
            {
                mCircleZones[i].MoveTo(mSimulator.GetPositionOfNode(i));
            }


            // Bruteforce - inefficient O(n²)
            // TODOO: Later: NeighbourSearch
            mAllIntersections.Clear();
            for(int i=0;i<mCircleZones.Count;i++)
            {
                for (int j = 0; j < mCircleZones.Count; j++)
                {
                    if (i == j) continue;
                    Intersection intersection = mCircleZones[i].GetIntersections(mCircleZones[j]);
                    if (intersection != null)
                    {
                        mAllIntersections.Add(intersection);
                    }
                }
            }
        }

        public void Render()
        {
            mWindow.Clear(Color.White);

            foreach (CircleZone zone in mCircleZones)
            {
                zone.Draw();
            }

            foreach (Intersection intersection in mAllIntersections)
            {
                mIntersectionMarker.Position = intersection.PointA;
                mWindow.Draw(mIntersectionMarker);

                mIntersectionMarker.Position = intersection.PointB;
                mWindow.Draw(mIntersectionMarker);
            }
            mSimulator.Draw();

            if (mCurrentSelected != null)
            {
                MathUtil.Line(mWindow, mousePos, mCurrentSelected.GetPosition(), Color.Blue);
            }

            mWindow.Display();
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

        //Closing application
        private void OnClose(object sender, EventArgs e)
        {
            mWindow.Close();
        }
    }
}
