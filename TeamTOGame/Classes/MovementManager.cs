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

        public void Move(IMovable movable, GameTime gameTime)
        {
            ApplyGravity(movable, gameTime);
            CheckInput(movable);

            movable.Velocity += movable.Acceleration;

            Vector2 futurePosition = movable.Position + movable.Velocity;
            if (
                (futurePosition.X < (800 - 180)
                 && futurePosition.X > -1) &&
                (futurePosition.Y < 480 - 247
                 && futurePosition.Y > -1)
            )
            {
                movable.Position += movable.Velocity;
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
            if (force != Vector2.Zero) //&& movable.Velocity == Vector2.Zero
            {
                movable.Velocity = force * mouseForceMultiplier;
            }
            else
            { // no mouse movement so we check keyboard
                Vector2 keyBoardDirection = movable.KeyboardInput.ReadInput();
                Vector2 direction = keyBoardDirection * movable.WalkSpeed;

                Vector2 futurePosition = movable.Position + direction;
                if (
                    (futurePosition.X < (800 - 180)
                     && futurePosition.X > -1) &&
                    (futurePosition.Y < 480 - 247
                     && futurePosition.Y > -1)
                )
                {
                    movable.Position += direction;
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
