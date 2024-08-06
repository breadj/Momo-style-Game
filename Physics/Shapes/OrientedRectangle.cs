using Microsoft.Xna.Framework;
using Rectified_Capstone.Fundamentals;
using Rectified_Capstone.Globals;
using System;

namespace Rectified_Capstone.Physics.Shapes;
public class OrientedRectangle : Collider
{
    public float Width { get; protected set; }
    public float Height { get; protected set; }
    public float Rotation { get; protected set; }

    public OrientedRectangle(Vector2 centre, float width, float height, float rotation) : base(centre)
    {
        Width = width;
        Height = height;
        Rotation = rotation;
    }

    public OrientedRectangle(OrientedBoundingBox rectangle) : base(rectangle.Centre)
    {
        Width = rectangle.Width;
        Height = rectangle.Height;
        Rotation = rectangle.Rotation;
    }

    public override void Rotate(float rotation)
    {
        Rotation = Utility.NormaliseRotation(Rotation + rotation);
    }

    public override void Move(Vector2 delta, float rotation = 0f)
    {
        Centre += delta;
        Rotation = Utility.NormaliseRotation(Rotation + rotation);
    }

    protected override Rectangle GenerateBoundingBox()
    {
        Vector2[] corners = Utility.FindCorners(Centre, Width, Height, Rotation);

        float left = float.MaxValue;
        float top = float.MaxValue;
        float right = float.MinValue;
        float bottom = float.MinValue;
        for (int i = 0; i < corners.Length; i++)
        {
            if (corners[i].X < left)
                left = corners[i].X;
            if (corners[i].Y < top)
                top = corners[i].Y;
            if (corners[i].X > right)
                right = corners[i].X;
            if (corners[i].Y > bottom)
                bottom = corners[i].Y;
        }

        float width = right - left;
        float height = bottom - top;

        return new Rectangle((int)MathF.Floor(left), (int)MathF.Floor(top), (int)MathF.Ceiling(width), (int)MathF.Ceiling(height));
    }
}
