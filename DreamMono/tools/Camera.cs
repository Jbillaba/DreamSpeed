using System.Net.Sockets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Camera(GraphicsDevice graphicsDevice)
{
    readonly GraphicsDevice graphicsDevice = graphicsDevice;
    public Vector2 position { get; set; }
    public float zoom { get; set; }
    public Matrix transform { get; private set; } = new();

    public void Update()
    {
        transform = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) * Matrix.CreateScale(zoom) * Matrix.CreateTranslation(graphicsDevice.Viewport.Width / 2f, graphicsDevice.Viewport.Height / 2f, 0);    
    }
}