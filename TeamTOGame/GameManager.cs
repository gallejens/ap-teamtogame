using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TeamTOGame.Classes;
using TeamTOGame.Classes.GameObjects;
using TeamTOGame.Classes.GameObjects.Player;
using TeamTOGame.Classes.UI;
using TeamTOGame.Interfaces;

namespace TeamTOGame
{
    public class GameManager : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Character character;
        private JumpArch jumpArch;
        private Background background;
        
        private Dictionary<string, Texture2D> textureList = new Dictionary<string, Texture2D>();
        private Dictionary<string, Texture2D> uiImagesList = new Dictionary<string, Texture2D>();
        private Dictionary<string, SpriteFont> fontList = new Dictionary<string, SpriteFont>();
        private SoundEffect backgroundMusic;

        private Level currentLevel;

        private Menu currentMenu;

        public GameState State { get; set; } = GameState.Menu;

        private int currentLevelIndex = 0;

        public GameManager()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080; 
            _graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            background = new Background(textureList["background"]);
            var bgMusicInstance = backgroundMusic.CreateInstance();
            bgMusicInstance.Volume = 0.1f;
            bgMusicInstance.Play();
            LoadMainMenu();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            textureList = LoadFolder<Texture2D>("textures");
            uiImagesList = LoadFolder<Texture2D>("UI");
            fontList = LoadFolder<SpriteFont>("Fonts");
            backgroundMusic = Content.Load<SoundEffect>("Audio/theme");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) LoadMainMenu();

            switch (State)
            {
                case GameState.Menu:
                    currentMenu.Update();
                    break;
                case GameState.InGame:
                    character.Update(gameTime);
                    foreach (IUpdateableObject updateable in currentLevel.Updateables)
                    {
                        updateable.Update(gameTime);
                    }
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Matrix.CreateScale(0.5f));

            background.Draw(_spriteBatch); // always draw background

            switch (State)
            {
                case GameState.Menu:
                    currentMenu.Draw(_spriteBatch);
                    break;
                case GameState.InGame:
                    currentLevel.Draw(_spriteBatch);
                    jumpArch.Draw(_spriteBatch);
                    character.Draw(_spriteBatch);
                    break;
                default:
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Load all files from the specified contentfolder
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="folder"></param>
        /// <returns></returns>
        private Dictionary<string, T> LoadFolder<T>(string folder)
        {
            Dictionary<string, T> dict = new Dictionary<string, T>();

            DirectoryInfo dir = new DirectoryInfo(Content.RootDirectory + "/" + folder);
            FileInfo[] files = dir.GetFiles("*.*");
            foreach (FileInfo file in files)
            {
                string key = Path.GetFileNameWithoutExtension(file.Name);
                dict[key] = Content.Load<T>(folder + "/" + key);
            }

            return dict;
        }

        /// <summary>
        /// Load level with index
        /// </summary>
        /// <param name="levelIndex"></param>
        private void LoadLevel(int levelIndex)
        {
            State = GameState.InGame;

            currentLevel = new LevelLoader().Load(levelIndex, textureList);

            jumpArch = JumpArch.GetInstance();
            jumpArch.DotTexture = textureList["jump_dot"];

            character = new Character(textureList, currentLevel.StartPosition);
            character.Platforms = currentLevel.Platforms;
            character.Enemies = currentLevel.Enemies;
            character.healthBar.OnDeath += e_GameOver;
            character.Carrots = currentLevel.Carrots;
            character.OnCarrotHit += e_RemoveCarrot;
        }

        // loading menus
        private void LoadMainMenu()
        {
            State = GameState.Menu;

            currentMenu = new MainMenu(uiImagesList, fontList);
            currentMenu.Buttons["START"].OnClick += e_StartClicked;
            currentMenu.Buttons["EXIT"].OnClick += e_ExitClicked;
        }

        private void LoadGameOverMenu()
        {
            State = GameState.Menu;

            currentMenu = new GameOverMenu(uiImagesList, fontList);
            currentMenu.Buttons["RETRY"].OnClick += e_RetryClicked;
            currentMenu.Buttons["MENU"].OnClick += e_MenuClicked;
        }

        private void LoadLevelCompletedMenu()
        {
            State = GameState.Menu;

            currentMenu = new GameFinishedMenu(uiImagesList, fontList);
            currentMenu.Buttons["NEXT"].OnClick += e_NextClicked;
            currentMenu.Buttons["MENU"].OnClick += e_MenuClicked;
        }

        // events
        private void e_StartClicked(object sender, EventArgs e) => LoadLevel(currentLevelIndex);

        private void e_ExitClicked(object sender, EventArgs e) => Exit();

        private void e_MenuClicked(object sender, EventArgs e) => LoadMainMenu();

        private void e_RetryClicked(object sender, EventArgs e) => LoadLevel(currentLevelIndex);

        private void e_NextClicked(object sender, EventArgs e)
        {
            currentLevelIndex++;
            LoadLevel(currentLevelIndex);
        }

        private void e_GameOver(object sender, EventArgs e) => LoadGameOverMenu();

        private void e_RemoveCarrot(object sender, CarrotHitEventArgs e)
        {
            currentLevel.Carrots.Remove(e.carrot);
            currentLevel.Drawables.Remove(e.carrot);
            
            if (currentLevel.Carrots.Count == 0)
            {
                LoadLevelCompletedMenu();
            }
        }
    }

    public enum GameState
    {
        Menu,
        InGame,
    }
}
