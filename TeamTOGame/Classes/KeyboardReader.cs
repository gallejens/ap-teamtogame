using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Text;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TeamTOGame.Interfaces;

namespace TeamTOGame.Classes
{
    class KeyboardReader
    {
        public Vector2 ReadInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            Vector2 direction = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                direction.X -= 1;
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                direction.X += 1;
            }

            return direction;
        }
    }
}
