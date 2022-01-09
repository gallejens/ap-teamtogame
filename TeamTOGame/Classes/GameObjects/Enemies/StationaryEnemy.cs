using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeamTOGame.Classes.GameObjects
{
    class StationaryEnemy : Enemy
    {
        public StationaryEnemy(Vector2 startPos, Vector2 endPos, Texture2D texture) : base(startPos, endPos, texture)
        {
            collisionBox = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, texture.Bounds, Color.White);
        }
    }
}
