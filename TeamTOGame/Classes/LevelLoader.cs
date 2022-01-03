using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace TeamTOGame.Classes
{
    class LevelLoader
    {
        public Level Load(int levelIndex, Dictionary<string, Texture2D> textureList)
        {
            Level level = new Level();

            List<List<LevelObject>> leveldata = JsonConvert.DeserializeObject<List<List<LevelObject>>>(File.ReadAllText("levels.json"));
            foreach (LevelObject levelObject in leveldata[levelIndex]) // lets say 0 is selected level atm,
            {
                Platform platform = new Platform(levelObject.left, levelObject.top, levelObject.width, levelObject.height, textureList[levelObject.texturename]);
                level.GameObjects.Add(platform);
                level.Collidables.Add(platform);
            }

            return level;
        }
    }
}