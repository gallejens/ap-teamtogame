using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamTOGame.Interfaces;

namespace TeamTOGame.Classes.GameObjects
{
    class Carrot : ICollidable, IDrawableObject
    {
        private Vector2 position;
        private Vector2 mid;
        private Texture2D texture;

        public Carrot(Vector2 position, Texture2D texture)
        {
            this.position = position;
            this.mid = this.position + new Vector2(texture.Width, texture.Height) / 2;
            this.texture = texture;
        }

        public bool CheckCollision(Rectangle rec)
        {
            return rec.Contains(mid);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, texture.Bounds, Color.White);
        }
    }

    class CarrotHitEventArgs : EventArgs
    {
        public Carrot carrot { get; private set; }

        public CarrotHitEventArgs(Carrot carrot)
        {
            this.carrot = carrot;
        }
    }
}
