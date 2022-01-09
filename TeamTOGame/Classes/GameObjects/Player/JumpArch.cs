using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TeamTOGame.Interfaces;

namespace TeamTOGame.Classes.GameObjects.Player
{
    class JumpArch : IDrawableObject
    {
        private static JumpArch instance;

        const float GRAVITY = 0.035f;
        const int DOT_AMOUNT = 6;

        public Texture2D DotTexture { get; set; }

        private Vector2 force;
        private Vector2 startPos;

        public bool Enabled { get; set; } = false;

        public static JumpArch GetInstance()
        {
            if (instance == null) instance = new JumpArch();
            return instance;
        }

        public void SetArch(Vector2 startPos, Vector2 force)
        {
            this.startPos = startPos;
            this.force = force;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Enabled)
            {
                Vector2 position = startPos + new Vector2(60, 0);
                Vector2 velocity = force*4;
                for (int i = 100; i <= DOT_AMOUNT * 100; i += 100)
                {
                    velocity += new Vector2(0, GRAVITY) * i;
                    position += velocity;
                    spriteBatch.Draw(DotTexture, position, DotTexture.Bounds, Color.White);
                }
            }
        }
    }
}
