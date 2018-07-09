using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using SFML.Window;

namespace DynamicNetwork
{
    class Core
    {
        private RenderWindow mWindow;
        private Vector2u mScreenSize;
        private Font mFont;
        private Text mText;

        private List<CircleZone> mCircleZones;
        private List<Intersection> mAllIntersections;

        private CircleShape mIntersectionMarker;

        public Core()
        {
            mScreenSize = new Vector2u(1280,720);
            // Using ContextSettings only for setting AntialiasingLevel 
            ContextSettings settings = new ContextSettings();
            settings.AntialiasingLevel = 4;
            mWindow = new RenderWindow(new SFML.Window.VideoMode(mScreenSize.X,mScreenSize.Y), "", Styles.Default, settings);

            //Setting method "OnClose()" for the Closed-Event (for example: when clicking of the X button of the window)
            mWindow.Closed += OnClose;


            mFont = new Font("calibri.ttf");
            mText = new Text("It works!",mFont);
            mText.Color = Color.Black;
            mText.Position = new Vector2f(mScreenSize.X/2f,mScreenSize.Y/2);

            mIntersectionMarker = new CircleShape(4);
            mIntersectionMarker.FillColor = Color.Cyan;
            mIntersectionMarker.Position = new Vector2f(-100,-100);
            mIntersectionMarker.Origin = new Vector2f(4,4);

            mCircleZones = new List<CircleZone>();
            mCircleZones.Add(new CircleZone(mWindow, 100f, new Vector2f(300, 300)));
            mCircleZones.Add(new CircleZone(mWindow, 100f, new Vector2f(450, 300)));
            mCircleZones.Add(new CircleZone(mWindow, 50f, new Vector2f(300, 600)));
            mCircleZones.Add(new CircleZone(mWindow, 120f, new Vector2f(450, 500)));

            mAllIntersections = new List<Intersection>();

        }

        public void Run()
        {
            while (mWindow.IsOpen)
            {
                mWindow.DispatchEvents();


                Update();
                Render();
            }
        }

        public void Update()
        {
            // Bruteforce - inefficient O(n²)
            // TODOO: Later: NeighbourSearch
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
            mText.Draw(mWindow,RenderStates.Default);


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


            mWindow.Display();
        }

        //Closing application
        private void OnClose(object sender, EventArgs e)
        {
            mWindow.Close();
        }
    }
}
