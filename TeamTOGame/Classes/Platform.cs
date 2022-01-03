using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamTOGame.Interfaces;

namespace TeamTOGame.Classes
{
    class Platform : ICollidable, IGameObject
    {
        private Rectangle collisionBox;
        private Texture2D texture;
        private Vector2 position;

        public Platform(int left, int top, int width, int height, Texture2D texture)
        {
            collisionBox = new Rectangle(left, top, width, height);
            position = new Vector2(left, top);
            this.texture = texture;
        }

        public bool CheckCollision(Rectangle rec)
        {
            return collisionBox.Intersects(rec);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Rectangle(0, 0, 70, 70), Color.White);
        }

        public void Update(GameTime gameTime, List<ICollidable> collidables)
        {

        }
    }
}
