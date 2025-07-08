namespace DreamMono.entities;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

public class Player
{
    public Vector2 Position
    {
        get => data.Position;
        get => data.Position = value;
    }
}