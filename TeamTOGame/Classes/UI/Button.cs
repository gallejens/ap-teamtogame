using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TeamTOGame.Interfaces;

namespace TeamTOGame.Classes.UI
{
    class Button : IDrawableObject
    {
        public event EventHandler OnClick;

        private Texture2D texture;
        private SpriteFont font;
        private string text;
        private Vector2 position;

        private bool clicked = true;

        public Button(Texture2D texture, SpriteFont font, string text, Vector2 position)
        {
            this.texture = texture;
            this.font = font;
            this.text = text;
            this.position = position - new Vector2(texture.Width / 2, 0);
        }

        public void Update()
        {
            MouseState mouseState = Mouse.GetState();

            if (OnButton() && mouseState.LeftButton == ButtonState.Pressed && !clicked)
            {
                clicked = true;
                OnClick?.Invoke(this, EventArgs.Empty);
            }

            if (mouseState.LeftButton == ButtonState.Released) clicked = false; 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height), Color.White);
            Vector2 textPos = position + new Vector2(texture.Width / 2, texture.Height / 2) - font.MeasureString(text) / 2;
            spriteBatch.DrawString(font, text, textPos, Color.Black);
        }

        private bool OnButton()
        {
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePos = new Vector2(mouseState.X * 2, mouseState.Y * 2);
            return mousePos.X < position.X + texture.Width && mousePos.X > position.X && mousePos.Y < position.Y + texture.Height && mousePos.Y > position.Y;
        }
    }
}
