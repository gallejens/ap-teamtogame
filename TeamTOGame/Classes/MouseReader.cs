using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TeamTOGame.Interfaces;

namespace TeamTOGame.Classes
{
    class MouseReader : IInputReader
    {
        public bool IsDestinationInput => true;

        public Vector2 ReadInput()
        {
            MouseState state = Mouse.GetState();
            Vector2 mousePosition = new Vector2(state.X, state.Y);
            return mousePosition;
        }
    }
}
