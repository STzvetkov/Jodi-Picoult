using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PirateGame.Stuff
{
    class WeaponUpgrade:Upgrade
    {
        public WeaponUpgrade(ContentManager content, string font, Vector2 position, string text) : base(content, font, position, text)
        {

        }
    }
}
