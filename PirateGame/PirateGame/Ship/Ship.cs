using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PirateGame.Enums;
using PirateGame.Interfaces;
using PirateGame.MapObjects;

namespace PirateGame.Ship
{
    public abstract class Ship : ICollidable
    {
        private Vector2 speed;

        private Rectangle rectangle;

        protected Ship(ContentManager content, string texture, int x, int y)
        {
            this.Texture = content.Load<Texture2D>(texture);
            this.rectangle = new Rectangle(x,y,40,40);
            this.speed = new Vector2(1,1);
            this.Weapons = Weapons.Basic;
            this.Hull = Hull.Basic;
        }

        public Rectangle Rectangle
        {
            get
            {
                return this.rectangle;
            }
        }

        public Texture2D Texture { get; private set; }

        public int HitPoints { get; set; }

        public Weapons Weapons { get; set; }

        public Hull Hull { get; set; }

        public Point CurrentPosition
        {
            get
            {
                return this.rectangle.Center;
            }
        }


        public virtual void Move(Keys key, List<PirateGame.Interfaces.IDrawableCustom> drawables)
        {
            bool match = false;
            Rectangle initial = new Rectangle(this.rectangle.X, this.rectangle.Y, this.rectangle.Width, this.rectangle.Height);
            foreach (var drawable in drawables)
            {
                switch(key)
                {
                    case Keys.Left:
                        this.rectangle.X -= (int)this.speed.X;
                        if (this.IsCollidedWith(drawable))
                        {
                            this.rectangle = initial;
                            while(this.IsCollidedWith(drawable))
                            {
                                this.rectangle.X += 1;
                            }
                            match = true;
                            break;
                        }
                        if (this.rectangle.X < 0)
                        {
                            this.rectangle.X = 1;
                        }
                        break;
                    case Keys.Right:
                        this.rectangle.X += (int)this.speed.X;
                        if (this.IsCollidedWith(drawable))
                        {
                            match = true;
                            this.rectangle = initial;
                            while (this.IsCollidedWith(drawable))
                            {
                                this.rectangle.X -= 1;
                            }
                            break;
                        }
                        if (this.rectangle.Right == GlobalConstants.WINDOW_WIDTH)
                        {
                            break;
                        }
                        if (this.rectangle.Right + (int)this.speed.X > GlobalConstants.WINDOW_WIDTH)
                        {
                            this.rectangle.X = GlobalConstants.WINDOW_WIDTH - this.rectangle.Width - 1;
                            break;
                        }
                           
                        break;
                    case Keys.Up:
                        this.rectangle.Y -= (int)this.speed.Y;
                        if (this.IsCollidedWith(drawable))
                        {
                            match = true;
                            this.rectangle = initial;
                            while (this.IsCollidedWith(drawable))
                            {
                                this.rectangle.Y += 1;
                            }
                            break;
                        }
                        if (this.rectangle.Y < 0)
                        {
                            this.rectangle.Y = 1;
                        }
                        break;
                    case Keys.Down:
                        this.rectangle.Y += (int)this.speed.Y;
                        if (this.IsCollidedWith(drawable))
                        {
                            match = true;
                            this.rectangle = initial;
                            while (this.IsCollidedWith(drawable))
                            {
                                this.rectangle.Y -= 1;
                            }
                            break;
                        }
                        if (this.rectangle.Bottom == GlobalConstants.WINDOW_HEIGHT)
                        {
                            break;
                        }
                        if (this.rectangle.Bottom + (int)this.speed.Y > GlobalConstants.WINDOW_HEIGHT)
                        {
                            this.rectangle.Y = GlobalConstants.WINDOW_HEIGHT - this.rectangle.Height - 1;
                            break;
                        }
                         
                        break;
                }
                if(match)
                {
                    return;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.rectangle, Color.White);
        }

        public bool IsCollidedWith(PirateGame.Interfaces.IDrawableCustom obj)
        {
            int top = Math.Max(this.rectangle.Top, obj.Rectangle.Top);
            int bottom = Math.Min(this.rectangle.Bottom, obj.Rectangle.Bottom);
            int left = Math.Max(this.rectangle.Left, obj.Rectangle.Left);
            int right = Math.Min(this.rectangle.Right, obj.Rectangle.Right);
            if (this.rectangle.Intersects(obj.Rectangle))
            {
                Color[] shipTextureData = new Color[this.Texture.Width * this.Texture.Height];
                this.Texture.GetData(shipTextureData);
                Color[] objTextureData = new Color[obj.Texture.Width * obj.Texture.Height];
                obj.Texture.GetData(objTextureData);

                for (int y = top; y < bottom; y++)
                {
                    for (int x = left; x < right; x++)
                    {
                        Color colorA = shipTextureData[(x - this.rectangle.Left) +
                                                       (y - this.rectangle.Top) * this.rectangle.Width];
                        Color colorB = objTextureData[(x - obj.Rectangle.Left) +
                                                      (y - obj.Rectangle.Top) * obj.Rectangle.Width];

                        if (colorA.A != 0 && colorB.A != 0)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}