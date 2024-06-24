using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TypingMonoGame.Themes;

namespace TypingMonoGame.States
{
    public abstract class State
    {
        protected ContentManager Content;
        protected GraphicsDeviceManager GraphicsDeviceManager;
        protected Game1 Game;
        protected SpriteFont Font;

        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update();

        protected State(Game1 game, GraphicsDeviceManager graphicsDeviceManager, ContentManager content, Theme theme)
        {
            Game = game;
            GraphicsDeviceManager = graphicsDeviceManager;
            Content = content;
            Font = theme.Font;
        }
    }
}
