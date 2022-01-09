using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using TeamTOGame.Classes.GameObjects;
using TeamTOGame.Classes.GameObjects.Enemies;

namespace TeamTOGame.Classes
{
    class LevelLoader
    {
        public Level Load(int levelIndex, Dictionary<string, Texture2D> textureList)
        {
            Level level = new Level();

            LevelData levelData = JsonConvert.DeserializeObject<List<LevelData>>(File.ReadAllText("levels.json"))[levelIndex];

            level.StartPosition = new Vector2(levelData.start.X, levelData.start.Y);

            foreach (PlatformData platformData in levelData.platforms)
            {
                Vector2 position = new Vector2(platformData.position.X, platformData.position.Y);
                Platform platform = new Platform(position, textureList[platformData.texturename]);
                level.Drawables.Add(platform);
                level.Platforms.Add(platform);
            }

            foreach (EnemyData enemyData in levelData.enemies)
            {
                Vector2 startPos = new Vector2(enemyData.start.X, enemyData.start.Y);
                Vector2 endPos = new Vector2(enemyData.end.X, enemyData.end.Y);
                Enemy enemy = null;
                switch (enemyData.type) 
                {
                    case "flying":
                        enemy = new FlyingEnemy(startPos, endPos, textureList[enemyData.texturename]);
                        level.Updateables.Add(enemy as FlyingEnemy);
                        break;
                    case "walking":
                        enemy = new WalkingEnemy(startPos, endPos, textureList[enemyData.texturename]);
                        level.Updateables.Add(enemy as WalkingEnemy);
                        break;
                    case "stationary":
                        enemy = new StationaryEnemy(startPos, endPos, textureList[enemyData.texturename]);
                        break;
                    default:
                        break;
                }
                level.Drawables.Add(enemy);
                level.Enemies.Add(enemy);
            }

            foreach (PositionData positionData in levelData.carrots)
            {
                Vector2 position = new Vector2(positionData.X, positionData.Y);
                Carrot carrot = new Carrot(position, textureList["carrots"]);
                level.Drawables.Add(carrot);
                level.Carrots.Add(carrot);
            }

            return level;
        }
    }
}