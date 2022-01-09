﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TeamTOGame.Classes.UI
{
    class GameFinishedMenu : Menu
    {
        public GameFinishedMenu(Dictionary<string, Texture2D> uiImagesList, Dictionary<string, SpriteFont> fontList)
        {
            Buttons["NEXT"] = new Button(uiImagesList["button"], fontList["ButtonFont"], "NEXT", middle - new Vector2(0, 200));
            Buttons["MENU"] = new Button(uiImagesList["button"], fontList["ButtonFont"], "MENU", middle + new Vector2(0, 200));
            header = new Text(middle - new Vector2(0f, 600), fontList["HeaderFont"], "LEVEL COMPLETED");
        }
    }
}
