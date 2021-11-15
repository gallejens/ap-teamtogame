using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using TeamTOGame.Interfaces;

namespace TeamTOGame.Classes
{
    class MovementManager
    {
        public void Move(IMovable movable)
        {
            Vector2 direction = movable.InputReader.ReadInput();

            if (movable.InputReader.IsDestinationInput)
            {
                movable.Position = direction;
            }
            else
            {
                Vector2 distance = direction * movable.Speed;
                movable.Position += distance;
            }
        }
    }
}
