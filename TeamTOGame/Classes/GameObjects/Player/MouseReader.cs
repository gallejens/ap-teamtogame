using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TeamTOGame.Interfaces;

namespace TeamTOGame.Classes
{
    class MouseReader : IMouseReader
    {
        private Vector2 mousePositionBeforePress = Vector2.Zero;
        public bool MousePressed { get; set; } = false;

        public Vector2 ReadInput()
        {
            MouseState mouseState = Mouse.GetState();
            Vector2 direction = Vector2.Zero;

            if (mouseState.LeftButton == ButtonState.Pressed && !MousePressed)
            {
                MousePressed = true;
                mousePositionBeforePress = new Vector2(mouseState.X, mouseState.Y);
            }
            else if (MousePressed) 
            {
                // only jump up
                if (mouseState.Y > mousePositionBeforePress.Y)
                {
                    float force = Vector2.Distance(new Vector2(mouseState.X, mouseState.Y), mousePositionBeforePress);
                    direction = Vector2.Normalize(-Vector2.Subtract(new Vector2(mouseState.X, mouseState.Y), mousePositionBeforePress)) * force;
                }
            }
            
            if (mouseState.LeftButton == ButtonState.Released)
            {
                MousePressed = false;
            }

            return direction;
        }
    }
}
