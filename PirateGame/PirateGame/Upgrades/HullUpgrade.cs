using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace PirateGame.Stuff
{
    class HullUpgrade:Upgrade
    {
        public HullUpgrade(ContentManager content, string font, Vector2 position, string text) : base(content, font, position, text)
        {
        }
    }
}
