using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PirateGame.Interfaces;
using PirateGame.Ships;

namespace PirateGame.Ships
{
    public class Projectile : IDrawableCustom
    {
        private Ship ship;
        private Rectangle rectangle;
        private Texture2D texture;

        public Projectile(ContentManager manager, Ship ship)
        {
            this.ship = ship;
            this.texture = manager.Load<Texture2D>("Projectile");
            this.rectangle = new Rectangle(ship.Rectangle.X + 15,ship.Rectangle.Y + 1,45,45);
            this.Hit = false;
        }

        public bool Hit { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return this.rectangle;
            }
        }

        public Texture2D Texture
        {
            get
            {
                return this.texture;
            }
        }

        public void Update(Ship target, Ship attacker)
        {
            if (this.rectangle.Intersects(target.Rectangle))
            {
                this.Hit = true;
            }
            if (attacker.Rectangle.Y > target.Rectangle.Y)
            {
                this.rectangle.Y -= 2;
            }
            else
            {
                this.rectangle.Y += 2;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.rectangle, Color.White);
        }
    }
}