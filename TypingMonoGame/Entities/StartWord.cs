using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TypingMonoGame.States;

namespace TypingMonoGame.Entities;

public class StartWord : IsTyped
{
    private readonly Game1 _game;
    
    public StartWord(string text, Game1 game, Vector2 position, SpriteFont font) : base(text, font, position)
    {
        _game = game;
        IsFunctional = true;
    }

    public new void Update()
    {
        base.Update();
        if (Iterator == Text.Length)
            _game.StartGame();
    }

}