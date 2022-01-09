using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TeamTOGame.Interfaces;

namespace TeamTOGame.Classes.GameObjects.Enemies
{
    class FlyingEnemy : Enemy, IUpdateableObject
    {
        private Animation animation;
        private float lerp = 0;
        const float SPEED = 0.005f;

        public FlyingEnemy(Vector2 startPos, Vector2 endPos, Texture2D texture) : base(startPos, endPos, texture)
        {
            animation = new Animation();
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 216, 126)));
            animation.AddFrame(new AnimationFrame(new Rectangle(216, 0, 216, 126)));
            animation.AddFrame(new AnimationFrame(new Rectangle(432, 0, 216, 126)));
            animation.AddFrame(new AnimationFrame(new Rectangle(648, 0, 216, 126)));
            animation.AddFrame(new AnimationFrame(new Rectangle(432, 0, 216, 126)));
            animation.AddFrame(new AnimationFrame(new Rectangle(216, 0, 216, 126)));
        }

        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
            Move();
            collisionBox = new Rectangle((int)Position.X, (int)Position.Y, 216, 126);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, animation.CurrentFrame.SourceRectangle, Color.White);
        }

        private void Move()
        {
            lerp += SPEED;
            Position = Vector2.Lerp(startPos, endPos, lerp);

            // switch points when at one of em
            if (lerp >= 1)
            {
                lerp = 0;
                Vector2 temp = startPos;
                startPos = endPos;
                endPos = temp;
            }
        }
    }
}
