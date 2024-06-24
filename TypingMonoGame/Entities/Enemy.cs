using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TypingMonoGame.Entities;

public class Enemy : IsTyped, IMoveable
{
    private bool _alive = true;
    private readonly List<Texture2D> _texture;
    private int _textureIndex = 0;
    private int _animTimer = 0;
    private readonly (float x, float y) _endPoint;
    private float _rotation = 0;
    private Vector2 _origin = new(0, 0);
    private readonly float _scale;
    public Enemy(string text, SpriteFont font, Vector2 position, List<Texture2D> texture, (float, float) endPoint, float scale) : base(text, font, position)
    {
        _texture = texture;
        IsActive = false;
        _endPoint = endPoint;
        _scale = scale;
    }

    public bool IsActive { get; set; }

    public new void Update()
    {
        if (_alive)
        {
            base.Update();
            if (Iterator == Text.Length)
            {
                Erase();
            }
        }
        
    }

    public new void Draw(SpriteBatch spriteBatch)
    {
        if (_alive)
        {
            if (_textureIndex < _texture.Count - 1)
            {
                if (_animTimer % 5 == 0)
                {
                    _textureIndex++;
                }
                _animTimer++;
            }
            else
            {
                _textureIndex = 0;
            }
            base.Draw(spriteBatch);
            _rotation = (float)Math.Atan2(Position.Y - _endPoint.y, Position.X - _endPoint.x);
            // ReSharper disable once PossibleLossOfFraction
            // ReSharper disable once PossibleLossOfFraction
            _origin = new Vector2(_texture[0].Width / 2, _texture[0].Height / 2);
            spriteBatch.Draw(_texture[_textureIndex], new Vector2(Position.X, Position.Y + 50), null, Color.White, _rotation, _origin, _scale, SpriteEffects.FlipHorizontally, 0f);
        }
    }


    public void Erase()
    {
        _alive = false;
    }

    public bool CheckStatus()
    {
        return _alive;
    }

    public string GetText()
    {
        return Text;
    }


    public Rectangle GetBoundingBox()
    {
        var textSize = Font.MeasureString(Text);
        return new Rectangle((int)Position.X - 5, (int)Position.Y - 5, (int)textSize.X + 10, (int)textSize.Y + 10);
    }

    public void Move(double x, double y)
    {
        Position += new Vector2((float)x, (float)y);
        if (Position.Y > 500)
        {
            Erase();
        }
    }
}