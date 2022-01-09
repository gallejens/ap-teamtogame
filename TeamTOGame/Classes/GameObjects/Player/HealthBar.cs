using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TeamTOGame.Interfaces;

namespace TeamTOGame.Classes
{
    class HealthBar : IDrawableObject
    {
        public event EventHandler OnDeath;

        const int MAX_HEALTH = 3;
        public int Health { get; private set; }

        private Texture2D texture;

        public HealthBar(Texture2D texture)
        {
            Health = MAX_HEALTH;
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 1; i <= Health; i++)
            {
                spriteBatch.Draw(texture, new Vector2(100 * i, 100), texture.Bounds, Color.White);
            }
        }

        public void Decrease()
        {
            Health = Math.Clamp(Health - 1, 0, MAX_HEALTH);

            if (Health == 0) OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }
}
