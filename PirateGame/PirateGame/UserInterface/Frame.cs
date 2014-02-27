namespace PirateGame.UserInterface
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public class Frame : Decorator
    {
        public const int Thickness = 5;

        public Frame(UserInterfaceElement element, Texture2D frameTexture, Color frameColor)
            : base(element)
        {
            this.FrameTexture = frameTexture;
            this.FrameColor = frameColor;
        }

        public Color FrameColor { get; set; }

        public Texture2D FrameTexture
        {
            get
            {
                return this.Texture;
            }
            private set
            {
                if (value == null)
                {
                    throw new InvalidTextureException("Frame texture can't be null", value);
                }

                this.Texture = value;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.Visible && (spriteBatch != null))
            {
                this.Rectangle = new Rectangle(this.element.Rectangle.X - Frame.Thickness,
                                           this.element.Rectangle.Y - Frame.Thickness,
                                           this.element.Rectangle.Width + Frame.Thickness * 2,
                                           this.element.Rectangle.Height + Frame.Thickness * 2);
                spriteBatch.Draw(this.FrameTexture, this.Rectangle, this.FrameColor);
            }

            base.Draw(spriteBatch);
        }
    }
}
