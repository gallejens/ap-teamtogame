using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using TeamTOGame.Classes;

namespace TeamTOGame.Interfaces
{
    interface IMovable
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public float WalkSpeed { get; set; }
        public Rectangle CollisionBox { get; set; }
        public KeyboardReader KeyboardInput { get; }
        public MouseReader MouseInput { get; }
    }
}
