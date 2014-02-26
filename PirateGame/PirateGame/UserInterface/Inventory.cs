namespace PirateGame.UserInterface
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using PirateGame.Interfaces;
    using System;
    using System.Collections.Generic;

    public class Inventory : UserInterfaceElement
    {
        private static readonly Color ItemHighlightColor = Color.Aqua;
        private const int ItemDisplaySize = 40;
        private const int ItemSpacing = 25;
        private const int HighlightFrameWidth = 5;

        private KeyboardState oldKBState;

        private Texture2D HighlightTexture;
        private int highlightIndex;

        public Inventory(Game game, EventHandler showInventoryHandler = null, EventHandler hideInventoryHandler = null)
            : base(game, showInventoryHandler, hideInventoryHandler)
        {
            this.Enabled = false;                                       // Disable Update()
            this.Visible = false;                                       // Disable Draw()
            this.Items = new List<SelectableItem<IDrawableCustom>>();   // Create the inventory list
            this.highlightIndex = 0;

            game.Components.Add(this);
        }

        public IList<SelectableItem<IDrawableCustom>> Items;

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

        protected override void LoadContent()
        {
            base.LoadContent();
            this.HighlightTexture = Game.Content.Load<Texture2D>("InventoryHighlight.png");
            this.Background = Game.Content.Load<Texture2D>("StoneBackgroundHorizontal");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            // If draw rectangle isn't set, set it to the middle of the window
            if (this.Rectangle.IsEmpty)
            {
                int leftEdge = (this.Graphics.PreferredBackBufferWidth - this.Background.Width) / 2;
                int topEdge = (this.Graphics.PreferredBackBufferHeight - this.Background.Height) / 2;
                this.Rectangle = new Rectangle(leftEdge, topEdge, this.Background.Width, this.Background.Height);
            }

            // Draw background
            spriteBatch.Draw(this.Background, this.Rectangle, Color.White);

            // Draw Items
            Rectangle itemPosition = new Rectangle(this.Rectangle.X + Inventory.ItemSpacing,
                                                   this.Rectangle.Y + Inventory.ItemSpacing,
                                                   Inventory.ItemDisplaySize, Inventory.ItemDisplaySize);
            for (int index = 0; index < this.Items.Count; index++)
            {
                spriteBatch.Draw(this.Items[index].Item.Texture, itemPosition, Color.White);
                if (index == highlightIndex)
                {
                    Vector2 highlightPosition = new Vector2(itemPosition.X - HighlightFrameWidth,
                                                            itemPosition.Y - HighlightFrameWidth);
                    spriteBatch.Draw(this.HighlightTexture, highlightPosition, Color.Aqua);
                }
                itemPosition.X += this.HighlightTexture.Width + Inventory.ItemSpacing;
            }

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
                if (newKBState.IsKeyDown(Keys.Left) && this.oldKBState.IsKeyUp(Keys.Left))
                {
                    if (this.highlightIndex > 0)
                    {
                        this.highlightIndex--;
                    }
                }
                else if (newKBState.IsKeyDown(Keys.Right) && this.oldKBState.IsKeyUp(Keys.Right))
                {
                    if (this.highlightIndex < Items.Count - 1)
                    {
                        this.highlightIndex++;
                    }
                }
                else if (newKBState.IsKeyDown(Keys.Enter) && this.oldKBState.IsKeyUp(Keys.Enter))
                {
                    Items[this.highlightIndex].Select();
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
