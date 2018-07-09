using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;


public class MathUtil
{

    public static Vector2f Normalize(Vector2f dir)
    {
        float length = Length(dir);
        return new Vector2f(dir.X / length, dir.Y / length);
    }

    public static float Length(Vector2f vec)
    {
        return (float)Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y);
    }

    public static float Dot(Vector2f vecA, Vector2f vecB)
    {
        return vecA.X * vecB.X + vecA.Y * vecB.Y;
    }

    public static Vector2f GetNormalOf(Vector2f vector)
    {
        return new Vector2f(vector.Y, -vector.X);
    }

    public static float SquaredLength(Vector2f vec)
    {
        return vec.X * vec.X + vec.Y * vec.Y;
    }
}
