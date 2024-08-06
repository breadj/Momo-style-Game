using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rectified_Capstone.Fundamentals;

namespace Rectified_Capstone;

public class Game1 : Game
{
    private GraphicsDeviceManager graphics;

    private RenderTarget2D renderTarget;

    public Camera Camera;

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

#if DEBUG
        graphics.IsFullScreen = false;
#else
        graphics.IsFullScreen = true;
#endif
    }

    protected override void Initialize()
    {
        // sets screen resolution to 1920x1080
        graphics.PreferredBackBufferWidth = Globals.Globals.ScreenWidth;
        graphics.PreferredBackBufferHeight = Globals.Globals.ScreenHeight;
        graphics.ApplyChanges();

        renderTarget = new RenderTarget2D(graphics.GraphicsDevice, Globals.Globals.ScreenWidth, Globals.Globals.ScreenHeight);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        Globals.Globals.GraphicsDevice = graphics.GraphicsDevice;
        Globals.Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        Camera = new Camera(new(0, 0, Globals.Globals.ScreenWidth, Globals.Globals.ScreenHeight));

        Globals.Globals.DebugFont = Content.Load<SpriteFont>("DebugFont");

        Globals.Globals.Pixel = new Texture2D(graphics.GraphicsDevice, 1, 1);
        Globals.Globals.Pixel.SetData(new Color[] { Color.White });
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        Globals.Globals.GameTime = gameTime;

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(renderTarget);
        GraphicsDevice.Clear(Color.DarkBlue);

        Globals.Globals.SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, RasterizerState.CullCounterClockwise, null, Camera.TransformMatrix);

        // drawing goes in here


        Globals.Globals.SpriteBatch.End();

        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.CornflowerBlue);
        Globals.Globals.SpriteBatch.Begin();
        Globals.Globals.SpriteBatch.Draw(renderTarget, new Rectangle(0, 0, 1920, 1080), Color.White);
        Globals.Globals.SpriteBatch.End();

        base.Draw(gameTime);
    }
}
