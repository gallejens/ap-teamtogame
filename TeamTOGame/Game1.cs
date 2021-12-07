using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TeamTOGame.Classes;
using TeamTOGame.Interfaces;

namespace TeamTOGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Character character;
        private Background background;


        private Texture2D slimeTexture;
        private Texture2D backgroundTile;
        


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            character = new Character(slimeTexture);
            background = new Background(backgroundTile);
            background.Initialize();
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            character.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            background.Draw(_spriteBatch);
            character.Draw(_spriteBatch);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
