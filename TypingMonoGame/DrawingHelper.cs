using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TypingMonoGame;

public class DrawingHelper
{
    private static Texture2D _pixel;

    public static void Initialize(GraphicsDevice graphicsDevice)
    {
        _pixel = new Texture2D(graphicsDevice, 1, 1);
        _pixel.SetData(new[] { Color.White });
    }

    public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color)
    {
        // Top line
        spriteBatch.Draw(_pixel, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, 1), color);
        // Left line
        spriteBatch.Draw(_pixel, new Rectangle(rectangle.Left, rectangle.Top, 1, rectangle.Height), color);
        // Right line
        spriteBatch.Draw(_pixel, new Rectangle(rectangle.Right, rectangle.Top, 1, rectangle.Height), color);
        // Bottom line
        spriteBatch.Draw(_pixel, new Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, 1), color);
    }
}