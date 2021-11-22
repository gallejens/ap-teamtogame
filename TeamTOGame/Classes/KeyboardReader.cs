using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TeamTOGame.Interfaces;

namespace TeamTOGame.Classes
{
    class KeyboardReader : IInputReader
    {
        public bool IsDestinationInput => false;

        public Vector2 ReadInput()
        {
            KeyboardState state = Keyboard.GetState();
            Vector2 direction = Vector2.Zero;

            if (state.IsKeyDown(Keys.Left))
            {
                direction.X -= 1;
            }

            if (state.IsKeyDown(Keys.Right))
            {
                direction.X += 1;
            }

            if (state.IsKeyDown(Keys.Up))
            {
                direction.Y -= 1;
            }

            if (state.IsKeyDown(Keys.Down))
            {
                direction.Y += 1;
            }

            return direction;
        }
    }
}
