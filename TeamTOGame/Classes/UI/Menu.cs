using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeamTOGame.Classes.UI
{
    class Menu
    {
        public Vector2 middle { get; private set; } = new Vector2(1920, 1080);
        public Dictionary<string, Button> Buttons { get; set; }
        public Text header;

        public Menu()
        {
            Buttons = new Dictionary<string, Button>();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            header.Draw(spriteBatch);
            foreach (var keyValuePair in Buttons)
            {
                keyValuePair.Value.Draw(spriteBatch);
            }
        }

        public void Update()
        {
            foreach (var keyValuePair in Buttons)
            {
                keyValuePair.Value.Update();
            }
        }
    }
}
