using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TypingMonoGame.Entities;
using TypingMonoGame.Themes;

namespace TypingMonoGame.States
{
    public class GameState : State
    {
        private readonly List<Enemy> _enemies;
        private readonly List<string> _words;
        private readonly Random _random;
        private int _currentEnemies = 1;
        private readonly (float x , float y) EndPoint = (350, 1000);
        private readonly (float x, float y) PlayerPoint = (350, 400);
        private readonly List<Texture2D> _player;
        private float _rotation;
        private readonly Vector2 _origin;
        private readonly SoundEffect _soundEffect;
        private int _soundCounter = 0;
        private readonly Song _backgroundMusic;
        private int _action = 0;
        private readonly List<Texture2D> _enemiesTextures;
        private int _score = 0;
        private int _lives = 3;
        private int _wordIndex = 0;
        public GameState(Game1 game, GraphicsDeviceManager graphicsDeviceManager, ContentManager content, Theme theme)
            : base(game, graphicsDeviceManager, content, theme)
        {
            Theme = theme;
            _enemies = new List<Enemy>();
            if (Theme.WordTheme == 0)
            {
                _words = Theme.Words;
            }
            else
            {
                _words = new List<string>()
                {
                    "Its", "a", "dangerous", "business", "Frodo", "going", "out", "your", "door",
                    "You", "step", "onto", "the", "road", "and", "if", "you", "dont", "keep", "your",
                    "feet", "theres", "no", "knowing", "where", "you", "might", "be", "swept", "off",
                    "to",
                    "The", "road", "goes", "ever", "on", "and", "on", "down", "from", "the", "door",
                    "where", "it", "began",
                    "now", "far", "ahead", "the", "road", "has", "gone", "and", "I", "must", "follow",
                    "if", "I", "can", "pursuing", "it", "with", "eager", "feet", "until", "it", "joins",
                    "some", "larger", "way", "where", "many", "paths", "and", "errands", "meet",
                    "All", "we", "have", "to", "decide", "is", "what", "to", "do", "with", "the", "time",
                    "that", "is", "given", "to", "us",
                    "The", "world", "is", "indeed", "full", "of", "peril", "and", "in", "it", "there",
                    "are", "many", "dark", "places", "but", "still", "there", "is", "much", "that", "is",
                    "fair", "and", "though", "in", "all", "lands", "love", "is", "now", "mingled", "with",
                    "grief", "it", "grows", "perhaps", "the", "greater",
                    "I", "wish", "it", "need", "not", "have", "happened", "in", "my", "time", "said",
                    "Frodo",
                    "So", "do", "I", "said", "Gandalf", "and", "so", "do", "all", "who", "live", "to",
                    "see", "such", "times",
                    "But", "that", "is", "not", "for", "them", "to", "decide",
                    "All", "we", "have", "to", "decide", "is", "what", "to", "do", "with", "the", "time",
                    "that", "is", "given", "us"
                };
            }
            _random = new Random();
            _player = Theme.Player;
            _origin = new Vector2(_player[0].Width / 2, _player[0].Height / 2);
            _soundEffect = Theme.Shoot;
            _backgroundMusic = Theme.BackgroundMusic;
            MediaPlayer.Volume = 0.2f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_backgroundMusic);
            _enemiesTextures = Theme.Enemy;
        }

        public static Theme Theme { get; set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(Font, $"Score: {_score}", new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(Font, $"Lives: {_lives}", new Vector2(10, 30), Color.White);
            foreach (var enemy in _enemies)
            {
                enemy.Draw(spriteBatch);
                if (enemy.IsActive)
                {
                    Rectangle hitbox = enemy.GetBoundingBox();
                    DrawingHelper.DrawRectangle(spriteBatch, hitbox, Color.Red);
                    _rotation = (float)Math.Atan2(PlayerPoint.y - hitbox.Y, PlayerPoint.x - hitbox.X);
                    float scale = 0.3f;
                    if (Theme.ThemeName.Equals("Fantasy"))
                    {
                        scale = 0.15f;
                    }
                    spriteBatch.Draw(_player[_action], new Vector2(PlayerPoint.x, PlayerPoint.y), null, Color.White, _rotation, _origin, scale, SpriteEffects.FlipHorizontally, 0f);
                }
                
            }
            spriteBatch.End();
        }


        public override void Update()
        {
            while (_enemies.Count < _currentEnemies)
            {
                string randomWord = "";
                if (Theme.WordTheme == 0)
                {
                    randomWord = _words[_random.Next(_words.Count)];
                }
                else
                {
                    _wordIndex++;
                    if (_wordIndex >= _words.Count)
                    {
                        _wordIndex = 0;
                    }
                    randomWord = _words[_wordIndex];
                }

                var spawnVector = new Vector2(_random.Next(0, 750), 0);
                int counter = 0;
                while (_enemies.Any(e => Vector2.Distance(e.Position, spawnVector) < 100))
                {
                    spawnVector = new Vector2(_random.Next(0, 750), 0);
                    counter++;
                    if (counter == 20)
                    {
                        break;
                    }
                }
                float scale = 0.25f;
                if (Theme.ThemeName.Equals("Fantasy"))
                {
                    scale = 1f;
                }
                _enemies.Add(new Enemy(randomWord, Font, spawnVector, _enemiesTextures, EndPoint, scale));
            }

            Enemy activeEnemy = _enemies.FirstOrDefault(e => e.IsActive);

            foreach (var enemy in _enemies.ToList())
            {
                var (directionX, directionY) = CalculateDirection(enemy.Position.X, enemy.Position.Y);
                enemy.Move(directionX * 0.5, directionY * 0.5);

                if (activeEnemy == null)
                {
                    if (!enemy.IsActive && enemy.CheckStatus())
                    {
                        enemy.IsActive = true;
                        activeEnemy = enemy;
                        _soundCounter = 0;
                    }
                }

                if (enemy.IsActive)
                {
                    enemy.Update();
                    if (enemy.GetIterator() > _soundCounter)
                    {
                        _soundCounter++;
                        _soundEffect.Play();
                        _action = 1;
                    }
                    else
                    {
                        _action = 0;
                    }

                }

                if (!enemy.CheckStatus())
                {
                    _enemies.Remove(enemy);
                    _currentEnemies++;
                    _score += 10;
                    if (_enemies.Count > 20)
                    {
                        _currentEnemies = 1;

                    }
                }

                if (enemy.Position.Y > 500)
                {
                    _enemies.Remove(enemy);
                    _lives--;
                    _score -= 10;
                    _wordIndex -= _enemies.Count;
                    _enemies.Clear();
                    _currentEnemies = 1;
                    
                    if (_lives == 0)
                    {
                        Game.ChangeState(new GameOverState(Game, GraphicsDeviceManager, Content, Theme, _score));
                        MediaPlayer.Stop();
                    }
                }
            }
            
        }

        public (double, double) CalculateDirection(double x, double y)
        {
            double playerX = EndPoint.x;
            double playerY = EndPoint.y;

            double directionX = playerX - x;
            double directionY = playerY - y;

            double magnitude = Math.Sqrt(directionX * directionX + directionY * directionY);
            directionX /= magnitude;
            directionY /= magnitude;

            return (directionX, directionY);
        }
    }
}
