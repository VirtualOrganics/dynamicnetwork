using SFML.Graphics;
using SFML.System;
using System;

namespace DynamicNetwork
{
    class Core
    {
        private RenderWindow mWindow;
        private Vector2u mScreenSize;
        private Font mFont;
        private Text mText;

        public Core()
        {
            mScreenSize = new Vector2u(1280,720);
            mWindow = new RenderWindow(new SFML.Window.VideoMode(mScreenSize.X,mScreenSize.Y), "", SFML.Window.Styles.Default);
            mWindow.Closed += OnClose;
            mFont = new Font("calibri.ttf");
            mText = new Text("It works!",mFont);
            mText.Color = Color.Black;
            mText.Position = new Vector2f(mScreenSize.X/2f,mScreenSize.Y/2);
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

        }

        public void Render()
        {
            mWindow.Clear(Color.White);
            mText.Draw(mWindow,RenderStates.Default);

            mWindow.Display();
        }

        private void OnClose(object sender, EventArgs e)
        {
            mWindow.Close();
        }

    }
}
