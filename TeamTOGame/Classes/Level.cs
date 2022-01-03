using System;
using System.Collections.Generic;
using System.Text;
using TeamTOGame.Interfaces;

namespace TeamTOGame.Classes
{
    public class Level
    {
        public List<IGameObject> GameObjects { get; set; }
        public List<ICollidable> Collidables { get; set; }

        public Level()
        {
            GameObjects = new List<IGameObject>();
            Collidables = new List<ICollidable>();
        }
    }
}
