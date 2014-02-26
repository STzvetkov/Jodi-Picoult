namespace PirateGame.UserInterface
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public abstract class UserInterfaceElement : DrawableGameComponent, PirateGame.Interfaces.IDrawableCustom
    {
        protected event EventHandler showItemHandler;       // An event that notifies when the menu is shown
        protected event EventHandler hideItemHandler;       // An event that notifies when the item is hidden

        protected UserInterfaceElement(Game game, EventHandler showItemHandler = null, EventHandler hideItemHandler = null)
            : base(game)
        {
            this.showItemHandler = showItemHandler;
            this.hideItemHandler = hideItemHandler;
        }

        protected GraphicsDeviceManager Graphics { get; set; }
        protected SpriteBatch SpriteBatch { get; set; }

        public override void Initialize()
        {
            this.Graphics = (GraphicsDeviceManager)Game.Services.GetService(typeof(GraphicsDeviceManager));
            this.SpriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));

            base.Initialize();
        }

        public virtual Rectangle Rectangle { get; protected set; }
        public virtual Texture2D Texture { get; protected set; }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Draw(this.SpriteBatch);
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public virtual void Hide()
        {
            if (this.Visible == false)
            {
                return;
            }

            this.Enabled = false;
            this.Visible = false;

            if (hideItemHandler != null)
            {
                hideItemHandler(this, null);
            }
        }

        public virtual void Show()
        {
            if (this.Visible == true)
            {
                return;
            }

            this.Enabled = true;
            this.Visible = true;

            if (showItemHandler != null)
            {
                showItemHandler(this, null);
            }
        }

        
    }
}
