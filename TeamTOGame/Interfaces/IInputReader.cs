using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace TeamTOGame.Interfaces
{
    interface IInputReader
    {
        Vector2 ReadInput();
        public bool IsDestinationInput { get; }
    }

}
