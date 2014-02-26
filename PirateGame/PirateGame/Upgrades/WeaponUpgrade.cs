using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PirateGame.Ships;
using PirateGame.Upgrades;

namespace PirateGame.Upgrades
{
    class WeaponUpgrade:Upgrade
    {
        public WeaponUpgrade(ContentManager content, string font, Vector2 position, string text,Ship ship) : base(content, font, position, text,ship)
        {

        }
        public override void UpgradeShip()
        {
            if((int)ship.Weapons<3)
            ship.Weapons++;
        }
    }
}
