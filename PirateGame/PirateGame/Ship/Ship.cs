using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PirateGame.Interfaces;

namespace PirateGame.Ship
{
    public abstract class Ship : IMovable
    {
        private Vector2 speed;

        private Rectangle rect;

        public Ship(ContentManager content, string texture, int x, int y)
        {
            this.Texture = content.Load<Texture2D>(texture);
            this.rect = new Rectangle(x,y,50,50);
            this.speed = new Vector2(5,5);
        }

        public Texture2D Texture { get; set; }

        public int HitPoints { get; set; }

        public Point CurrentPosition
        {
            get
            {
                return this.rect.Center;
            }
        }

        public virtual void Move(Keys key, int width, int height)
        {
            switch(key)
            {
                case Keys.Left:
                    this.rect.X -= (int)this.speed.X;
                    if (this.rect.X < 0)
                    {
                        this.rect.X = 1;
                    }
                    break;
                case Keys.Right:
                    if (this.rect.Right == height)
                    {
                        break;
                    }
                    if (this.rect.Right + (int)this.speed.X > width)
                    {
                        this.rect.X = width - this.rect.Width - 1;
                        break;
                    }
                    this.rect.X += (int)this.speed.X;
                    break;
                case Keys.Up:
                    this.rect.Y -= (int)this.speed.Y;
                    if (this.rect.Y < 0)
                    {
                        this.rect.Y = 1;
                    }
                    break;
                case Keys.Down:
                    if (this.rect.Bottom == height)
                    {
                        break;
                    }
                    if (this.rect.Bottom + (int)this.speed.Y > height)
                    {
                        this.rect.Y = height - this.rect.Height - 1;
                        break;
                    }
                    this.rect.Y += (int)this.speed.Y;
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.rect, Color.White);
        }
    }
}