using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeamTOGame.Interfaces
{
    interface IMouseReader
    {
        public bool MousePressed { get; set; }
        public Vector2 ReadInput();
    }
}
