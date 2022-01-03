using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using SharpDX.Direct3D11;
using TeamTOGame.Classes;
using TeamTOGame.Interfaces;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;

namespace TeamTOGame
{
    public class Game1 : Game
    {
        private SpriteFont font;
        private int score = 0;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private LevelLoader levelLoader;

        private Character character;
        private Background background;


        private Texture2D slimeTexture;
        private Texture2D platformTexture;
        private Texture2D backgroundTile;
        

        private Dictionary<string, Texture2D> textureList = new Dictionary<string, Texture2D>();

        private Level currentLevel;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            // Turn this on to set game to fullscreen.
            // _graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            levelLoader = new LevelLoader();

            currentLevel = levelLoader.Load(0, textureList);
            character = new Character(slimeTexture);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            backgroundTile = Content.Load<Texture2D>("background");
            slimeTexture = Content.Load<Texture2D>("slimespritesheet");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            foreach (IGameObject gameObject in currentLevel.GameObjects)
            {
                gameObject.Update(gameTime, currentLevel.Collidables);
            }

            base.Update(gameTime);
            score++;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            character.Draw(_spriteBatch);
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
