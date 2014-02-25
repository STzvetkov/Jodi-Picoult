namespace PirateGame.UserInterface
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class UserInterfaceElement : DrawableGameComponent, PirateGame.Interfaces.IDrawableCustom
    {
        protected UserInterfaceElement(Game game) : base(game)
        {}

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

        public abstract void Draw(SpriteBatch spriteBatch);

        public virtual void ToggleActive()
        {
            this.Enabled = !this.Enabled;
            this.Visible = !this.Visible;
        }
    }
}
