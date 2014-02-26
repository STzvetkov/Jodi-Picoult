using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PirateGame.Stuff
{
    public abstract class Upgrade
    {
        private SpriteFont font;
        private Vector2 position;
        private string text;

        public Upgrade(ContentManager content, string font, Vector2 position,string text)
        {
            this.font = content.Load<SpriteFont>(font);
            this.position = position;
            this.text = text;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(this.font, text, this.position, Color.Black);
        }
    }
}