using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using LDtk;
using LDtk.Renderer;
using DreamMono.entities;
using System;
using LDtkTypes;
using System.Runtime.CompilerServices;


namespace DreamMono;

public class Game1 : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    Texture2D spriteSheet;
    LDtkWorld world;
    LDtkFile file;
    PlayerEntity player;
    Camera camera;

    ExampleRenderer renderer;

    float deltaTime, totalTime;

    float pixelScale = 1f;

    public static  Texture2D pixel{ get; set;}

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    public void MonogameInitializer()
    {
        Window.Title = "DreamSpeed protoype";
        spriteBatch = new SpriteBatch(GraphicsDevice);
        graphics.PreferredBackBufferHeight = Globals.HEIGHT;
        graphics.PreferredBackBufferWidth = Globals.WIDTH;
        graphics.ApplyChanges();

        Window.ClientSizeChanged += (o, e) => pixelScale = Math.Max(GraphicsDevice.Viewport.Height / 180, 1);

        pixelScale = Math.Max(GraphicsDevice.Viewport.Height / 186, 0);

        pixel = new Texture2D(GraphicsDevice, 1, 1);
        pixel.SetData(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });
    }
    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        MonogameInitializer();

        camera = new Camera(GraphicsDevice);
        renderer = new ExampleRenderer(spriteBatch, Content);
        file = LDtkFile.FromFile("ldtk/DreamSpeed", Content);
        world = file.LoadWorld(Worlds.World.Iid);
        spriteSheet = Content.Load<Texture2D>("assets/player-table-16-16");

        foreach (LDtkLevel level in world.Levels)
        {
            renderer.PrerenderLevel(level);
        }

        Player playerData = world.GetEntity<Player>();
        player = new PlayerEntity(renderer, playerData ,spriteSheet);

        base.Initialize(); 

    }


    protected override void Update(GameTime gameTime)
    {

        deltaTime = totalTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (LDtkLevel level in world.Levels)
        {
            if (level.Contains(player.Position))
            {
                player.Level = level;
                break;
            }
        }

        camera.Update();
        camera.position = new Vector2(player.Position.X, player.Position.Y);
        camera.zoom = pixelScale;
        player.Update(deltaTime, totalTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        float totalTime = (float)gameTime.TotalGameTime.TotalSeconds;
        GraphicsDevice.Clear(file.BgColor);

        spriteBatch.Begin(SpriteSortMode.Deferred, null, samplerState: SamplerState.PointClamp, transformMatrix: camera.transform);
        {
            foreach (LDtkLevel level in world.Levels)
            {
                renderer.PrerenderLevel(level);
            }
            player.Draw(totalTime);
        }
        spriteBatch.End();

        base.Draw(gameTime);
    }
}
