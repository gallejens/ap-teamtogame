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
        public Vector2 Speed { get; set; }
        public IInputReader InputReader { get; set; }

        private MovementManager movementManager = new MovementManager(); 

        public Character(Texture2D texture, IInputReader inputReader)
        {
            this.texture = texture;
            InputReader = inputReader;
            animation = new Animation();
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 32, 32)));
            animation.AddFrame(new AnimationFrame(new Rectangle(32, 0, 32, 32)));
            animation.AddFrame(new AnimationFrame(new Rectangle(64, 0, 32, 32)));
            animation.AddFrame(new AnimationFrame(new Rectangle(96, 0, 32, 32)));

            Position = new Vector2(0, 0);
            Debug.WriteLine(Position);
            Speed = new Vector2(1, 1);
        }

        public void Update(GameTime gameTime)
        {
            
            Move();
            animation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, animation.CurrentFrame.SourceRectangle, Color.White);
        }

        private void Move()
        {
            movementManager.Move(this);
        }
    }
}
