using Microsoft.Xna.Framework;
using System;

namespace Rectified_Capstone.Physics.Shapes;
public class AxisAlignedRectangle : Collider
{
    public float Width { get; protected set; }
    public float Height { get; protected set; }

    public AxisAlignedRectangle(Vector2 centre, float width, float height) : base(centre)
    {
        Width = width;
        Height = height;
    }

    public override void Rotate(float rotation)
    {
        throw new Exception("Cannot rotate AxisAlignedRectangle - use OrientedRectangle instead");
    }

    public override void Move(Vector2 delta, float rotation = 0f)
    {
        Centre += delta;
    }

    protected override Rectangle GenerateBoundingBox()
    {
        return new Rectangle((int)MathF.Floor(Centre.X - Width / 2f), (int)MathF.Floor(Centre.Y - Height / 2f), (int)MathF.Ceiling(Width), (int)MathF.Ceiling(Height));
    }
}
