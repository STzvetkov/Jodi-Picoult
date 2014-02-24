using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PirateGame.MapObjects
{
    public abstract class MapObject:PirateGame.Interfaces.IDrawable
    {
        public Rectangle Rectangle { get; private set; }

        public Texture2D Texture { get;private set; }

        protected MapObject(ContentManager content, string texture, int x, int y, int width, int height)
        {
            this.Texture = content.Load<Texture2D>(texture);
            this.Rectangle = new Rectangle(x,y,width,height);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Rectangle, Color.White);
        }
    }
}
