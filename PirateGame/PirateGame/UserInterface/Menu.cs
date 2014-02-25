namespace PirateGame.UserInterface
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System;
    using System.Collections.Generic;

    class Menu : UserInterfaceElement
    {
        private const float TitleOffsetX = 50;
        private const float TitleHeight = 25;
        private const float ItemsOffsetX = 35;
        private const float ItemsHeight = 25;
        private static readonly Color ItemColor = Color.DeepSkyBlue;
        private static readonly Color ItemHighlightColor = Color.Aqua;

        private SpriteFont menuFont;
        private KeyboardState oldKBState;

        private string title;
        private int highlightPosition;

        public Menu(Game game, string title, EventHandler closeMenuHandler)
            : base(game)
        {
            this.Enabled = false;                           // Disable Update()
            this.Visible = false;                           // Disable Draw()
            this.MenuItems = new List<MenuItem>();          // Create the menu list
            this.Title = title;
            this.highlightPosition = 0;

            if (closeMenuHandler != null)
            {
                this.closeMenuHandler = closeMenuHandler;
            }
            else
            {
                throw new ArgumentNullException("closeMenuHandler", "Close menu handler can't be null.");
            }

            game.Components.Add(this);
        }

        // An event that clients can use to be notified when the menu is closed
        private event EventHandler closeMenuHandler;

        public List<MenuItem> MenuItems { get; private set; }

        private Texture2D MenuBackground
        {
            get
            {
                return this.Texture;
            }
            set
            {
                this.Texture = value;
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Title can't be null or empty string.");
                }
                this.title = value;
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            this.oldKBState = Keyboard.GetState();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            this.menuFont = (SpriteFont)Game.Content.Load<SpriteFont>("Arial");
            this.MenuBackground = Game.Content.Load<Texture2D>("StoneBackground");
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Draw(this.SpriteBatch);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            // Draw background
            Vector2 menuPosition = new Vector2();
            menuPosition.Y = (this.Graphics.PreferredBackBufferHeight - this.MenuBackground.Height) / 2;
            menuPosition.X = (this.Graphics.PreferredBackBufferWidth - this.MenuBackground.Width) / 2;
            spriteBatch.Draw(this.MenuBackground, menuPosition, Color.White);

            // Draw title
            Vector2 itemPosition = new Vector2(menuPosition.X + Menu.TitleOffsetX, menuPosition.Y + Menu.TitleHeight);
            spriteBatch.DrawString(this.menuFont, this.Title, itemPosition, Menu.ItemColor);
            itemPosition.Y += Menu.TitleHeight;

            // Draw items
            itemPosition.X = menuPosition.X + Menu.ItemsOffsetX;
            for (int index = 0; index < MenuItems.Count; index++)
            {
                Color color = Menu.ItemColor;
                if (highlightPosition == index)
                {
                    color = Menu.ItemHighlightColor;
                }
                itemPosition.Y += Menu.ItemsHeight;
                spriteBatch.DrawString(this.menuFont, this.MenuItems[index].Title, itemPosition, color);
            }

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            KeyboardState newKBState = Keyboard.GetState();

            if (newKBState.IsKeyDown(Keys.Escape) && this.oldKBState.IsKeyUp(Keys.Escape))  // Check for close menu
            {
                closeMenuHandler(this, null);
            }

            if (this.MenuItems.Count > 0)                   // Check if menu contains items
            {
                if (newKBState.IsKeyDown(Keys.Up) && this.oldKBState.IsKeyUp(Keys.Up))
                {
                    if (highlightPosition > 0)
                    {
                        highlightPosition--;
                    }
                }
                else if (newKBState.IsKeyDown(Keys.Down) && this.oldKBState.IsKeyUp(Keys.Down))
                {
                    if (highlightPosition < MenuItems.Count - 1)
                    {
                        highlightPosition++;
                    }
                }
                else if (newKBState.IsKeyDown(Keys.Enter) && this.oldKBState.IsKeyUp(Keys.Enter))
                {
                    MenuItems[highlightPosition].Select();
                }
            }

            this.oldKBState = newKBState;
        }

        public override void ToggleActive()
        {
            base.ToggleActive();

            this.oldKBState = Keyboard.GetState();          // Store old KB state on enable and disable
        }
    }
}
