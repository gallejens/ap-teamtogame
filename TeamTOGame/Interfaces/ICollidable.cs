using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamTOGame.Interfaces
{
    interface ICollidable
    {
        bool CheckCollision(Rectangle rec);
    }
}
