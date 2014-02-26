using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using PirateGame.Interfaces;
using PirateGame.Ships;

namespace PirateGame.Upgrades
{
    class HullUpgrade:Upgrade,IUpgrade
    {
        public HullUpgrade(ContentManager content, string font, Vector2 position, string text, Ship ship) : base(content, font, position, text,ship)
        {
        }
        public override void UpgradeShip()
        {
            if ((int)ship.Hull < 3)
                ship.Hull++;
        }
    }
}
