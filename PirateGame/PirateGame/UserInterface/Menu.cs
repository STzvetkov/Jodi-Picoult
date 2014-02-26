namespace PirateGame.UserInterface
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System;
    using System.Collections.Generic;

    public class Menu : UserInterfaceElement
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


        public Menu(Game game, string title, EventHandler showMenuHandler = null, EventHandler hideMenuHandler = null)
            : base(game, showMenuHandler, hideMenuHandler)
        {
            this.Enabled = false;                               // Disable Update()
            this.Visible = false;                               // Disable Draw()
            this.Items = new List<SelectableItem<string>>();    // Create the menu list
            this.Title = title;
            this.highlightPosition = 0;

            game.Components.Add(this);
        }

        public IList<SelectableItem<string>> Items { get; private set; }

        private Texture2D Background
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
            this.Background = Game.Content.Load<Texture2D>("StoneBackground");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            // If draw rectangle isn't set, set it to the middle of the window
            if (this.Rectangle.IsEmpty)
            {
                int leftEdge = (this.Graphics.PreferredBackBufferWidth - this.Background.Width) / 2;
                int topEdge = (this.Graphics.PreferredBackBufferHeight - this.Background.Height) / 2;
                this.Rectangle = new Rectangle(leftEdge, topEdge, this.Background.Width, this.Background.Height);
            }

            // Draw background
            spriteBatch.Draw(this.Background, this.Rectangle, Color.White);

            // Draw title
            Vector2 itemPosition = new Vector2(this.Rectangle.X + Menu.TitleOffsetX, this.Rectangle.Y + Menu.TitleHeight);
            spriteBatch.DrawString(this.menuFont, this.Title, itemPosition, Menu.ItemColor);
            itemPosition.Y += Menu.TitleHeight;

            // Draw items
            itemPosition.X = this.Rectangle.X + Menu.ItemsOffsetX;
            for (int index = 0; index < this.Items.Count; index++)
            {
                Color color = Menu.ItemColor;
                if (highlightPosition == index)
                {
                    color = Menu.ItemHighlightColor;
                }
                itemPosition.Y += Menu.ItemsHeight;
                spriteBatch.DrawString(this.menuFont, this.Items[index].Item, itemPosition, color);
            }

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            KeyboardState newKBState = Keyboard.GetState();

            if (newKBState.IsKeyDown(Keys.Escape) && this.oldKBState.IsKeyUp(Keys.Escape))  // Check for close menu
            {
                this.Hide();
            }

            if (this.Items.Count > 0)                   // Check if menu contains items
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
                    if (highlightPosition < Items.Count - 1)
                    {
                        highlightPosition++;
                    }
                }
                else if (newKBState.IsKeyDown(Keys.Enter) && this.oldKBState.IsKeyUp(Keys.Enter))
                {
                    Items[highlightPosition].Select();
                }
            }

            this.oldKBState = newKBState;
        }

        public override void Show()
        {
            base.Show();

            this.oldKBState = Keyboard.GetState();          // Store old KB state on open
        }
    }
}
