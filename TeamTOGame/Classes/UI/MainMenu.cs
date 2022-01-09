using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TeamTOGame.Classes.UI
{
    class MainMenu : Menu
    {
        public MainMenu(Dictionary<string, Texture2D> uiImagesList, Dictionary<string, SpriteFont> fontList)
        {
            Buttons["START"] = new Button(uiImagesList["button"], fontList["ButtonFont"], "START", middle - new Vector2(0, 200));
            Buttons["EXIT"] = new Button(uiImagesList["button"], fontList["ButtonFont"], "EXIT", middle + new Vector2(0, 200));
            header = new Text(middle - new Vector2(0f, 600), fontList["HeaderFont"], "MAIN MENU");
        }
    }
}
