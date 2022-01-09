using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TeamTOGame.Classes;
using TeamTOGame.Classes.GameObjects;
using TeamTOGame.Interfaces;

namespace TeamTOGame
{
    class Character : IUpdateableObject, IDrawableObject
    {
        private Texture2D idleTexture;
        private Texture2D jumpingTexture;
        private Texture2D fallingTexture;
        private Texture2D movingLeftTexture;
        private Animation movingLeftAnimation;
        private Texture2D movingRightTexture;
        private Animation movingRightAnimation;

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }

        public List<ICollidable> Platforms { get; set; }
        public List<ICollidable> Enemies { get; set; }
        public List<ICollidable> Carrots { get; set; }
        public CharacterState State { get; set; } = CharacterState.Idle;

        private MovementManager movementManager = new MovementManager(new KeyboardReader(), new MouseReader());
        public HealthBar healthBar { get; set; }

        public event EventHandler<CarrotHitEventArgs> OnCarrotHit;

        private Vector2 startPos;

        public Character(Dictionary<string, Texture2D> textureList, Vector2 startPos)
        {
            idleTexture = textureList["bunnystand"];
            jumpingTexture = textureList["bunnyready"];
            fallingTexture = textureList["bunnyjump"];

            movingLeftTexture = textureList["bunnywalkleft"];
            movingLeftAnimation = new Animation();
            movingLeftAnimation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 120, 208)));
            movingLeftAnimation.AddFrame(new AnimationFrame(new Rectangle(120, 0, 120, 208)));

            movingRightTexture = textureList["bunnywalkright"];
            movingRightAnimation = new Animation();
            movingRightAnimation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 120, 208)));
            movingRightAnimation.AddFrame(new AnimationFrame(new Rectangle(120, 0, 120, 208)));

            this.startPos = startPos;
            Position = startPos;
            Velocity = new Vector2(0, 0);
            Acceleration = new Vector2(0, 0);
            CollisionBox = new Rectangle(0, 0, 120, 208);

            healthBar = new HealthBar(textureList["lifes"]);
        }

        public void Update(GameTime gameTime)
        { 
            movementManager.Move(this, gameTime, Platforms);

            if (State == CharacterState.MovingLeft)
            {
                movingLeftAnimation.Update(gameTime);
            }
            else if (State == CharacterState.MovingRight)
            {
                movingRightAnimation.Update(gameTime);
            }

            CheckEnemyCollision();
            CheckCarrotCollision();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (State)
            {
                case CharacterState.Idle:
                    spriteBatch.Draw(idleTexture, Position, new Rectangle(0, 0, 120, 208), Color.White);
                    break;
                case CharacterState.Jumping:
                    spriteBatch.Draw(jumpingTexture, Position, new Rectangle(0, 0, 120, 208), Color.White);
                    break;
                case CharacterState.Falling:
                    spriteBatch.Draw(fallingTexture, Position, new Rectangle(0, 0, 120, 208), Color.White); 
                    break;
                case CharacterState.MovingLeft:
                    spriteBatch.Draw(movingLeftTexture, Position, movingLeftAnimation.CurrentFrame.SourceRectangle, Color.White);
                    break;
                case CharacterState.MovingRight:
                    spriteBatch.Draw(movingRightTexture, Position, movingRightAnimation.CurrentFrame.SourceRectangle, Color.White);
                    break;
            }

            healthBar.Draw(spriteBatch);
        }

        public void Damage()
        {
            Position = startPos;
            Velocity = Vector2.Zero;

            healthBar.Decrease();
        }

        private void CheckEnemyCollision()
        {
            Rectangle currentCollision = new Rectangle((int)Position.X + CollisionBox.X, (int)Position.Y + CollisionBox.Y, CollisionBox.Width, CollisionBox.Height);

            foreach (ICollidable enemy in Enemies)
            {
                if (enemy.CheckCollision(currentCollision))
                {
                    Damage();
                }
            }
        }

        private void CheckCarrotCollision()
        {
            Rectangle currentCollision = new Rectangle((int)Position.X + CollisionBox.X, (int)Position.Y + CollisionBox.Y, CollisionBox.Width, CollisionBox.Height);

            foreach (ICollidable carrot in Carrots)
            {
                if (carrot.CheckCollision(currentCollision))
                {
                    Carrots.Remove(carrot);
                    CarrotHitEventArgs eventArgs = new CarrotHitEventArgs((Carrot)carrot);
                    OnCarrotHit?.Invoke(this, eventArgs);
                    break;
                }
            }
        }
    }

    public enum CharacterState
    {
        Idle,
        Jumping,
        Falling,
        MovingLeft,
        MovingRight
    }
}
