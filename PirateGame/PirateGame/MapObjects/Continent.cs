using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PirateGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PirateGame.MapObjects
{
    class Continent:LandObject
    {
        public Continent(ContentManager content, string texture, int x, int y, int width, int height) : base(content, texture, x, y, width, height)
        {
        }
    }
}
