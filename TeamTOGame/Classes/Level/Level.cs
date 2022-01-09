using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TeamTOGame.Classes.GameObjects;
using TeamTOGame.Interfaces;

namespace TeamTOGame.Classes
{
    class Level : IDrawableObject
    {
        public Vector2 StartPosition { get; set; }

        public List<IUpdateableObject> Updateables { get; set; }
        public List<ICollidable> Platforms { get; set; }
        public List<ICollidable> Enemies { get; set; }
        public List<ICollidable> Carrots { get; set; }
        public List<IDrawableObject> Drawables { get; set; }

        public Level()
        {
            Updateables = new List<IUpdateableObject>();
            Platforms = new List<ICollidable>();
            Enemies = new List<ICollidable>();
            Carrots = new List<ICollidable>();
            Drawables = new List<IDrawableObject>();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IDrawableObject drawable in Drawables)
            {
                drawable.Draw(spriteBatch);
            }
        }
    }
}
