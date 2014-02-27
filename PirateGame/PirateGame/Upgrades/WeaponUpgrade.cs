using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PirateGame.Ships;
using PirateGame.Enums;

namespace PirateGame.Upgrades
{
    internal class WeaponUpgrade : Upgrade
    {
        public WeaponUpgrade(ContentManager content, string font, string text, Ship ship) : base(content, font, text,ship)
        {
        }

        public override void UpgradeShip()
        {
            if ((int)this.ship.Weapons < 3)
            {
                this.ship.Weapons++;
            }
        }

    }
}