using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TypingMonoGame.Entities
{
    public class IsTyped
    {
        public string Text { get; set; }
        protected bool[] Typed;
        protected int Iterator = 0;
        public Vector2 Position { get; set; }
        public SpriteFont Font { get; set; }
        protected bool IsFunctional = false;
        public bool IsCurrent { get; set; }
        public KeyboardState CurrentKeyState { get; set; }

        public IsTyped(string text, SpriteFont font, Vector2 position)
        {
            Text = text.ToLower();
            Font = font;
            Position = position;
            Typed = new bool[text.Length];
            for (int i = 0; i < Typed.Length; i++)
            {
                Typed[i] = false;
            }
        }

        public bool UpdateTyped(char text)
        {
            if (Iterator < Text.Length && Text[Iterator] == text)
            {
                Typed[Iterator] = true;
                return true;
            }

            return false;
        }

        public void ResetIterator()
        {
            Iterator = 0;
        }

        public int GetIterator()
        {
            return Iterator;
        }

        public void Update()
        {
            var keyboardState = Keyboard.GetState();
            if (CurrentKeyState != keyboardState)
            {
                foreach (var key in keyboardState.GetPressedKeys())
                {
                    if (key == Keys.Back && IsFunctional)
                    {
                        if (Iterator > 0)
                        {
                            Iterator--;
                            Typed[Iterator] = false;
                        }
                    }
                    else if (Iterator < Text.Length)
                    {
                        var keyValue = key.ToString().ToLower();
                        if (keyValue.Length == 1 && UpdateTyped(keyValue[0]))
                        {
                            Iterator++;
                        }
                    }

                    break;
                }
            }
            CurrentKeyState = keyboardState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 drawPosition = Position;
            for (int i = 0; i < Text.Length; i++)
            {
                Color color = Typed[i] ? Color.Green : Color.White;
                spriteBatch.DrawString(Font, Text[i].ToString(), drawPosition, color);
                drawPosition.X += Font.MeasureString(Text[i].ToString()).X;
            }
        }
    }
}