using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PirateGame.Interfaces;
using PirateGame.Ships;

namespace PirateGame.Upgrades
{
    public abstract class Upgrade : IUpgrade
    {
        protected Ship ship;

        private SpriteFont font;
        private Vector2 position;
        private string text;

        public Upgrade(ContentManager content, string font, string text, Ship ship)
        {
            this.font = content.Load<SpriteFont>(font);
            this.position.X = ship.Rectangle.X;
            this.position.Y = ship.Rectangle.Y-10;
            this.text = text;
            this.ship = ship;
        }

        public abstract void UpgradeShip();

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(this.font, this.text, this.position, Color.Red);
            
        }
    }
}