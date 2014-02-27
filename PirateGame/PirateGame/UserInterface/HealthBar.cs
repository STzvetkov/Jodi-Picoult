namespace PirateGame.UserInterface
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public class HealthBar : UserInterfaceElement
    {
        public const double YellowPercentage = 0.75;
        public const double RedPercentage = 0.25;

        private Func<double> getHealthDelegate;
        private Texture2D lifeBar;
        private int currentHealth;

        public HealthBar(Game game, Func<double> getHealthPercentDelegate)
            : base(game)
        {
            this.Enabled = false;                                       // Disable this UI Element
            this.Visible = false;
            this.GetHealthPercentDelegate = getHealthPercentDelegate;   // Assign getHealth delegate
            this.currentHealth = this.GetHealth();                      // Get target health

            //this.Game.Components.Add(this);
        }

        ~HealthBar()
        {
            //this.Game.Components.Remove(this);
        }

        private int FullHealth
        {
            get
            {
                return this.Rectangle.Width;
            }
        }

        private Texture2D Container
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

        private int GetHealth()
        {
            double healthPercent = GetHealthPercentDelegate();       // Get target health

            if (healthPercent > 100)
            {
                healthPercent = 100;
            }
            if (healthPercent < 0)
            {
                healthPercent = 0;
            }

            return (int)((healthPercent/100) * this.FullHealth);
        }

        public Func<double> GetHealthPercentDelegate
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

        public override void LoadContent()
        {
            base.LoadContent();

            this.Container = this.Game.Content.Load<Texture2D>("lifeBar");
            this.lifeBar = this.Game.Content.Load<Texture2D>("healthGauge");

            this.Rectangle = new Rectangle(0, 0, lifeBar.Width, lifeBar.Height);

            this.currentHealth = this.FullHealth;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if ((this.Visible == false) || (spriteBatch == null))
            {
                return;
            }

            // draw health bar on this.Rectangle coordinates
            Rectangle barRectangle = new Rectangle(this.Rectangle.X, this.Rectangle.Y, currentHealth, this.Rectangle.Height);
            spriteBatch.Draw(this.lifeBar, barRectangle, this.HealthColor);
            spriteBatch.Draw(this.Container, this.Rectangle, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            // update internal state here
            base.Update(gameTime);

            if (this.Enabled == false)
            {
                return;
            }

            this.currentHealth = this.GetHealth();          // Get target health
        }

        public Color HealthColor
        {
            get
            {
                if (this.currentHealth >= this.FullHealth * YellowPercentage)
                {
                    return Color.Green;
                }
                else if (this.currentHealth >= this.FullHealth * RedPercentage)
                {
                    return Color.Yellow;
                }
                else
                {
                    return Color.Red;
                }
            }
        }

        /// <summary>
        /// HealthBarTest
        /// </summary>
        /// TODO: remove test
        private const double healthMax = 100;
        private static double healthCount;
        public static double UpdateHealthTest()
        {
            double rateOfChange = 1;

            if (healthCount > 0)
                healthCount -= rateOfChange;
            else
                healthCount = healthMax;

            return healthCount;
        }
    }
}
