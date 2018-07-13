using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace DynamicNetwork
{
    class CurrentConnection
    {
        private TriangleMidpoint mMidPoint0;
        private TriangleMidpoint mMidPoint1;
        private float mCurrentValue;

        public CurrentConnection(TriangleMidpoint midpoint0, TriangleMidpoint midpoint1)
        {
            mMidPoint0 = midpoint0;
            mMidPoint1 = midpoint1;
        }


        public void DrawConnection(RenderWindow window)
        {
            MathUtil.Line(window,mMidPoint0.GetMidPoint(),mMidPoint1.GetMidPoint(),Color.Red);    
        }

        public void SetCurrentValue(float value)
        {
            mCurrentValue = value;
        }

        public float GetCurrentValue()
        {
            return mCurrentValue;
        }

    }
}
