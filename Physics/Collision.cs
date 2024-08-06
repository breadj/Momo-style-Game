using Microsoft.Xna.Framework;
using Rectified_Capstone.Globals;
using Rectified_Capstone.Physics.Shapes;
using System;

namespace Rectified_Capstone.Physics;
public static class Collision
{
    #region Collision Detection

    #region AARectangular
    public static bool AARectangular(Collider a, Collider b)
    {
        return a.BoundingBox.Intersects(b.BoundingBox);
    }

    public static bool AARectangular(Collider a, Collider b, out CollisionDetails cd)
    {
        cd = new CollisionDetails(a, b);

        Rectangle intersection;
        if (!(intersection = Rectangle.Intersect(a.BoundingBox, b.BoundingBox)).IsEmpty)
        {
            cd.Collided = true;

            FindAARectangleCD(a, b, intersection, out Vector2 aNormal, out Vector2 bNormal, out float depth);
            cd.ANormal = aNormal;
            cd.BNormal = bNormal;
            cd.Depth = depth;
        }

        return cd.Collided;
    }

    private static void FindAARectangleCD(Collider a, Collider b, Rectangle intersection,
        out Vector2 aNormal, out Vector2 bNormal, out float depth)
    {
        // calculates the normal b-to-a (aNormal)
        Vector2 displacement = a.Centre - b.Centre;
        float absDiffX = MathF.Abs(displacement.X);
        float absDiffY = MathF.Abs(displacement.Y);

        // |X| > |Y| means the collision is on the x-axis (and vice versa)
        // |X| == |Y| means just shove them out from the corner
        if (absDiffX > absDiffY)
        {
            aNormal = new Vector2(Utility.Sign(displacement.X), 0);
            depth = intersection.Width;
        }
        else if (absDiffY > absDiffX)
        {
            aNormal = new Vector2(0, Utility.Sign(displacement.Y));
            depth = intersection.Height;
        }
        else    // if absDiffX == absDiffY
        {
            aNormal = Vector2.Normalize(displacement);
            depth = displacement.Length();
        }

        bNormal = -aNormal;
    }
    #endregion AARectangular

    #region Circular
    public static bool Circular(Circle a, Circle b, out CollisionDetails cd)
    {
        cd = new CollisionDetails(a, b);

        float depth = a.Radius + b.Radius - (a.Centre - b.Centre).Length();
        if (depth > 0)
        {
            cd.Collided = true;
            cd.ANormal = Vector2.Normalize(a.Centre - b.Centre);
            cd.BNormal = -cd.ANormal;
            cd.Depth = depth;
        }

        return cd.Collided;
    }
    #endregion Circular

    #region OrientedRectangular

    public static bool OrientedRectangular(OrientedRectangle a, OrientedRectangle b, out CollisionDetails cd)
    {

    }

    #endregion OrientedRectangular

    #region Capsular
    // basically just dynamic circle against dynamic circle collision
    public static bool Capsular(Capsule a, Capsule b, out CollisionDetails cd)
    {

    }

    #endregion Capsular


    #endregion Collision Detection
}