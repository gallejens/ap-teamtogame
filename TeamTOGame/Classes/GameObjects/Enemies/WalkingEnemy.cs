using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TeamTOGame.Interfaces;

namespace TeamTOGame.Classes.GameObjects.Enemies
{
    class WalkingEnemy : Enemy, IUpdateableObject
    {
        private Animation walkRightAnimation;
        private Animation walkLeftAnimation;
        private Animation currentAnimation;
        private bool walkingRight;

        private float lerp = 0;
        const float SPEED = 0.005f;

        public WalkingEnemy(Vector2 startPos, Vector2 endPos, Texture2D texture) : base(startPos, endPos, texture)
        {
            walkRightAnimation = new Animation();
            walkRightAnimation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 120, 159)));
            walkRightAnimation.AddFrame(new AnimationFrame(new Rectangle(120, 0, 120, 159)));
            walkLeftAnimation = new Animation();
            walkLeftAnimation.AddFrame(new AnimationFrame(new Rectangle(240, 0, 120, 159)));
            walkLeftAnimation.AddFrame(new AnimationFrame(new Rectangle(360, 0, 120, 159)));

            walkingRight = true;
            currentAnimation = walkRightAnimation;
        }

        public void Update(GameTime gameTime)
        {
            if (walkingRight)
            {
                currentAnimation = walkRightAnimation;
            }
            else
            {
                currentAnimation = walkLeftAnimation;
            }

            currentAnimation.Update(gameTime);
            Move();
            collisionBox = new Rectangle((int)Position.X, (int)Position.Y, 120, 159);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, currentAnimation.CurrentFrame.SourceRectangle, Color.White);
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
                walkingRight = !walkingRight;
            }
        }
    }
}
