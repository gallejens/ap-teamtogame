using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TeamTOGame.Classes.GameObjects.Player;
using TeamTOGame.Interfaces;

namespace TeamTOGame.Classes
{
    class MovementManager
    {
        const float WALK_SPEED = 5f;
        const float GRAVITY = 0.035f;
        const float JUMP_FORCE = 0.07f;

        private GameTime previousGameTime = new GameTime();

        private IKeyboardReader keyboardReader;
        private IMouseReader mouseReader;

        public MovementManager(IKeyboardReader keyboardReader, IMouseReader mouseReader)
        {
            this.keyboardReader = keyboardReader;
            this.mouseReader = mouseReader;
        }

        public void Move(Character character, GameTime gameTime, List<ICollidable> platforms)
        {
            ApplyGravity(character, gameTime);
            CheckInput(character);
            CheckOutOfBounds(character);

            character.Velocity += character.Acceleration;

            Vector2 futurePosition = character.Position + character.Velocity;
            Rectangle futureCollisionBox = new Rectangle((int)futurePosition.X + character.CollisionBox.X,
                (int)futurePosition.Y + character.CollisionBox.Y, character.CollisionBox.Width, character.CollisionBox.Height);

            bool hasCollided = false;
            foreach (ICollidable platform in platforms)
            {
                if (platform.CheckCollision(futureCollisionBox))
                {
                    hasCollided = true;
                    character.Velocity = Vector2.Zero;
                    break;
                }
            }

            if (!hasCollided)
            {
                character.Position += character.Velocity;
            }
        }

        public void CheckInput(Character character)
        {
            if (character.Velocity == Vector2.Zero)
            {
                Vector2 force = mouseReader.ReadInput();

                if (mouseReader.MousePressed)
                {
                    character.State = CharacterState.Jumping;

                    if (force != Vector2.Zero)
                    {
                        JumpArch.GetInstance().Enabled = true;
                        JumpArch.GetInstance().SetArch(character.Position, force * JUMP_FORCE);
                    }
                }
                else
                {
                    JumpArch.GetInstance().Enabled = false;

                    if (force != Vector2.Zero)
                    {
                        character.Velocity = force * JUMP_FORCE;
                        character.State = CharacterState.Falling;
                    }
                    else
                    { // no mouse movement so we check keyboard
                        int keyBoardDirection = keyboardReader.ReadInput();
                        if (keyBoardDirection != 0)
                        {
                            float direction = (float)keyBoardDirection * WALK_SPEED;

                            Vector2 futurePosition = character.Position + new Vector2(direction, 0);
                            if ((futurePosition.X < (3840 - 20) && futurePosition.X > -1) && (futurePosition.Y < (2160 - 35) && futurePosition.Y > -1))
                            {
                                character.Position += new Vector2(direction, 0);

                                if (keyBoardDirection == -1)
                                {
                                    character.State = CharacterState.MovingLeft;
                                }
                                else if (keyBoardDirection == 1)
                                {
                                    character.State = CharacterState.MovingRight;
                                }
                            }
                        }
                        else if (!mouseReader.MousePressed && character.Velocity == Vector2.Zero)
                        {
                            character.State = CharacterState.Idle;
                        }
                    }
                }
            }
        }

        private void ApplyGravity(Character character, GameTime gameTime)
        {
            int deltaTime = (int)gameTime.ElapsedGameTime.TotalMilliseconds - (int)previousGameTime.ElapsedGameTime.TotalMilliseconds;
            character.Acceleration += new Vector2(0, GRAVITY * deltaTime);
            previousGameTime = gameTime;
        }

        private void CheckOutOfBounds(Character character)
        {
            Rectangle bounds = new Rectangle(-100, -1000, 4000, 4000);

            if (!bounds.Contains(character.Position))
            {
                character.Damage();            
            }
        }
    }
}
