using Microsoft.Xna.Framework;

namespace Rectified_Capstone.Physics.Shapes;
public abstract class Collider
{
    public Vector2 Centre { get; protected set; }
    public Rectangle BoundingBox => GenerateBoundingBox();

    public Collider(Vector2 centre)
    {
        Centre = centre;
    }

    public abstract void Rotate(float rotation);

    public abstract void Move(Vector2 delta, float rotation = 0f);

    protected abstract Rectangle GenerateBoundingBox();
}
