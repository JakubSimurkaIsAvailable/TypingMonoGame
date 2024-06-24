using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TypingMonoGame.Themes;

namespace TypingMonoGame.States
{
    public class GameOverState : State
    {
        public Theme Theme { get; set; }
        private readonly Game1 _game1;
        private readonly int _yourScore = 11;
        private readonly bool _firstSwap = true;
        private readonly List<int> _scores = new();

        public GameOverState(Game1 game, GraphicsDeviceManager graphicsDeviceManager, ContentManager content, Theme theme, int score) 
            : base(game, graphicsDeviceManager, content, theme)
        {
            Theme = theme;
            var score1 = score;
            _game1 = game;

            FileInfo file = new("ScoreBoard.txt");
            if (file.Exists)
            {
                try
                {
                    using StreamReader sr = file.OpenText();
                    string s;
                    while ((s = sr.ReadLine()) != null)
                    {
                        _scores.Add(int.Parse(s));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error reading the scoreboard file: " + ex.Message);
                }
            }

            for (int i = 0; i < _scores.Count; i++)
            {
                if (score1 > _scores[i])
                {
                    (_scores[i], score1) = (score1, _scores[i]);
                    if (_firstSwap)
                    {
                        _yourScore = i;
                        _firstSwap = false;
                    }
                }
            }

            try
            {
                using StreamWriter sw = file.CreateText();
                foreach (var item in _scores)
                {
                    sw.WriteLine(item.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing the scoreboard file: " + ex.Message);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(Theme.Font, "Game Over", new Vector2(100, 100), Color.White);
            spriteBatch.DrawString(Theme.Font, "Press Enter to restart", new Vector2(100, 150), Color.White);
            for (int i = 0; i < _scores.Count; i++)
            {
                Color c = (i == _yourScore) ? Color.Red : Color.White;
                spriteBatch.DrawString(Theme.Font, $"Score {i + 1}: {_scores[i]}", new Vector2(350, 100 + i * 30), c);
            }
            spriteBatch.End();
        }

        public override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                _game1.ChangeState(new MenuState(_game1, GraphicsDeviceManager, Content, Theme));
            }
        }
    }
}
