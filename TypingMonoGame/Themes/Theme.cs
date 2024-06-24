using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace TypingMonoGame.Themes
{
    public record Theme(
        SpriteFont Font,
        List<string> Words,
        List<Texture2D> Player,
        List<Texture2D> Enemy,
        SoundEffect Shoot,
        Song BackgroundMusic,
        int WordTheme)
    {
        public SpriteFont Font { get; set; } = Font;
        public List<string> Words { get; set; } = Words;
        public List<Texture2D> Player { get; set; } = Player;
        public List<Texture2D> Enemy { get; set; } = Enemy;
        public SoundEffect Shoot { get; set; } = Shoot;
        public Song BackgroundMusic { get; set; } = BackgroundMusic;
        public string ThemeName { get; set; }
        public int WordTheme { get; set; } = WordTheme;
    }
}
