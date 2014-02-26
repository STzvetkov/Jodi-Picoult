using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PirateGame.Interfaces;
using PirateGame.Ships;

namespace PirateGame.Upgrades
{
    public abstract class Upgrade:IUpgrade
    {
        private SpriteFont font;
        private Vector2 position;
        private string text;
        protected Ship ship;

        public Upgrade(ContentManager content, string font, Vector2 position, string text, Ship ship)
        {
            this.font = content.Load<SpriteFont>(font);
            this.position = position;
            this.text = text;
            this.ship = ship;
        }

        public abstract void UpgradeShip();

        public void Draw(SpriteBatch spriteBatch)
        {
            UpgradeShip();
            spriteBatch.DrawString(this.font, text, this.position, Color.Black);
        }
    }
}