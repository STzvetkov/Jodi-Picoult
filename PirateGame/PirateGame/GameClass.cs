
#region Using Statements

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PirateGame.MapObjects;
using PirateGame.Popups;
using PirateGame.Ship;
using PirateGame.UserInterface;

#endregion

namespace PirateGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameClass : Game
    {
        private Texture2D waterMapTexture;
        private Rectangle watermapRect;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private KeyboardState oldKBState;
        private PlayerShip playerShip;
        private Continent continent1;
        private Continent continent2;
        private Continent continent3;
        private List<PirateGame.Interfaces.IDrawableCustom> continents;
        List<string> messages;
        private Popup p;

        //Menu system
        private Menu mainMenu;
        private int openMenusCount;

        public GameClass() : base()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
            this.graphics.PreferredBackBufferWidth = GlobalConstants.WINDOW_WIDTH;
            this.graphics.PreferredBackBufferHeight = GlobalConstants.WINDOW_HEIGHT;
            
            this.IsMouseVisible = true;
            
            this.mainMenu = new Menu(this, "Pirate Menu", this.OnOpenMenu, this.OnCloseMenu);

            this.openMenusCount = 0;
        }

        /// <summary>
        /// Calculates if the game engine is allowed to run
        /// </summary>
        private bool EnableGameProcessing
        {
            get
            {
                if (this.openMenusCount == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.Services.AddService(typeof(GraphicsDeviceManager), this.graphics);
            
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            this.Services.AddService(typeof(SpriteBatch), this.spriteBatch);
            
            this.oldKBState = Keyboard.GetState();

            this.mainMenu.Items.Add(new SelectableItem<string>("Play", this.OnPlay));
            this.mainMenu.Items.Add(new SelectableItem<string>("Test Inventory", this.OnInventoryTest));
            this.mainMenu.Items.Add(new SelectableItem<string>("Quit", this.OnExit));
            base.Initialize();
        }
        
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            this.waterMapTexture = this.Content.Load<Texture2D>("waterMap");
            this.watermapRect = new Rectangle(0, 0, GlobalConstants.WINDOW_WIDTH, GlobalConstants.WINDOW_HEIGHT);
            this.continent1 = new Continent(this.Content,"con1",500,100,250,250);
            this.continent2 = new Continent(this.Content, "con2", 40, 40, 300, 300);
            this.continent3 = new Continent(this.Content, "con3", 40, 300, 300, 300);
            this.playerShip = new PlayerShip(this.Content, "pirate_ship", GlobalConstants.WINDOW_WIDTH / 2, GlobalConstants.WINDOW_HEIGHT / 2);
            this.continents = new List<PirateGame.Interfaces.IDrawableCustom>
            {
                this.continent1,
                this.continent2,
                this.continent3,
            };
            messages = new List<string>
                {
                    "Hull:" + playerShip.Hull,
                    "Weapons:"+playerShip.Weapons
                };
            p = new Popup(this.Content, "ship_popup", "Arial", messages, playerShip);


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

            if (newKBState.IsKeyDown(Keys.Escape) && this.oldKBState.IsKeyUp(Keys.Escape))  // Open main menu
            {
                this.mainMenu.Show();
            }

            if (this.EnableGameProcessing)                  // Process the game logic if all menus are off
            {
                if (newKBState.IsKeyDown(Keys.Up))
                {
                    this.playerShip.Move(Keys.Up, this.continents);
                }
                else if (newKBState.IsKeyDown(Keys.Down))
                {
                    this.playerShip.Move(Keys.Down, this.continents);
                }
                else if (newKBState.IsKeyDown(Keys.Left))
                {
                    this.playerShip.Move(Keys.Left, this.continents);
                }
                else if (newKBState.IsKeyDown(Keys.Right))
                {
                    this.playerShip.Move(Keys.Right, this.continents);
                }

                bool mouseOverShip = playerShip.Rectangle.Contains(Mouse.GetState().X, Mouse.GetState().Y);

                if (mouseOverShip || newKBState.IsKeyDown(Keys.LeftControl))
                {
                    p.IsVisible = true;
                }
                else
                {
                    p.IsVisible = false;
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
            this.continent1.Draw(this.spriteBatch);
            this.continent2.Draw(this.spriteBatch);
            this.continent3.Draw(this.spriteBatch);
            this.playerShip.Draw(this.spriteBatch);
            this.p.Draw(this.spriteBatch);
            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Open menu post processing
        /// </summary>
        /// <param name="menu">Menu</param>
        /// <param name="e">Not used</param>
        public void OnOpenMenu(object menu, EventArgs e = null)
        {
            this.openMenusCount += 1;
        }

        /// <summary>
        /// Close menu post processing
        /// </summary>
        /// <param name="menu">Menu</param>
        /// <param name="e">Not used</param>
        public void OnCloseMenu(object menu, EventArgs e = null)
        {
            this.openMenusCount -= 1;
        }

        /// <summary>
        /// Exit the game command
        /// </summary>
        /// <param name="menuItem">Menu item</param>
        /// <param name="e">Not used</param>
        public void OnExit(object menuItem, EventArgs e = null)
        {
            this.Exit();
        }

        /// <summary>
        /// Continue the game
        /// </summary>
        /// <param name="menuItem">Menu item</param>
        /// <param name="e">Not used</param>
        public void OnPlay(object menuItem, EventArgs e = null)
        {
            this.mainMenu.Hide();
        }

        private void OnInventoryTest(object menuItem, EventArgs e = null)
        {
            this.mainMenu.Hide();

            Inventory test = new Inventory(this);

            // TODO: remove the test
            test.Items.Add(new SelectableItem<Interfaces.IDrawableCustom>(this.continent1, OnExit));
            test.Items.Add(new SelectableItem<Interfaces.IDrawableCustom>(this.continent2, null));
            test.Items.Add(new SelectableItem<Interfaces.IDrawableCustom>(this.continent3, OnExit));
            test.Items.Add(new SelectableItem<Interfaces.IDrawableCustom>(this.continent1, null));
            test.Items.Add(new SelectableItem<Interfaces.IDrawableCustom>(this.continent1, null));

            test.Show();
        }

    }
}