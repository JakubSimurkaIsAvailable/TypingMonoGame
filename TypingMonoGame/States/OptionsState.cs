using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TypingMonoGame.Themes;

namespace TypingMonoGame.States;

public class OptionsState : State
{
    public Theme Theme { get; set; }
    private int _position;
    public OptionsState(Game1 game, GraphicsDeviceManager graphicsDeviceManager, ContentManager content, Theme theme) : base(game, graphicsDeviceManager, content, theme)
    {
        Theme = theme;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.DrawString(Font, $"Theme: {Theme.ThemeName}", new Vector2(100, 100), Color.White);
        if (Theme.WordTheme == 0)
        {
            spriteBatch.DrawString(Font, "Word Theme: Random", new Vector2(100, 150), Color.White);
        }
        else
        {
            spriteBatch.DrawString(Font, "Word Theme: Essay", new Vector2(100, 150), Color.White);
        }
        
        Vector2 textSize;
        Rectangle r = default;
        if (_position == 0)
        {
            textSize = Font.MeasureString($"Theme: {Theme.ThemeName}");
            r = new Rectangle(100 - 5, 100 - 5, (int)textSize.X + 10, (int)textSize.Y + 10);
        } else if (_position == 1)
        {
            if (Game.WordTheme == 0)
            {
                textSize = Font.MeasureString("Word Theme: Random");
            }
            else
            {
                textSize = Font.MeasureString("Word Theme: Essay");
            }
            r = new Rectangle(100 - 5, 150 - 5, (int)textSize.X + 10, (int)textSize.Y + 10);
        }
        DrawingHelper.DrawRectangle(spriteBatch, r, Color.Red);

        spriteBatch.End();
    }


    public override void Update()
    {
        var keyboardState = Keyboard.GetState();
        if (CurrentKeyState != keyboardState)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _position--;
                if (_position < 0)
                {
                    _position = 1;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _position++;
                if (_position > 1)
                {
                    _position = 0;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if (_position == 0)
                {
                    Game.ChangeTheme();
                    Theme = Game.GetTheme();
                }
                else if (_position == 1)
                {
                    Game.ChangeWordTheme();
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Back))
            {
                Game.ChangeState(new MenuState(Game, GraphicsDeviceManager, Content, Theme));
            }
        }
        CurrentKeyState = keyboardState;
    }

    public KeyboardState CurrentKeyState { get; set; }
}