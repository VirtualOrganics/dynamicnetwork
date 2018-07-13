using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;


public static class MathUtil
{
    public static float SquaredLength(this Vector2f vec)
    {
        return vec.X * vec.X + vec.Y * vec.Y;
    }

    public static float Length(this Vector2f vec)
    {
        return (float) Math.Sqrt(vec.SquaredLength());
    }

    public static float Dot(this Vector2f vec, Vector2f vec2)
    {
        return vec.X * vec.X + vec2.Y * vec2.Y;
    }

    public static Vector2f Normalize(Vector2f vec)
    {
        float length = vec.Length();
        vec.X /= length;
        vec.Y /= length;
        return new Vector2f(vec.X,vec.Y);
    }

    public static Vector2f GetNormal(Vector2f vec)
    {
        return new Vector2f(vec.Y,-vec.X);
    }

    public static void Line(this RenderWindow window,Vector2f start, Vector2f end, SFML.Graphics.Color color)
    {
        Vertex[] lines = new Vertex[] {new Vertex(start,color),new Vertex(end,color)};
        window.Draw(lines,PrimitiveType.Lines,RenderStates.Default);
    }

    public static float ToRadians(float degress)
    {
        return (float) (degress * Math.PI / 180);
    }
    public static float ToDegrees(float radians)
    {
        return (float)(180 / Math.PI * radians);
    }

    public static double AngleBetween(Vector2f vector1, Vector2f vector2)
    {
        double sin = vector1.X * vector2.Y - vector2.X * vector1.Y;
        double cos = vector1.X * vector2.X + vector1.Y * vector2.Y;

        return Math.Atan2(sin, cos);
    }

    public static Vector2f RotateAboutPoint(Vector2f origin,Vector2f point, float angleInRadians)
    {
        float s = (float) Math.Sin(angleInRadians);
        float c = (float) Math.Cos(angleInRadians);
        point -= origin;
        Vector2f newPoint = new Vector2f(point.X*c-point.Y*s,point.X*s+point.Y*c);
        newPoint += origin;
        return newPoint;
    }

    public static int IndexOf(int i, int j, int N)
    {
        return i + (N + 2) * j;
    }

    public static Vector2f GetCenterOf(Vector2f start, Vector2f end)
    {
        Vector2f direction = end - start;
        float distance = Length(direction)/2f;
        Vector2f center = start + Normalize(direction) * distance;
        return center;
    }
}

