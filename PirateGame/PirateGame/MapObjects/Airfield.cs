using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PirateGame.MapObjects
{
    public class Airfield:MilitarySettlement
    {
        public Airfield(ContentManager content, string texture, int x, int y, int width, int height)
            : base(content, texture, x, y, width, height)
        {}
    }
}
