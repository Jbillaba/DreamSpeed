namespace DreamMono.entities;

using DreamMono;
using LDtk;
using LDtk.Renderer;
using LDtkTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Data.Common;

public class PlayerEntity
{
    public Vector2 Position
    {
        get => data.Position;
        set => data.Position = value;
    }

    public LDtkLevel Level { get; set; }
    readonly Player data;
    readonly ExampleRenderer renderer;
    readonly Texture2D texture;
    Vector2 startPosition;
    Vector2 velocity;
    KeyboardState oldkeyboard;

    public PlayerEntity(ExampleRenderer renderer, Player player, Texture2D texture)
    {
        data = player;
        this.renderer = renderer;
        this.texture = texture;

        startPosition = data.Position;
    }

    public void Update(float deltaTime, float totalTime)
    {
        KeyboardState keyboard = Keyboard.GetState();

        int h = (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left) ? -2 : 0) + (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right) ? +2 : 0);
        int v = (keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up) ? -2 : 0) + (keyboard.IsKeyDown(Keys.S) || keyboard.IsKeyDown(Keys.Down) ? +2 : 0);

        velocity = new Vector2(h * 60, v * 60);

        Position += velocity * deltaTime;
        oldkeyboard = keyboard;
    }

    public void Draw(float totalTime)
    {
        renderer.RenderEntity(data, texture);
    }
}