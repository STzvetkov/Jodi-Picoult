namespace PirateGame.UserInterface
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public abstract class Decorator : UserInterfaceElement
    {
        protected UserInterfaceElement element;

        public Decorator(UserInterfaceElement element)
            : base(element.Game)
        {
            this.element = element;
            this.element.ShowItemHandler += this.OnShow;
            this.element.HideItemHandler += this.OnHide;
        }

        private void OnShow(object menuItem, EventArgs e = null)
        {
            base.Show();
        }

        private void OnHide(object menuItem, EventArgs e = null)
        {
            base.Hide();
        }

        public override void Draw(GameTime gameTime)
        {
            this.element.Draw(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.element.Draw(spriteBatch);
        }

        public override void Hide()
        {
            this.element.Hide();
            base.Hide();
        }

        public override void Show()
        {
            this.element.Show();
            base.Show();
        }

        public override void Initialize()
        {
            this.element.Initialize();
        }

        public override void LoadContent()
        {
            this.element.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            this.element.Update(gameTime);
        }
    }
}
