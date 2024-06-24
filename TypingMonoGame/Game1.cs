using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TypingMonoGame.States;
using TypingMonoGame.Themes;

namespace TypingMonoGame
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private State _currentState;
        private State _nextState;
        private SpriteFont _font;
        // Textures from site https://opengameart.org/users/rileygombart
        // Riley Gombart or ChessMasterRiley
        private readonly List<Texture2D> _player;
        private readonly List<Texture2D> _enemies;
        private SoundEffect _soundEffect;
        private Song _backgroundMusic;
        private readonly List<Theme> _themes;
        private readonly List<string> _words;
        private int _styleIndex = 0;

        public KeyboardState CurrentKeyState { get; set; }

        public int WordTheme { get; set; }

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        public void StartGame()
        {
            ChangeState(new GameState(this, _graphics, Content, _themes[_styleIndex]));
        }

        public void Options()
        {
            ChangeState(new OptionsState(this, _graphics, Content, _themes[_styleIndex]));
        }

        public void ChangeTheme()
        {
            _styleIndex++;
            _styleIndex %= _themes.Count;
        }

        public void ChangeWordTheme()
        {
            for (int i = 0; i < _themes.Count; i++)
            {
                _themes[i].WordTheme++;
                if (_themes[i].WordTheme > 1)
                {
                    _themes[i].WordTheme = 0;
                }
            }
        }

        public Theme GetTheme()
        {
            return _themes[_styleIndex];
        }

        public void ExitGame()
        {
            this.Exit();
        }
        public Game1()
        {
            _themes = new List<Theme>();
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            WordTheme = 0;
            _player = new List<Texture2D>();
            _enemies = new List<Texture2D>();
            _words = new List<string>
            {
                "apple",       // A
                "banana",      // B
                "cherry",      // C
                "dragonfruit", // D
                "elderberry",  // E
                "fig",         // F
                "grape",       // G
                "honeydew",    // H
                "ivy",         // I
                "jasmine",     // J
                "kiwi",        // K
                "lemon",       // L
                "mango",       // M
                "nectarine",   // N
                "orange",      // O
                "papaya",      // P
                "quince",      // Q
                "raspberry",   // R
                "strawberry",  // S
                "tomato",      // T
                "ugli",        // U
                "vanilla",     // V
                "watermelon",  // W
                "xigua",       // X
                "yam",         // Y
                "zucchini"     // Z
            };
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("Arial");
            Texture2D temp = Content.Load<Texture2D>("soldier");
            _player.Add(temp);
            temp = Content.Load<Texture2D>("soldierShooting");
            _player.Add(temp);
            temp = Content.Load<Texture2D>("Enemy/skeleton-move_0");
            _enemies.Add(temp);
            temp = Content.Load<Texture2D>("Enemy/skeleton-move_1");
            _enemies.Add(temp);
            temp = Content.Load<Texture2D>("Enemy/skeleton-move_2");
            _enemies.Add(temp);
            temp = Content.Load<Texture2D>("Enemy/skeleton-move_3");
            _enemies.Add(temp);
            temp = Content.Load<Texture2D>("Enemy/skeleton-move_4");
            _enemies.Add(temp);
            temp = Content.Load<Texture2D>("Enemy/skeleton-move_5");
            _enemies.Add(temp);
            temp = Content.Load<Texture2D>("Enemy/skeleton-move_6"); 
            _enemies.Add(temp);
            temp = Content.Load<Texture2D>("Enemy/skeleton-move_7");
            _enemies.Add(temp);
            temp = Content.Load<Texture2D>("Enemy/skeleton-move_8");
            _enemies.Add(temp);
            temp = Content.Load<Texture2D>("Enemy/skeleton-move_9");
            _enemies.Add(temp);
            temp = Content.Load<Texture2D>("Enemy/skeleton-move_10");
            _enemies.Add(temp);
            temp = Content.Load<Texture2D>("Enemy/skeleton-move_11");
            _enemies.Add(temp);
            temp = Content.Load<Texture2D>("Enemy/skeleton-move_12");
            _enemies.Add(temp);
            temp = Content.Load<Texture2D>("Enemy/skeleton-move_13");
            _enemies.Add(temp);
            temp = Content.Load<Texture2D>("Enemy/skeleton-move_14");
            _enemies.Add(temp);
            temp = Content.Load<Texture2D>("Enemy/skeleton-move_15");
            _enemies.Add(temp);
            temp = Content.Load<Texture2D>("Enemy/skeleton-move_16");
            _enemies.Add(temp);
            _soundEffect = Content.Load<SoundEffect>("Audio/gunshot");
            _backgroundMusic = Content.Load<Song>("Audio/rain");
            _themes.Add(new Theme(_font, _words, _player, _enemies, _soundEffect, _backgroundMusic, WordTheme));
            _themes[0].ThemeName = "Default";
            //_player.Clear();
            //_enemies.Clear();
            var playerList = new List<Texture2D>();
            SpriteFont _font2 = Content.Load<SpriteFont>("yFont");
            temp = Content.Load<Texture2D>("wizzard");
            playerList.Add(temp);
            temp = Content.Load<Texture2D>("wizzardShooting");
            playerList.Add(temp);
            var enemiesList = new List<Texture2D>();
            temp = Content.Load<Texture2D>("Enemy/tile014");
            enemiesList.Add(temp);
            temp = Content.Load<Texture2D>("Enemy/tile015");
            enemiesList.Add(temp);
            temp = Content.Load<Texture2D>("Enemy/tile016");
            enemiesList.Add(temp);
            temp = Content.Load<Texture2D>("Enemy/tile017");
            enemiesList.Add(temp);
            temp = Content.Load<Texture2D>("Enemy/tile018");
            enemiesList.Add(temp);
            temp = Content.Load<Texture2D>("Enemy/tile019");
            enemiesList.Add(temp);
            temp = Content.Load<Texture2D>("Enemy/tile020");
            enemiesList.Add(temp);
            SoundEffect _soundEffect2 = Content.Load<SoundEffect>("Audio/wizzardShoot");
            Song _backgroundMusic2 = Content.Load<Song>("Audio/magicSong");
            var fantasyWords = new List<string>
            {
                "aether",
                "basilisk",
                "cryomancer",
                "djinn",
                "eldritch",
                "fey",
                "gorgon",
                "hydra",
                "ignivomous",
                "jinn",
                "kraken",
                "lich",
                "manticore",
                "necromancer",
                "orc",
                "phoenix",
                "quasit",
                "rune",
                "sylph",
                "troll",
                "undine",
                "valkyrie",
                "wraith",
                "xorn",
                "yggdrasil",
                "zephyr"
            };
            _themes.Add(new Theme(_font2, fantasyWords, playerList, enemiesList, _soundEffect2, _backgroundMusic2, WordTheme));
            _themes[1].ThemeName = "Fantasy";
            _currentState = new MenuState(this, _graphics, Content, _themes[_styleIndex]);
            DrawingHelper.Initialize(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
           
            {
                if (_nextState != null)
                {
                    _currentState = _nextState;
                    _nextState = null;
                }
                _currentState.Update();

                base.Update(gameTime);
            }
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _currentState.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
