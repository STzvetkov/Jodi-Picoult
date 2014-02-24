using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PirateGame.MapObjects
{
    public abstract class SeaObject:MapObject
    {
        public SeaObject(ContentManager content, string texture, int x, int y, int width, int height) : base(content, texture, x, y, width, height)
        {
        }
    }
}
