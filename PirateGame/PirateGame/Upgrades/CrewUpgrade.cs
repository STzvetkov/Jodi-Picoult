using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PirateGame.Stuff
{
    class CrewUpgrade:Upgrade
    {
        public CrewUpgrade(ContentManager content, string font, Vector2 position, string text) : base(content, font, position, text)
        {
        }
    }
}
