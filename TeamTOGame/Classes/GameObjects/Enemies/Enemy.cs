using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TeamTOGame.Interfaces;

namespace TeamTOGame.Classes.GameObjects
{
    abstract class Enemy : IDrawableObject, ICollidable
    {
        public Vector2 Position { get; set; }
        public Texture2D texture;
        public Vector2 startPos;
        public Vector2 endPos;
        public Rectangle collisionBox;

        public Enemy(Vector2 startPos, Vector2 endPos, Texture2D texture)
        {
            Position = startPos;
            this.startPos = startPos;
            this.endPos = endPos;
            this.texture = texture;
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public bool CheckCollision(Rectangle rec)
        {
            return collisionBox.Intersects(rec);
        }
    }
}
