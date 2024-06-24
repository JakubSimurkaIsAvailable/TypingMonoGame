using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using TypingMonoGame.Entities;
using TypingMonoGame.Themes;

namespace TypingMonoGame.States;

public class MenuState : State
{
    private readonly StartWord _sw;
    private readonly OptionsWord _ow;

    public MenuState(Game1 game, GraphicsDeviceManager graphicsDeviceManager, ContentManager content, Theme theme) : base(game, graphicsDeviceManager, content, theme)
    {
        Font = theme.Font;
        if (graphicsDeviceManager != null)
        {
            _sw = new StartWord("start", game, new Vector2(
                    // ReSharper disable once PossibleLossOfFraction
                    graphicsDeviceManager.PreferredBackBufferWidth / 2 - Font.MeasureString("start").X / 2,
                    // ReSharper disable once PossibleLossOfFraction
                    graphicsDeviceManager.PreferredBackBufferHeight / 2 - 10),
                Font);
            _ow = new OptionsWord("options", game, new Vector2(
                // ReSharper disable once PossibleLossOfFraction
                graphicsDeviceManager.PreferredBackBufferWidth / 2 - Font.MeasureString("options").X / 2,
                // ReSharper disable once PossibleLossOfFraction
                graphicsDeviceManager.PreferredBackBufferHeight / 2 + 10), Font);
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.DrawString(Font, _sw.Text, _sw.Position, Color.White);
        spriteBatch.DrawString(Font, _ow.Text, _ow.Position, Color.White);
        _sw.Draw(spriteBatch);
        _ow.Draw(spriteBatch);
        spriteBatch.End();
    }


    public override void Update()
    {
        if (_ow.GetIterator() == 0)
            _sw.Update();
        if (_sw.GetIterator() == 0)
            _ow.Update();
    }
}