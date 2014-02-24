using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace PirateGame.MapObjects
{
    class Wreckage:SeaObject
    {
        public Wreckage(ContentManager content, string texture, int x, int y, int width, int height) : base(content, texture, x, y, width, height)
        {
        }
    }
}
