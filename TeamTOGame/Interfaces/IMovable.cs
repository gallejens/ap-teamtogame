using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace TeamTOGame.Interfaces
{
    interface IMovable
    {
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public IInputReader InputReader { get; set; }
    }
}
