using Microsoft.Xna.Framework;
using Rectified_Capstone.Globals;
using System;

namespace Rectified_Capstone.Fundamentals;

public struct OrientedBoundingBox
{
    public Vector2 Centre;
    public float Width;
    public float Height;
    public float Rotation;
    public Vector2[] Corners { get; init; } = new Vector2[4];     // should always be clockwise (e.g.: top-left, top-right, bottom-right, bottom-left)

    public OrientedBoundingBox(Vector2 centre, float width, float height, float rotation = 0f)
    {
        Centre = centre;
        Width = width;
        Height = height;
        Rotation = rotation;

        Corners = Utility.FindCorners(centre, width, height, rotation);
    }

    public OrientedBoundingBox(Rectangle rectangle)
    {
        Centre = rectangle.Center.ToVector2();
        Width = rectangle.Width;
        Height = rectangle.Height;

        Rotation = 0f;

        Vector2 topLeft = new Vector2(rectangle.Left, rectangle.Top);
        Vector2 topRight = new Vector2(rectangle.Right, rectangle.Top);
        Vector2 bottomRight = new Vector2(rectangle.Right, rectangle.Bottom);
        Vector2 bottomLeft = new Vector2(rectangle.Left, rectangle.Bottom);
        Corners = new Vector2[] { topLeft, topRight, bottomRight, bottomLeft };
    }

    public OrientedBoundingBox(RectangleF rectangle)
    {
        Centre = rectangle.Centre;
        Width = rectangle.Width;
        Height = rectangle.Height;

        Rotation = 0f;

        Vector2 topLeft = new Vector2(rectangle.Left, rectangle.Top);
        Vector2 topRight = new Vector2(rectangle.Right, rectangle.Top);
        Vector2 bottomRight = new Vector2(rectangle.Right, rectangle.Bottom);
        Vector2 bottomLeft = new Vector2(rectangle.Left, rectangle.Bottom);
        Corners = new Vector2[] { topLeft, topRight, bottomRight, bottomLeft };
    }

    public OrientedBoundingBox(Vector2[] corners)
    {
        if (corners.Length != 4)
            throw new ArgumentException("corners must be an array of Vector2 with exactly four elements");

        Corners = corners;

        Centre = (corners[2] - corners[0]) / 2f + corners[0];
        Width = Vector2.Distance(corners[0], corners[1]);
        Height = Vector2.Distance(corners[1], corners[2]);

        // assumes rotation is the angle from the centre to the midpoint between corner [0] and [1]
        Rotation = Utility.AngleTowards(Centre, (corners[1] - corners[0]) / 2f);
    }

    public OrientedBoundingBox(Vector2[] corners, float rotation)
    {
        if (corners.Length != 4)
            throw new ArgumentException("corners must be an array of Vector2 with exactly four elements");

        Corners = corners;

        Centre = (corners[2] - corners[0]) / 2f + corners[0];
        Width = Vector2.Distance(corners[0], corners[1]);
        Height = Vector2.Distance(corners[1], corners[2]);

        Rotation = rotation;
    }


    public void Rotate(float angle)
    {
        for (int i = 0; i < Corners.Length; i++)
        {
            Corners[i] = Utility.RotateAroundAnchor(Corners[i], Centre, angle);
        }
    }

    public void Move(Vector2 delta, float rotation = 0f)
    {
        Centre += delta;
        for (int i = 0; i < Corners.Length; i++)
        {
            Corners[i] += delta;

            // more efficient to do the rotation within the loop
            if (rotation != 0f)
            {
                Corners[i] = Utility.RotateAroundAnchor(Corners[i], Centre, rotation);
            }
        }
    }
}
