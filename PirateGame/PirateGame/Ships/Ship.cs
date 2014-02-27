using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PirateGame;
using PirateGame.Enums;
using PirateGame.Interfaces;
using PirateGame.Ships;

namespace PirateGame.Ships
{
    public abstract class Ship : ICollidable, IDestroyable
    {
        private const int MaxHitpoints = 200;

        private const int InitialDamage = 40;

        private Vector2 speed;

        protected Rectangle rectangle;

        private ContentManager content;

        protected double fireTime;

        protected Vector2 initialCoordinates;

        protected Ship(ContentManager content, string texture, int x, int y)
        {
            this.content = content;
            this.Texture = this.content.Load<Texture2D>(texture);
            this.rectangle = new Rectangle(x,y,40,40);
            this.speed = new Vector2(1,1);
            this.Weapons = Weapons.Basic;
            this.Hull = Hull.Basic;
            this.IsDestroyed = false;
            this.Hitpoints = MaxHitpoints*(int)this.Hull;
            this.Damage = InitialDamage*(int)this.Weapons;
            this.Bullets = new List<Projectile>();
            this.fireTime = 0;
            this.initialCoordinates.X = x;
            this.initialCoordinates.Y = y;
        }

        public List<Projectile> Bullets { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return this.rectangle;
            }
        }

        public int Damage { get; private set; }
        
        public Texture2D Texture { get; private set; }

        public Weapons Weapons { get; set; }

        public Hull Hull { get; set; }

        public void ResetHitpoints()
        {
            this.Hitpoints = MaxHitpoints;
        }

        public Point CurrentPosition
        {
            get
            {
                return this.rectangle.Center;
            }
        }

        public int Hitpoints { get; protected set; }

        public bool IsDestroyed { get; private set; }

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
                            while (this.IsCollidedWith(drawable))
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
                if (match)
                {
                    return;
                }
            }
        }

        public void TakeDamage(int damageCaused)
        {
              this.Hitpoints -= damageCaused/(int)this.Hull;
            this.IsDestroyed = this.Hitpoints <= 0;
        }

        public void Fire(GameTime gameTime)
        {
            if (Math.Abs(gameTime.TotalGameTime.TotalSeconds - this.fireTime) > 1)
            {
                this.fireTime = gameTime.TotalGameTime.TotalSeconds;
                 this.Bullets.Add(new Projectile(this.content, this));
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

        public virtual void Update(Ship target, ref GameState gameState, GameTime gameTime)
        {
            if (gameState==GameState.Combat)
            {
                for (int i = this.Bullets.Count - 1; i >= 0; i--)
                {
                    if (this.Bullets[i].Hit)
                    {
                        target.TakeDamage(this.Damage);
                        if (target.IsDestroyed)
                        {
                            this.rectangle.X = (int)target.initialCoordinates.X;
                            this.rectangle.Y = (int)target.initialCoordinates.Y;
                            this.Bullets.Clear();
                            gameState = GameState.FreeRoam;
                            return;
                        }
                        this.Bullets.RemoveAt(i);
                    }
                }
                foreach (var item in this.Bullets)
                {
                    item.Update(target, this);
                }
            }
        }

        public void AdjustPos(int x, int y)
        {
            this.rectangle.X = x;
            this.rectangle.Y = y;
        }
    }
}