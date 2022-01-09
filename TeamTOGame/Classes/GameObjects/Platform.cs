using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamTOGame.Interfaces;

namespace TeamTOGame.Classes
{
    class Platform : ICollidable, IDrawableObject
    {
        private Vector2 position;
        private Rectangle collisionBox;
        private Texture2D texture;
        
        public Platform(Vector2 position, Texture2D texture)
        {
            this.position = position;
            collisionBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.texture = texture;
        }

        public bool CheckCollision(Rectangle rec)
        {
            return collisionBox.Intersects(rec);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, texture.Bounds, Color.White);
        }
    }
}
