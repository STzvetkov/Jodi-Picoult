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
        private static readonly int WINDOW_HEIGHT = 800;
        private PlayerShip playerShip;
        
        //temporary
        private Texture2D waterMapTexture;
        private Rectangle watermapRect;
        private Texture2D con1Tex;
        private Rectangle con1rect;
        //temporary
        public GameClass() : base()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
            this.graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            this.graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
        }
        
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }
        
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

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
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
        }
        
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                this.playerShip.Move(Keys.Up, WINDOW_WIDTH, WINDOW_HEIGHT);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                this.playerShip.Move(Keys.Down, WINDOW_WIDTH, WINDOW_HEIGHT);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                this.playerShip.Move(Keys.Left, WINDOW_WIDTH, WINDOW_HEIGHT);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                this.playerShip.Move(Keys.Right, WINDOW_WIDTH, WINDOW_HEIGHT);
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
            this.spriteBatch.Draw(this.waterMapTexture, this.watermapRect, Color.White);
            this.spriteBatch.Draw(this.con1Tex, this.con1rect, Color.White);
            this.playerShip.Draw(this.spriteBatch);
            
            this.spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}