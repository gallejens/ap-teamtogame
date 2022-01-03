using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TeamTOGame.Classes;
using TeamTOGame.Interfaces;


namespace TeamTOGame
{
    class Character : IGameObject, IMovable
    {
        private Texture2D texture;
        private Animation animation;
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public float WalkSpeed { get; set; }
        public Rectangle CollisionBox { get; set; }
        public KeyboardReader KeyboardInput { get; }
        public MouseReader MouseInput { get; }

        private MovementManager movementManager = new MovementManager(); 

        public Character(Texture2D texture)
        {
            this.texture = texture;
            KeyboardInput = new KeyboardReader();
            MouseInput = new MouseReader();
            animation = new Animation();
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 32, 32)));
            animation.AddFrame(new AnimationFrame(new Rectangle(32, 0, 32, 32)));
            animation.AddFrame(new AnimationFrame(new Rectangle(64, 0, 32, 32)));
            animation.AddFrame(new AnimationFrame(new Rectangle(96, 0, 32, 32)));

            Position = new Vector2(300, 100);
            Velocity = new Vector2(0, 0);
            Acceleration = new Vector2(0, 0);
            WalkSpeed = 1f;
            CollisionBox = new Rectangle(10, 24, 12, 8);
        }

        public void Update(GameTime gameTime, List<ICollidable> collidables)
        {
            Move(gameTime, collidables);
            animation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, animation.CurrentFrame.SourceRectangle, Color.White);
        }

        private void Move(GameTime gameTime, List<ICollidable> collidables)
        {
            movementManager.Move(this, gameTime, collidables);
        }
    }
}
