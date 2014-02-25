using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PirateGame.Enums;
using PirateGame.Interfaces;

namespace PirateGame.Popups
{
    public class Popup : IDrawableCustom
    {
        private PopupPosition position;

        private IDrawableCustom owner;

        private Rectangle backgroundRect;

        public Popup(ContentManager manager, String background, String font, List<string> messages, PirateGame.Interfaces.IDrawableCustom owner)
        {
            this.IsVisible = false;
            this.Texture = manager.Load<Texture2D>(background);
            this.Font = manager.Load<SpriteFont>(font);
            this.Messages = messages;
            this.owner = owner;
            this.backgroundRect = new Rectangle();
        }

        public bool IsVisible { get; set; }

        public Texture2D Texture { get; set; }

        public List<string> Messages { get; set; }

        public SpriteFont Font { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return this.backgroundRect;
            }
        }

        public void Draw(SpriteBatch spriteBach)
        {
            if (this.IsVisible)
            {
                if (this.owner.Rectangle.Top < 200 && this.owner.Rectangle.Left < 200)
                {
                    this.position = PopupPosition.Bottom;
                    this.backgroundRect = new Rectangle(this.owner.Rectangle.X + 100, this.owner.Rectangle.Y + 70, 150, 150);
                }
                else if (this.owner.Rectangle.Top < 200 && GlobalConstants.WINDOW_WIDTH - this.owner.Rectangle.Right < 200)
                {
                    this.position = PopupPosition.Bottom;
                    this.backgroundRect = new Rectangle(this.owner.Rectangle.X - 140, this.owner.Rectangle.Y + 70, 150, 150);
                }
                else if (GlobalConstants.WINDOW_HEIGHT - this.owner.Rectangle.Bottom < 200 && this.owner.Rectangle.Left < 200)
                {
                    this.position = PopupPosition.Top;
                    this.backgroundRect = new Rectangle(this.owner.Rectangle.X + 100, this.owner.Rectangle.Y - 170, 150, 150);
                }
                else if (GlobalConstants.WINDOW_HEIGHT - this.owner.Rectangle.Bottom < 200 && GlobalConstants.WINDOW_WIDTH - this.owner.Rectangle.Right < 200)
                {
                    this.position = PopupPosition.Top;
                    this.backgroundRect = new Rectangle(this.owner.Rectangle.X - 140, this.owner.Rectangle.Y - 170, 150, 150);
                }
                else if (GlobalConstants.WINDOW_WIDTH - this.owner.Rectangle.Right < 400 && this.owner.Rectangle.Top > 300 && this.owner.Rectangle.Top < 400)
                {
                    this.backgroundRect = new Rectangle(this.owner.Rectangle.X - 170, this.owner.Rectangle.Y - 50, 150, 150);
                    this.position = PopupPosition.Left;
                }
                else if (this.owner.Rectangle.Left < 300 && this.owner.Rectangle.Top > 300 && this.owner.Rectangle.Top < 600)
                {
                    this.backgroundRect = new Rectangle(this.owner.Rectangle.X + 70, this.owner.Rectangle.Y - 50, 150, 150);
                    this.position = PopupPosition.Right;
                }
                else if (this.owner.Rectangle.Top > 200)
                {
                    this.position = PopupPosition.Top;
                    this.backgroundRect = new Rectangle(this.owner.Rectangle.X - 50, this.owner.Rectangle.Y - 170, 150, 150);
                }
                else if (GlobalConstants.WINDOW_HEIGHT - this.owner.Rectangle.Bottom > 200)
                {
                    this.position = PopupPosition.Bottom;
                    this.backgroundRect = new Rectangle(this.owner.Rectangle.X - 50, this.owner.Rectangle.Y + 70, 150, 150);
                }
                    
                Vector2 text = new Vector2(this.backgroundRect.X + 5,this.backgroundRect.Y + 15);
                spriteBach.Draw(this.Texture, this.backgroundRect, Color.White);
                foreach (var item in this.Messages)
                {
                    spriteBach.DrawString(this.Font, item, text, Color.Red);
                    text.Y += 20;
                }
            }
        }
    }
}