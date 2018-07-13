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
        private TestScene mTestScene;
        private float deltaTime;



        public Core()
        {
            mScreenSize = new Vector2u(1280,720);
            ContextSettings settings = new ContextSettings();
            settings.AntialiasingLevel = 4;
            mWindow = new RenderWindow(new SFML.Window.VideoMode(mScreenSize.X,mScreenSize.Y), "", Styles.Default, settings);
            mWindow.SetFramerateLimit(40);
            mWindow.Closed += OnClose;
            mClock = new Clock();
            mTestScene = new TestScene(mWindow);
            deltaTime = 1f;
        }

        public void Run()
        {
            while (mWindow.IsOpen)
            {
                mClock.Restart();
                mWindow.DispatchEvents();

                Update(deltaTime);
                Render();

                deltaTime = mClock.ElapsedTime.AsSeconds();
            }
        }

        public void Update(float dt)
        {
            mWindow.SetTitle("FPS: "+1/dt);
            mTestScene.Update(dt);
        }

        public void Render()
        {
            mWindow.Clear(Color.White);
            mTestScene.Draw();
            mWindow.Display();
        }

        //Closing application
        private void OnClose(object sender, EventArgs e)
        {
            mWindow.Close();
        }
    }
}
