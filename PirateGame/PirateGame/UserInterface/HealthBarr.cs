namespace PirateGame.UserInterface
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    class HealthBarr
    {
        private Texture2D container, lifeBar;
        private Vector2 position;
        private int fullHealth;
        private int currentHealth;
        private int reteOfChange;
        private Color barColor;

        public HealthBarr(ContentManager content)
        {
            position = new Vector2(10, 10);
            LoadContent(content);
            fullHealth = lifeBar.Width;
            currentHealth = fullHealth;
            reteOfChange = 1;
        }

        private void LoadContent(ContentManager content)
        {
            container = content.Load<Texture2D>("lifeBar");
            lifeBar = content.Load<Texture2D>("healthGauge");
        }

        public void Update()
        {
            HealthColor();
            if (currentHealth >= 0)
                currentHealth -= reteOfChange;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(lifeBar, position, new Rectangle((int)position.X, (int)position.Y, currentHealth, lifeBar.Height), barColor);
            spriteBatch.Draw(container, position, Color.White);
        }

        public void HealthColor()
        {
            if (currentHealth >= lifeBar.Width * 0.75)
                barColor = Color.Green;

            else if (currentHealth >= lifeBar.Width * 0.25)
                barColor = Color.Yellow;
            else
            {
                barColor = Color.Red;
            }
        }

    }
}
