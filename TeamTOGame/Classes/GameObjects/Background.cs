using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TeamTOGame.Interfaces;

namespace TeamTOGame.Classes
{
    class Background : IDrawableObject
    {
        private Texture2D texture;

        public Background(Texture2D texture)
        {
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(0f, 0f), new Rectangle(0, 0, 3840, 2160), Color.White);
        }
    }
}
