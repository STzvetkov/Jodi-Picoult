#region Using Statements

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PirateGame.Ship;

#endregion

namespace PirateGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameClass : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private static readonly int WINDOW_WIDTH = 800;
        private static readonly int WINDOW_HEIGHT = 600;
        private KeyboardState oldKBState;
        private PlayerShip playerShip;
        
        //temporary
        private Texture2D waterMapTexture;
        private Rectangle watermapRect;
        private Texture2D con1Tex;
        private Rectangle con1rect;
        private Menu mainMenu;
        private bool MainMenuIsOn;
        //temporary

        public GameClass() : base()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
            this.graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            this.graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;

            this.IsMouseVisible = true;

            mainMenu = new Menu(this, "Pirate Menu", this.OnToggleMainMenu);
            
            mainMenu.Visible = false;
            mainMenu.Enabled = false;
            this.MainMenuIsOn = false;

            this.Components.Add(mainMenu);
        }
        
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.Services.AddService(typeof(GraphicsDeviceManager), graphics);

            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            this.Services.AddService(typeof(SpriteBatch), spriteBatch);

            oldKBState = Keyboard.GetState();

            mainMenu.MenuItems.Add(new MenuItem("Play", OnUnimplementedHandler));
            mainMenu.MenuItems.Add(new MenuItem("Quit", OnExit));

            base.Initialize();
        }
        
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //temporary
            this.waterMapTexture = this.Content.Load<Texture2D>("waterMap");
            this.watermapRect = new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);
            this.con1Tex = this.Content.Load<Texture2D>("con1");
            this.con1rect = new Rectangle(200,100,400,400);
            //temporary


            this.playerShip = new PlayerShip(this.Content,"pirate_ship",WINDOW_WIDTH / 2,WINDOW_HEIGHT / 2);
        }
        
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {

        }
        
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState newKBState = Keyboard.GetState();


            if (newKBState.IsKeyDown(Keys.Escape) && this.oldKBState.IsKeyUp(Keys.Escape))   // Toggle main menu
            {
                this.OnToggleMainMenu(this.mainMenu, null);
            }

            if (this.MainMenuIsOn == false)                 // Process the input if the menu is off
            {
                if (newKBState.IsKeyDown(Keys.Up) && this.oldKBState.IsKeyUp(Keys.Up))
                {
                    this.playerShip.Move(Keys.Up, WINDOW_WIDTH, WINDOW_HEIGHT);
                }
                else if (newKBState.IsKeyDown(Keys.Down) && this.oldKBState.IsKeyUp(Keys.Down))
                {
                    this.playerShip.Move(Keys.Down, WINDOW_WIDTH, WINDOW_HEIGHT);
                }
                else if (newKBState.IsKeyDown(Keys.Left) && this.oldKBState.IsKeyUp(Keys.Left))
                {
                    this.playerShip.Move(Keys.Left, WINDOW_WIDTH, WINDOW_HEIGHT);
                }
                else if (newKBState.IsKeyDown(Keys.Right) && this.oldKBState.IsKeyUp(Keys.Right))
                {
                    this.playerShip.Move(Keys.Right, WINDOW_WIDTH, WINDOW_HEIGHT);
                }
            }

            this.oldKBState = newKBState;

            base.Update(gameTime);
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            
            this.spriteBatch.Begin();
            this.spriteBatch.Draw(this.waterMapTexture, this.watermapRect, Color.White);
            this.spriteBatch.Draw(this.con1Tex, this.con1rect, Color.White);
            this.playerShip.Draw(this.spriteBatch);
            
            this.spriteBatch.End();
            
            base.Draw(gameTime);
        }

        /// <summary>
        /// Toggles the main menu on and off
        /// </summary>
        /// <param name="menu">Menu object</param>
        /// <param name="e">Not used</param>
        public void OnToggleMainMenu(object menu, EventArgs e = null)
        {
            ((Menu)menu).Toggle();

            // All other DrawableGameComponent.Enabled properties should be disabled here
            // when the main menu is on
            this.MainMenuIsOn = !this.MainMenuIsOn;
        }

        public void OnExit(object menu, EventArgs e = null)
        {
            this.Exit();
        }

        public void OnUnimplementedHandler(object menu, EventArgs e = null)
        {
            throw new System.NotImplementedException();
        }
    }
}