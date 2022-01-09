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
        public int ReadInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                return -1;
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                return 1;
            }

            return 0;
        }
    }
}
