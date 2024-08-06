using Microsoft.Xna.Framework;
using Rectified_Capstone.Globals;
using System;

namespace Rectified_Capstone.Physics.Shapes;
public class Capsule : Collider
{
    public float Height { get; protected set; }     // does not include the radii of the semicircles at each end - basically only the height of the straight part
    public float Radius { get; protected set; }
    public float Rotation { get; protected set; }   // 0 radians: semi-circles at north and south of shape

    public Capsule(Vector2 centre, float height, float radius, float rotation) : base(centre)
    {
        Height = height;
        Radius = radius;
        Rotation = Utility.NormaliseRotation(rotation);
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
        // centre of the top and bottom semicircles
        Vector2 direction = Utility.AngleToVector(Rotation) * Height / 2f;
        Vector2 topOfCapsule = Centre + direction;
        Vector2 bottomOfCapsule = Centre - direction;

        float left;
        float top;
        float right;
        float bottom;
        if (Rotation <= -MathF.PI / 2f)         // topOfCapsule is to the southwest
        {
            left = topOfCapsule.X;
            top = bottomOfCapsule.Y;
            right = bottomOfCapsule.X;
            bottom = topOfCapsule.Y;
        }
        else if (Rotation <= 0)                 // topOfCapsule is to the northwest
        {
            left = topOfCapsule.X;
            top = topOfCapsule.Y;
            right = bottomOfCapsule.X;
            bottom = bottomOfCapsule.Y;
        }
        else if (Rotation <= MathF.PI / 2f)     // topOfCapsule is to the northeast
        {
            left = bottomOfCapsule.X;
            top = topOfCapsule.Y;
            right = topOfCapsule.X;
            bottom = bottomOfCapsule.Y;
        }
        else                                    // topOfCapsule is to the southeast
        {
            left = bottomOfCapsule.X;
            top = bottomOfCapsule.X;
            right = topOfCapsule.X;
            bottom = topOfCapsule.Y;
        }

        // take the semicircles at each end into consideration
        left -= Radius;
        top -= Radius;
        right += Radius;
        bottom += Radius;

        float width = right - left;
        float height = bottom - top;

        return new Rectangle((int)MathF.Floor(left), (int)MathF.Floor(top), (int)MathF.Ceiling(width), (int)MathF.Ceiling(height));
    }
}
