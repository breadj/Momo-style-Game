using Microsoft.Xna.Framework;
using Rectified_Capstone.Globals;
using Rectified_Capstone.Physics.Shapes;

namespace Rectified_Capstone.Physics;
public struct CollisionDetails
{
    public bool Collided = false;

    public Collider A = null;
    public Vector2 ANormal = Vector2.Zero;

    public Collider B = null;
    public Vector2 BNormal = Vector2.Zero;

    public Rectangle Intersection = Rectangle.Empty;
    public int IntersectionArea => Intersection.Width * Intersection.Height;

    public float Depth = 0f;

    public CollisionDetails() { }

    public CollisionDetails(Collider a, Collider b)
    {
        A = a;
        B = b;
    }

    public void SwapAB()
    {
        Utility.Swap(ref A, ref B);
        Utility.Swap(ref ANormal, ref BNormal);
    }
}
