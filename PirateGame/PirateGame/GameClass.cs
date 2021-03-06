﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PirateGame.Enums;
using PirateGame.Interfaces;
using PirateGame.MapObjects;
using PirateGame.Popups;
using PirateGame.Ships;
using PirateGame.Upgrades;
using PirateGame.UserInterface;

#endregion

namespace PirateGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>con1
    public class GameClass : Game
    {
        private static Random rnd;
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
        private MilitaryPort militaryPort1;
        private List<PirateGame.Interfaces.IDrawableCustom> continents;
        private List<string> shipMessages;
        private List<string> fishingMessages;
        private List<string> tradeMessages;
        private List<string> militaryMessages;
        private Popup shipPopup;
        private Popup fishingPopup;
        private Popup tradePopup;
        private Popup militaryPopup;
        private KeyboardState newKBState;
        private bool flag = false;
        private List<NpcShip> npcs;
        private SpriteFont gameFont;
        private Upgrade wUpgrade;
        private bool upgradeFlagWeapons = false;
        private bool upgradeFlagHull = false;
        private double upgradeTime = 0;
        
        //Menu system
        private MenuAccountant menus;
        private UserInterfaceElement mainMenu;
        private Texture2D UIFrameTexture;
        private List<UserInterfaceElement> UIElements;
        
        public GameClass() : base()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
            this.graphics.PreferredBackBufferWidth = GlobalConstants.WINDOW_WIDTH;
            this.graphics.PreferredBackBufferHeight = GlobalConstants.WINDOW_HEIGHT;
            
            this.IsMouseVisible = true;
            
            this.UIElements = new List<UserInterfaceElement>();
            this.menus.openCount = 0;
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

            base.Initialize();
        }
        
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            this.gameFont = this.Content.Load<SpriteFont>("Arial");
            this.waterMapTexture = this.Content.Load<Texture2D>("waterMap");
            this.watermapRect = new Rectangle(0, 0, GlobalConstants.WINDOW_WIDTH, GlobalConstants.WINDOW_HEIGHT);
            this.continent1 = new Continent(this.Content,"con1",500,100,250,250);
            this.continent2 = new Continent(this.Content, "con2", 40, 40, 300, 300);
            this.continent3 = new Continent(this.Content, "con3", 40, 300, 300, 300);
            this.playerShip = new PlayerShip(this.Content, "pirate_ship", GlobalConstants.WINDOW_WIDTH / 2, GlobalConstants.WINDOW_HEIGHT / 2);
            this.npcs = new List<NpcShip>
            {
                new NpcShip(this.Content,"pirate_ship_npc1",300,200,new Vector2(500,100)),
                new NpcShip(this.Content,"pirate_ship_npc1",500,300),
                new NpcShip(this.Content,"pirate_ship_npc1",400,400,new Vector2(240,520)),
                new NpcShip(this.Content,"big_ship",570,490,100,100)
            };
            // add fishing village
            this.fishingVillage1 = new FishingVillage(2, 200, 10, 10000, 150, 1000, 30, Coutries.Tanzania,
                this.Content, "fishing_village", 500, 100, 100, 100);
            // add trade center
            this.tradeCenter1 = new TradeCenter(5000, 50, 100000, 800, 6000, 50, Coutries.Yemen,
                this.Content, "trade_center", 100, 470, 128, 128);
            // add military settlement
            this.militaryPort1 = new MilitaryPort(0, 200, 150, 500, 800, 300, Coutries.Yemen, this.Content, "military_settlement", 180, 310, 128, 128);            
            
            this.continents = new List<PirateGame.Interfaces.IDrawableCustom>
            {
                this.continent1,
                this.continent2,
                this.continent3,
            };
            this.shipMessages = new List<string>
            {
                string.Format("Hull:{0}", this.playerShip.Hull),
                string.Format("Weapons:{0}", this.playerShip.Weapons),
                string.Format("Hit Points:{0}", this.playerShip.Hitpoints),
                string.Format("Damage:{0}", this.playerShip.Damage)
            };
            
            this.fishingMessages = new List<string>
            {
                "Fishing Village",
                string.Format("Population:{0}", this.fishingVillage1.Population),
                string.Format("Wealth:{0}", this.fishingVillage1.Wealth),
                string.Format("Defense Power:{0}", this.fishingVillage1.DefencePower)
            };
            
            this.tradeMessages = new List<string>
            {
                "Trade Center",
                string.Format("Population:{0}", this.tradeCenter1.Population),
                string.Format("Wealth:{0}", this.tradeCenter1.Wealth),
                string.Format("Defense Power:{0}", this.tradeCenter1.DefencePower)
            };
            
            this.militaryMessages = new List<string>
            {
                "Military Port",
                string.Format("Population:{0}", this.militaryPort1.Population),
                string.Format("Wealth:{0}", this.militaryPort1.Wealth),
                string.Format("Defense Power:{0}", this.militaryPort1.DefencePower)
            };
            
            this.shipPopup = new Popup(this.Content, "popup_background", "Arial", this.shipMessages, this.playerShip);
            this.fishingPopup = new Popup(this.Content, "popup_background", "Arial", this.fishingMessages, this.fishingVillage1);
            this.tradePopup = new Popup(this.Content, "popup_background", "Arial", this.tradeMessages, this.tradeCenter1);
            this.militaryPopup = new Popup(this.Content, "popup_background", "Arial", this.militaryMessages, this.militaryPort1);

            this.UIFrameTexture = this.Content.Load<Texture2D>("InventoryHighlight.png");

            Menu main = new Menu(this, "Pirate Menu", menus.OnOpen, menus.OnClose);
            main.Initialize();
            main.LoadContent();
            main.Items.Add(new SelectableItem<string>("Play", this.OnPlay));
            main.Items.Add(new SelectableItem<string>("Test Inventory", this.OnInventoryTest));
            main.Items.Add(new SelectableItem<string>("Test Health Bar", this.OnHealthBarTest));
            main.Items.Add(new SelectableItem<string>("Quit", this.OnExit));
            this.mainMenu = new Frame(main, this.UIFrameTexture, Color.Gray);
            this.UIElements.Add(this.mainMenu);
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
                    this.flag = false;
                    this.newKBState = Keyboard.GetState();
                    
                    // add production of goods
                    this.tradeCenter1.ProduceGoods(gameTime);
                    this.fishingVillage1.ProduceGoods(gameTime);
                    
                    this.playerShip.ResetHitpoints();
                    
                    bool mouseOverShip = this.playerShip.Rectangle.Contains(Mouse.GetState().X, Mouse.GetState().Y);
                    bool mouseOverFishing = this.fishingVillage1.Rectangle.Contains(Mouse.GetState().X, Mouse.GetState().Y);
                    bool mouseOverTrade = this.tradeCenter1.Rectangle.Contains(Mouse.GetState().X, Mouse.GetState().Y);
                    bool mouseOverMilitary = this.militaryPort1.Rectangle.Contains(Mouse.GetState().X, Mouse.GetState().Y);
                    
                    if (mouseOverShip || this.newKBState.IsKeyDown(Keys.LeftControl))
                    {
                        this.shipPopup.IsVisible = true;
                    }
                    else
                    {
                        this.shipPopup.IsVisible = false;
                    }
                    if (mouseOverFishing || this.newKBState.IsKeyDown(Keys.LeftControl))
                    {
                        this.fishingPopup.IsVisible = true;
                    }
                    else
                    {
                        this.fishingPopup.IsVisible = false;
                    }
                    if (mouseOverTrade || this.newKBState.IsKeyDown(Keys.LeftControl))
                    {
                        this.tradePopup.IsVisible = true;
                    }
                    else
                    {
                        this.tradePopup.IsVisible = false;
                    }
                    if (mouseOverMilitary || this.newKBState.IsKeyDown(Keys.LeftControl))
                    {
                        this.militaryPopup.IsVisible = true;
                    }
                    else
                    {
                        this.militaryPopup.IsVisible = false;
                    }
                    if ((this.newKBState.IsKeyDown(Keys.M) && this.oldKBState.IsKeyUp(Keys.M)) ||   // Open main menu
                        (this.newKBState.IsKeyDown(Keys.Escape) && this.oldKBState.IsKeyUp(Keys.Escape)))
                    {
                        this.mainMenu.Show();
                    }
                    
                    this.UpdateMove(this.continents);
                    
                    this.npcs.RemoveAll(x => x.IsDestroyed);
                    for (int i = 0; i < this.npcs.Count; i++)
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
                    var ships = this.npcs.FindAll(x => x.IsInCombat == true);
                    this.UpdateMove(ships.ConvertAll<IDrawableCustom>(x => x as IDrawableCustom));
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        this.playerShip.Fire(gameTime);
                    }
                    this.playerShip.Update(this.npcs.Find(x => x.IsInCombat), ref this.gameState, gameTime);
                    if (this.npcs.RemoveAll(x => x.IsDestroyed == true) > 0)
                    {
                        rnd = new Random();
                        if(rnd.Next(1,101)<=50)
                        {
                            this.upgradeFlagWeapons = true;
                        }
                        else
                        {
                            this.upgradeFlagHull = true;
                        }

                        break;
                    }
                    this.npcs.Find(x => x.IsInCombat).Update(this.playerShip, ref this.gameState, gameTime);
                    if (this.playerShip.IsDestroyed)
                    {
                        this.gameState = GameState.GameOver;
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
            
            for (int index = 0; index < this.UIElements.Count; index++)
            {
                this.UIElements[index].Update(gameTime);
            }
            
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
                    this.fishingVillage1.Draw(this.spriteBatch);
                    this.tradeCenter1.Draw(this.spriteBatch);
                    this.militaryPort1.Draw(this.spriteBatch);
                    this.playerShip.Draw(this.spriteBatch);
                    this.shipPopup.Draw(this.spriteBatch);
                    this.fishingPopup.Draw(this.spriteBatch);
                    this.tradePopup.Draw(this.spriteBatch);
                    this.militaryPopup.Draw(this.spriteBatch);
                    foreach (var item in this.npcs)
                    {
                        item.Draw(this.spriteBatch);
                    }
                    if (this.upgradeFlagWeapons)
                    {
                        if (this.upgradeTime == 0)
                        {
                            this.wUpgrade = new WeaponUpgrade(this.Content, "Arial", "Weapon Upgraded", this.playerShip);
                            this.wUpgrade.UpgradeShip();
                            this.shipPopup.Messages.RemoveAt(1);
                            this.shipPopup.Messages.Insert(1, string.Format("Weapons:{0}", this.playerShip.Weapons));
                            this.upgradeTime = gameTime.TotalGameTime.TotalSeconds;
                        }
                        if (gameTime.TotalGameTime.TotalSeconds - this.upgradeTime < 4)
                        {
                            this.wUpgrade = new WeaponUpgrade(this.Content, "Arial", "Weapon Upgraded", this.playerShip);
                            this.wUpgrade.Draw(this.spriteBatch);
                        }
                        else
                        {
                            this.upgradeFlagWeapons = false;
                            this.upgradeTime = 0;
                        }
                    }

                    if (this.upgradeFlagHull)
                    {
                        if (this.upgradeTime == 0)
                        {
                            this.wUpgrade = new HullUpgrade(this.Content, "Arial", "Hull Upgraded", this.playerShip);
                            this.wUpgrade.UpgradeShip();
                            this.shipPopup.Messages.RemoveAt(0);
                            this.shipPopup.Messages.Insert(0, string.Format("Hull:{0}", this.playerShip.Hull));
                            this.upgradeTime = gameTime.TotalGameTime.TotalSeconds;
                        }
                        if (gameTime.TotalGameTime.TotalSeconds - this.upgradeTime < 4)
                        {
                            this.wUpgrade = new WeaponUpgrade(this.Content, "Arial", "Hull Upgraded", this.playerShip);
                            this.wUpgrade.Draw(this.spriteBatch);
                        }
                        else
                        {
                            this.upgradeFlagHull = false;
                            this.upgradeTime = 0;
                        }
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
                    this.spriteBatch.DrawString(this.gameFont, "You got sunk!", new Vector2(GlobalConstants.WINDOW_WIDTH / 2 - 50,GlobalConstants.WINDOW_HEIGHT / 2), Color.Red);
                    break;
                case GameState.YouWin:
                    break;
                default:
                    break;
            }
            
            for (int index = 0; index < this.UIElements.Count; index++)
            {
                this.UIElements[index].Draw(this.spriteBatch);
            }
            
            this.spriteBatch.End();
            
            base.Draw(gameTime);
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
        { }
        
        private void OnInventoryTest(object menuItem, EventArgs e = null)
        {
            Inventory test = new Inventory(this, this.menus.OnOpen, this.menus.OnClose);
            test.Initialize();
            test.LoadContent();
            
            // TODO: remove the test
            // add items 
            Continent item1 = new Continent(this.Content, "cutlass", 500, 100, 250, 250);
            //test.Items.Add(new SelectableItem<Interfaces.IDrawableCustom>(this.continent1, this.OnPlay));
            test.Items.Add(new SelectableItem<Interfaces.IDrawableCustom>(item1, this.OnPlay));
            test.Items.Add(new SelectableItem<Interfaces.IDrawableCustom>(this.continent2, this.OnPlay));
            test.Items.Add(new SelectableItem<Interfaces.IDrawableCustom>(this.continent3, this.OnPlay));
            test.Items.Add(new SelectableItem<Interfaces.IDrawableCustom>(this.continent1, this.OnPlay));
            test.Items.Add(new SelectableItem<Interfaces.IDrawableCustom>(this.continent1, this.OnPlay));

            Frame testFrame = new Frame(test, this.UIFrameTexture, Color.Gray);
            testFrame.Show();

            this.UIElements.Add(testFrame);
        }
        
        private void OnHealthBarTest(object menuItem, EventArgs e = null)
        {
            // TODO: remove the test
            HealthBar test = new HealthBar(this, HealthBar.UpdateHealthTest);
            test.Initialize();
            test.LoadContent();
            
            this.UIElements.Add(test);
            
            test.Rectangle = new Rectangle(50, 50, 100, 20);
            
            test.Show();
        }
        
        private void UpdateMove(List<IDrawableCustom> drawables)
        {
            if (this.menus.AllClosed)                       // Process the input if the menu is off
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    if (this.gameState == GameState.Combat)
                    {
                        if (this.playerShip.Rectangle.Top - this.npcs.Find(x => x.IsInCombat).Rectangle.Bottom >= 50)
                        {
                            this.playerShip.Move(Keys.Up, drawables);
                        }
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