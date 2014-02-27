namespace PirateGame.UserInterface
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public abstract class UserInterfaceElement : DrawableGameComponent, PirateGame.Interfaces.IDrawableCustom
    {
        protected UserInterfaceElement(Game game, EventHandler showItemHandler = null, EventHandler hideItemHandler = null)
            : base(game)
        {
            this.ShowItemHandler = showItemHandler;
            this.HideItemHandler = hideItemHandler;
        }

        public event EventHandler ShowItemHandler;       // An event that notifies when the menu is shown
        public event EventHandler HideItemHandler;       // An event that notifies when the item is hidden

        protected GraphicsDeviceManager Graphics { get; set; }
        protected SpriteBatch SpriteBatch { get; set; }

        public override void Initialize()
        {
            this.Graphics = (GraphicsDeviceManager)Game.Services.GetService(typeof(GraphicsDeviceManager));
            this.SpriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));

            if (this.Graphics == null)
            {
                this.Graphics = new GraphicsDeviceManager(this.Game);
            }

            if (this.SpriteBatch == null)
            {
                this.SpriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            }

            base.Initialize();
        }

        public virtual Rectangle Rectangle { get; set; }
        public virtual Texture2D Texture { get; protected set; }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            this.SpriteBatch.Begin();
            Draw(this.SpriteBatch);
            this.SpriteBatch.End();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public virtual void Hide()
        {
            if (this.Visible == false)
            {
                return;
            }

            this.Enabled = false;
            this.Visible = false;

            if (HideItemHandler != null)
            {
                HideItemHandler(this, null);
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

            if (ShowItemHandler != null)
            {
                ShowItemHandler(this, null);
            }
        }

        public virtual void LoadContent()
        {
            base.LoadContent();
        }
    }
}
