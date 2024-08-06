using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rectified_Capstone.Globals;

public static class Globals
{
    public static int ScreenWidth = 1920;
    public static int ScreenHeight = 1080;

    public static float TimeScale = 1f;
    public static GameTime GameTime;

    public static GraphicsDevice GraphicsDevice;
    public static SpriteBatch SpriteBatch;

    public static SpriteFont DebugFont;
    public static Texture2D Pixel;
}
