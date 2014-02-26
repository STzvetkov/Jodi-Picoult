
#region Using Statements

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PirateGame.Enums;
using PirateGame.Interfaces;
using PirateGame.MapObjects;
using PirateGame.Popups;
using PirateGame.Ship;
using PirateGame.UserInterface;

#endregion

namespace PirateGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>con1
    public class GameClass : Game
    {
        private GameState gameState;
        private Texture2D waterMapTexture;
        private Rectangle watermapRect;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private KeyboardState oldKBState;
        private PlayerShip playerShip;
        private Continent continent1;
        private Continent continent2;
        private Continent continent3;
        private FishingVillage fishingVillage1;
        private TradeCenter tradeCenter1;
        private List<PirateGame.Interfaces.IDrawableCustom> continents;
        private List<string> messages;
        private Popup p;
        private KeyboardState newKBState;
        private bool flag = false;
        private List<NpcShip> npcs;
        private SpriteFont gameFont;
        private HealthBarr healthBar;
        
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
            this.gameFont = Content.Load<SpriteFont>("Arial");
            this.waterMapTexture = this.Content.Load<Texture2D>("waterMap");
            this.watermapRect = new Rectangle(0, 0, GlobalConstants.WINDOW_WIDTH, GlobalConstants.WINDOW_HEIGHT);
            this.continent1 = new Continent(this.Content,"con1",500,100,250,250);
            this.continent2 = new Continent(this.Content, "con2", 40, 40, 300, 300);
            this.continent3 = new Continent(this.Content, "con3", 40, 300, 300, 300);
            this.playerShip = new PlayerShip(this.Content, "pirate_ship", GlobalConstants.WINDOW_WIDTH / 2, GlobalConstants.WINDOW_HEIGHT / 2);
            healthBar = new HealthBarr(Content);
            this.npcs = new List<NpcShip>
            {
                new NpcShip(this.Content,"pirate_ship_npc1",300,200),
                new NpcShip(this.Content,"pirate_ship_npc1",500,300),
                new NpcShip(this.Content,"pirate_ship_npc1",400,400)
            };
            // add fishing village
            this.fishingVillage1 = new FishingVillage(2, 200, 10, 10000, 150, 1000, 30, Coutries.Tanzania,
                                                      this.Content, "fishing_village", 500, 100, 100, 100);
            // add trade center
            this.tradeCenter1 = new TradeCenter(5000, 50, 100000, 800, 6000, 50, Coutries.Yemen,
                                                this.Content, "trade_center", 600, 600, 128, 128);

            this.continents = new List<PirateGame.Interfaces.IDrawableCustom>
            {
                this.continent1,
                this.continent2,
                this.continent3,
            };
            this.messages = new List<string>
            {
                string.Format("Hull:{0}", this.playerShip.Hull),
                string.Format("Weapons:{0}", this.playerShip.Weapons),
                string.Format("Hit Points:{0}", this.playerShip.Hitpoints),
                string.Format("Damage:{0}",this.playerShip.Damage)
            };
            this.p = new Popup(this.Content, "ship_popup", "Arial", this.messages, this.playerShip);
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
            switch (this.gameState)
            {
                
                case GameState.FreeRoam:
                    this.newKBState = Keyboard.GetState();

                    this.playerShip.ResetHitpoints();

                    bool mouseOverShip = this.playerShip.Rectangle.Contains(Mouse.GetState().X, Mouse.GetState().Y);
                    
                    if (mouseOverShip || this.newKBState.IsKeyDown(Keys.LeftControl))
                    {
                        this.p.IsVisible = true;
                    }
                    else
                    {
                        this.p.IsVisible = false;
                    }
                    if (this.newKBState.IsKeyDown(Keys.M) && this.oldKBState.IsKeyUp(Keys.M)) // Open main menu
                    {
                        this.mainMenu.Show();
                    }
                    
                    if (this.newKBState.IsKeyDown(Keys.Escape) && this.oldKBState.IsKeyUp(Keys.Escape))  // Open main menu
                    {
                        this.mainMenu.Show();
                    }
                    
                    this.UpdateMove(this.continents);
                    
                    this.npcs.RemoveAll(x => x.IsDestroyed);
                    for (int i = this.npcs.Count - 1; i >= 0; i--)
                    {
                        this.npcs[i].Update(this.playerShip, ref this.gameState, gameTime);
                    }
                    
                    this.oldKBState = this.newKBState;
                    break;
                case GameState.Combat:
                    if (!this.flag)
                    {
                        this.npcs.Find(x => x.IsInCombat).AdjustPos(300, 200);
                        this.playerShip.AdjustPos(300, 500);
                        this.flag = true;
                    }
                    var ships = this.npcs.FindAll(x => x.IsInCombat = true);
                    this.UpdateMove(ships.ConvertAll<IDrawableCustom>(x => x as IDrawableCustom));
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        this.playerShip.Fire(gameTime);
                    }
                    this.playerShip.Update(this.npcs.Find(x => x.IsInCombat), ref this.gameState, gameTime);
                    this.npcs.Find(x => x.IsInCombat).Update(this.playerShip, ref this.gameState, gameTime);
                    if(this.playerShip.IsDestroyed)
                    {
                        gameState = GameState.GameOver;
                    }
                    break;
                case GameState.GameOver:
                    this.newKBState = Keyboard.GetState();
                    if (this.newKBState.IsKeyDown(Keys.Escape) && this.oldKBState.IsKeyUp(Keys.Escape))  // Open main menu
                    {
                        this.mainMenu.Show();
                    }
                    break;
                case GameState.YouWin:
                    break;
                default:
                    break;
            }
            healthBar.Update();
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
            switch (this.gameState)
            {
                case GameState.FreeRoam:
                    this.spriteBatch.Draw(this.waterMapTexture, this.watermapRect, Color.White);
                    this.continent1.Draw(this.spriteBatch);
                    this.continent2.Draw(this.spriteBatch);
                    this.continent3.Draw(this.spriteBatch);
                    this.fishingVillage1.Draw(spriteBatch);
                    this.playerShip.Draw(this.spriteBatch);
                    this.p.Draw(this.spriteBatch);
                    foreach (var item in this.npcs)
                    {
                        item.Draw(this.spriteBatch);
                    }
                    break;
                case GameState.Combat:
                    if (this.npcs.Find(x => x.IsInCombat).IsDestroyed)
                    {
                        this.gameState = GameState.FreeRoam;
                        break;
                    }
                    this.spriteBatch.Draw(this.waterMapTexture, this.watermapRect, Color.White);
                    this.playerShip.Draw(this.spriteBatch);
                    foreach (var item in this.playerShip.Bullets)
                    {
                        item.Draw(this.spriteBatch);
                    }
                    foreach (var item in this.npcs.Find(x => x.IsInCombat).Bullets)
                    {
                        item.Draw(this.spriteBatch);
                    }
                    this.npcs.Find(x => x.IsInCombat).Draw(this.spriteBatch);
                    break;
                case GameState.GameOver:
                    spriteBatch.DrawString(this.gameFont, "You got sunk!", new Vector2(GlobalConstants.WINDOW_WIDTH/2-50,GlobalConstants.WINDOW_HEIGHT / 2), Color.Red);
                    break;
                case GameState.YouWin:
                    break;
                default:
                    break;
            }
            healthBar.Draw(spriteBatch);
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
            test.Items.Add(new SelectableItem<Interfaces.IDrawableCustom>(this.continent1, this.OnExit));
            test.Items.Add(new SelectableItem<Interfaces.IDrawableCustom>(this.continent2, null));
            test.Items.Add(new SelectableItem<Interfaces.IDrawableCustom>(this.continent3, this.OnExit));
            test.Items.Add(new SelectableItem<Interfaces.IDrawableCustom>(this.continent1, null));
            test.Items.Add(new SelectableItem<Interfaces.IDrawableCustom>(this.continent1, null));
            
            test.Show();
        }
        
        private void UpdateMove(List<IDrawableCustom> drawables)
        {
            if (this.EnableGameProcessing)                 // Process the input if the menu is off
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    if(gameState==GameState.Combat)
                    {
                        if (this.playerShip.Rectangle.Top - npcs.Find(x => x.IsInCombat).Rectangle.Bottom >= 50)
                            this.playerShip.Move(Keys.Up, drawables);
                    }
                    else
                    {
                        this.playerShip.Move(Keys.Up, drawables);
                    }
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    this.playerShip.Move(Keys.Down, drawables);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    this.playerShip.Move(Keys.Left, drawables);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    this.playerShip.Move(Keys.Right, drawables);
                }
            }
        }
    }
}