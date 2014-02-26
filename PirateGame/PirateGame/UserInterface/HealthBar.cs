namespace PirateGame.UserInterface
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public class HealthBar : UserInterfaceElement
    {
        private int health;
        private Func<int> getHealthDelegate;
        private Texture2D container, lifebar;

        public HealthBar(Game game, Func<int> getHealthDelegate)
            : base(game)
        {
            this.Enabled = false;                           // Disable Update()
            this.Visible = false;                        // Disable Draw()

            this.GetHealthDelegate = getHealthDelegate;     // Assign getHealth delegate
            this.health = GetHealthDelegate();              // Get target health

            game.Components.Add(this);                      // Add health bar to the game components collection
            // LoadContent(), Draw(GameTime gameTime) & Update(GameTime gameTime)
            // are called automatically by the game

            throw new System.NotImplementedException();
        }



        public Func<int> GetHealthDelegate
        {
            private get
            {
                return this.getHealthDelegate;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("GetHealthDelegate", "GetHealthDelegate can't be null.");
                }
                this.getHealthDelegate = value;
            }
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            container = Game.Content.Load<Texture2D>("lifeBar");  // load health bar texture
            lifebar = Game.Content.Load<Texture2D>("healthGauge");
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Draw(this.SpriteBatch);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // draw health bar on this.Rectangle coordinates
            spriteBatch.Draw(lifebar, new Rectangle(100, 100, 40, 40), Color.White);
            spriteBatch.Draw(container, Rectangle, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            // update internal state here
            base.Update(gameTime);
        }
    }
}
