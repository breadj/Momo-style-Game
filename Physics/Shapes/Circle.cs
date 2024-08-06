using Microsoft.Xna.Framework;
using System;

namespace Rectified_Capstone.Physics.Shapes;
public class Circle : Collider
{
    public float Radius { get; protected set; }

    public Circle(Vector2 centre, float radius) : base(centre)
    {
        Radius = radius;
    }

    public override void Rotate(float rotation)
    {
        // empty - a circle won't change based on rotation
    }

    public override void Move(Vector2 delta, float rotation = 0f)
    {
        Centre += delta;
    }

    protected override Rectangle GenerateBoundingBox()
    {
        int sideLength = (int)MathF.Ceiling(2f * Radius);
        return new Rectangle((int)MathF.Floor(Centre.X - Radius), (int)MathF.Floor(Centre.Y - Radius), sideLength, sideLength);
    }
}
