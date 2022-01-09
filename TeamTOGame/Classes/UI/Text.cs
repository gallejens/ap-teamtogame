using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeamTOGame.Classes.UI
{
    class Text
    {
        private Vector2 position;
        private SpriteFont font;
        private string text;

        public Text(Vector2 position, SpriteFont font, string text)
        {
            this.position = position - font.MeasureString(text) / 2;
            this.font = font;
            this.text = text;
        }

        public void Draw(SpriteBatch spriteBatch)
        { 
            spriteBatch.DrawString(font, text, position, Color.Black);
        }
    }
}
