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
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private LevelLoader levelLoader;

        private Character character;
        private Texture2D slimeTexture;
        private Texture2D platformTexture;

        private Dictionary<string, Texture2D> textureList = new Dictionary<string, Texture2D>();

        private Level currentLevel;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            levelLoader = new LevelLoader();

            currentLevel = levelLoader.Load(0, textureList);
            character = new Character(slimeTexture);
            currentLevel.GameObjects.Add(character);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            slimeTexture = Content.Load<Texture2D>("slimespritesheet");
            platformTexture = Content.Load<Texture2D>("grasssprite");
            textureList.Add("grasssprite", platformTexture);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            foreach (IGameObject gameObject in currentLevel.GameObjects)
            {
                gameObject.Update(gameTime, currentLevel.Collidables);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            foreach (IGameObject gameObject in currentLevel.GameObjects)
            {
                gameObject.Draw(_spriteBatch);
            }

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
