using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;

namespace TypingMonoGame.Entities;

public class OptionsWord : IsTyped
{
    private readonly Game1 _game;

    public OptionsWord(string text, Game1 game,Vector2 position, SpriteFont font) : base(text, font, position)
    {
        _game = game;
        IsFunctional = true;
    }

    public new void Update()
    {
        base.Update();
        if (Iterator == Text.Length)
            _game.Options();
    }

}