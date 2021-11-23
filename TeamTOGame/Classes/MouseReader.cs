﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TeamTOGame.Interfaces;

namespace TeamTOGame.Classes
{
    class MouseReader 
    {
        private Vector2 mousePositionBeforePress = Vector2.Zero;
        private bool mousePressed = false;

        public Vector2 ReadInput()
        {
            MouseState mouseState = Mouse.GetState();
            Vector2 direction = Vector2.Zero;

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (!mousePressed)
                {
                    mousePressed = true;
                    mousePositionBeforePress = new Vector2(mouseState.X, mouseState.Y);
                }
            }
            else if (mouseState.LeftButton == ButtonState.Released && mousePressed)
            {
                mousePressed = false;

                // only jump up
                if (mouseState.Y > mousePositionBeforePress.Y)
                {
                    float force = Vector2.Distance(new Vector2(mouseState.X, mouseState.Y), mousePositionBeforePress);
                    direction = Vector2.Normalize(-Vector2.Subtract(new Vector2(mouseState.X, mouseState.Y), mousePositionBeforePress)) * force;
                }
            }

            return direction;
        }
    }
}
