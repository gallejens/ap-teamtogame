using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TeamTOGame.Classes
{
    public class LevelData
    {
        public PositionData start;
        public List<PlatformData> platforms;
        public List<EnemyData> enemies;
        public List<PositionData> carrots;
    }

    public class PlatformData
    {
        public PositionData position;
        public string texturename;
    }

    public class EnemyData
    {
        public string type;
        public PositionData start;
        public PositionData end;
        public string texturename;
    }

    public class PositionData
    {
        public int X;
        public int Y;
    }
}