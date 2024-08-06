using Microsoft.Xna.Framework;
using Rectified_Capstone.Globals;
using System;

namespace Rectified_Capstone.Fundamentals;

public struct RectangleF : IEquatable<RectangleF>
{
    private static RectangleF emptyRectangle;

    public float X;
    public float Y;
    public float Width;
    public float Height;

    public static RectangleF Empty => emptyRectangle;

    public float Left => X;
    public float Right => X + Width;
    public float Top => Y;
    public float Bottom => Y + Height;

    public bool IsEmpty => Width == 0 && Height == 0 && X == 0 && Y == 0;

    public Vector2 Location
    {
        get { return new Vector2(X, Y); }
        set { X = value.X; Y = value.Y; }
    }

    public Vector2 Size
    {
        get { return new Vector2(Width, Height); }
        set { Width = value.X; Height = value.Y; }
    }

    public Vector2 Centre => new Vector2(X + Width / 2f, Y + Height / 2f);

    public RectangleF(float x, float y, float width, float height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public RectangleF(Vector2 location, Vector2 size)
    {
        X = location.X;
        Y = location.Y;
        Width = size.X;
        Height = size.Y;
    }

    public RectangleF(Rectangle rect)
    {
        X = rect.X;
        Y = rect.Y;
        Width = rect.Width;
        Height = rect.Height;
    }


    public static bool operator ==(RectangleF a, RectangleF b)
    {
        return a.X == b.X && a.Y == b.Y && a.Width == b.Width && a.Height == b.Height;
    }

    public static bool operator !=(RectangleF a, RectangleF b)
    {
        return !(a == b);
    }

    public bool Contains(int x, int y)
    {
        return X <= x && x < Right && Y <= y && y < Bottom;
    }

    public bool Contains(float x, float y)
    {
        return X <= x && x < Right && Y <= y && y < Bottom;
    }

    public bool Contains(Point value)
    {
        return X <= value.X && value.X < Right && Y <= value.Y && value.Y < Bottom;
    }

    public bool Contains(Vector2 value)
    {
        return X <= value.X && value.X < Right && Y <= value.Y && value.Y < Bottom;
    }

    public bool Contains(RectangleF value)
    {
        return Left <= value.Left && value.Right <= Right && Top <= value.Top && value.Bottom <= Bottom;
    }


    public override bool Equals(object obj)
    {
        return obj is RectangleF rect && this == rect;
    }

    public bool Equals(RectangleF other)
    {
        return this == other;
    }

    public override int GetHashCode()
    {
        return (((17 * 23 + X.GetHashCode()) * 23 + Y.GetHashCode()) * 23 + Width.GetHashCode()) * 23 + Height.GetHashCode();
    }


    public bool Intersects(RectangleF other)
    {
        return other.Left < Right && Left < other.Right && other.Top < Bottom && Top < other.Bottom;
    }

    public static void Intersect(ref RectangleF a, ref RectangleF b, out RectangleF result)
    {
        if (a.Intersects(b))
        {
            float x = MathF.Max(a.X, b.X);
            float y = MathF.Max(a.Y, b.Y);
            float w = MathF.Min(a.Right, b.Right);
            float h = MathF.Min(a.Bottom, b.Bottom);
            result = new RectangleF(x, y, w - x, h - y);
        }
        else
        {
            result = new RectangleF(0, 0, 0, 0);
        }
    }

    public override string ToString()
    {
        return "{X:" + X + " Y:" + Y + " Width:" + Width + " Height:" + Height + "}";
    }

    public void Deconstruct(out float x, out float y, out float width, out float height)
    {
        x = X;
        y = Y;
        width = Width;
        height = Height;
    }

    public Rectangle Round() => new Rectangle(Utility.Round(X), Utility.Round(Y), Utility.Round(Width), Utility.Round(Height));

    public static explicit operator RectangleF(Rectangle rect) => new RectangleF(rect);
    public static explicit operator Rectangle(RectangleF rect) => new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
}
