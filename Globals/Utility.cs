using Microsoft.Xna.Framework;
using System;

namespace Rectified_Capstone.Globals;

public static class Utility
{
    public static void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }

    public static int Round(float x) => (int)(x + 0.5f);
    public static int Sign(float x) => x < 0 ? -1 : 1;
    public static int Sign(int x) => x < 0 ? -1 : 1;

    #region Vectors (extensions and rotation)
    public static void Deconstruct(this Vector2 v, out float x, out float y) { x = v.X; y = v.Y; }

    public static Point Round(this Vector2 v) => new Point(Round(v.X), Round(v.Y));

    public static Vector2 FindNormal(Vector2 start, Vector2 end) => new Vector2(-(end.Y - start.Y), end.X - start.X);

    public static Vector2 FindNormal(Vector2 edge) => new Vector2(-edge.Y, edge.X);

    public static Vector2 VectorProjection(Vector2 a, Vector2 b) => b * (Vector2.Dot(a, b) / b.LengthSquared());

    public static float CrossProduct(Vector2 a, Vector2 b) => a.X * b.Y - a.Y * b.X;

    public static Vector2 Rotate(Vector2 vector, float rotation) => new Vector2(
        vector.X * MathF.Cos(rotation) - vector.Y * MathF.Sin(rotation),
        vector.X * MathF.Sin(rotation) + vector.Y * MathF.Cos(rotation));

    // sets the vector to have the anchor = (0,0), rotates it, then adds adds the anchor offset back
    public static Vector2 RotateAroundAnchor(Vector2 vector, Vector2 anchor, float rotation) => Rotate(vector - anchor, rotation) + anchor;

    public static float VectorToAngle(Vector2 vector) => NormaliseRotation(MathF.Atan2(vector.Y, vector.X) + MathF.PI / 2f);

    // because 0 radians is +ve X, the angle is offset by -pi/2, so 0 radians is -ve Y (north)
    public static Vector2 AngleToVector(float angle) => new Vector2(MathF.Cos(angle -= MathF.PI / 2f), MathF.Sin(angle));

    public static float AngleTowards(Vector2 from, Vector2 to) => VectorToAngle(to - from);

    // keeps angle between (-pi,pi)
    public static float NormaliseRotation(float angle)
    {
        while (angle < -MathF.PI) angle += 2 * MathF.PI;
        while (angle > MathF.PI) angle -= 2 * MathF.PI;
        return angle;
    }
    #endregion Vectors (extensions and rotation)

    // finds the corners of a rectangle based on it's centre, width, height, and rotation
    public static Vector2[] FindCorners(Vector2 centre, float width, float height, float rotation = 0f)
    {
        Vector2[] corners = new Vector2[4];

        float halfWidth = width / 2f;
        float halfHeight = height / 2f;

        // calculates AABB corners before rotating them
        corners[0] = new Vector2(centre.X - halfWidth, centre.Y - halfHeight);
        corners[1] = new Vector2(centre.X + halfWidth, centre.Y - halfHeight);
        corners[2] = new Vector2(centre.X + halfWidth, centre.Y + halfHeight);
        corners[3] = new Vector2(centre.X - halfWidth, centre.Y + halfHeight);

        if (rotation == 0f || rotation == MathF.PI)
            return corners;

        for (int i = 0; i < 4; i++)
        {
            corners[i] = RotateAroundAnchor(corners[i], centre, rotation);
        }

        return corners;
    }
}
