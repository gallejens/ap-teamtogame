using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TeamTOGame.Interfaces;

namespace TeamTOGame.Classes
{
    class MovementManager
    {
        private GameTime previousGameTime = new GameTime(); 
        private float gravity = 0.004f;
        private float mouseForceMultiplier = 0.04f;

        public void Move(IMovable movable, GameTime gameTime, List<ICollidable> collidables)
        {
            ApplyGravity(movable, gameTime);
            CheckInput(movable);

            movable.Velocity += movable.Acceleration;

            Vector2 futurePosition = movable.Position + movable.Velocity;
            Rectangle futureCollisionBox = new Rectangle((int)futurePosition.X + movable.CollisionBox.X,
                (int)futurePosition.Y + movable.CollisionBox.Y, movable.CollisionBox.Width, movable.CollisionBox.Height);

            if (
                (futurePosition.X < (800 - 20)
                 && futurePosition.X > -1) &&
                (futurePosition.Y < 480 - 35
                 && futurePosition.Y > -1)
            )
            {
                bool hasCollided = false;
                foreach (ICollidable collidable in collidables)
                {
                    if (collidable.CheckCollision(futureCollisionBox))
                    {
                        hasCollided = true;
                        break;
                    }
                }

                if (!hasCollided)
                {
                    movable.Position += movable.Velocity;
                }
                else
                {
                    movable.Velocity = Vector2.Zero;
                }
            }
            else
            {
                movable.Velocity = Vector2.Zero;
            }
        }

        public void CheckInput(IMovable movable)
        {
            Vector2 force = movable.MouseInput.ReadInput();

            // if mouse wants to launch and not already moving
            if (force != Vector2.Zero && movable.Velocity == Vector2.Zero) //&& movable.Velocity == Vector2.Zero
            {
                movable.Velocity = force * mouseForceMultiplier;
            }
            else
            { // no mouse movement so we check keyboard
                int keyBoardDirection = movable.KeyboardInput.ReadInput();
                float direction = (float)keyBoardDirection * movable.WalkSpeed;

                Vector2 futurePosition = movable.Position + new Vector2(direction, 0);
                if (
                (futurePosition.X < (800 - 20)
                 && futurePosition.X > -1) &&
                (futurePosition.Y < 480 - 35
                 && futurePosition.Y > -1)
                )
                {
                    movable.Position += new Vector2(direction, 0);
                }
            }
        }

        private void ApplyGravity(IMovable movable, GameTime gameTime)
        {
            int deltaTime = (int)gameTime.ElapsedGameTime.TotalMilliseconds - (int)previousGameTime.ElapsedGameTime.TotalMilliseconds;
            movable.Acceleration += new Vector2(0, gravity * deltaTime);
            previousGameTime = gameTime;
        }
    }
}
