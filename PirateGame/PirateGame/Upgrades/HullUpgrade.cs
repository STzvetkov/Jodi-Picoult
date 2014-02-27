using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PirateGame.Interfaces;
using PirateGame.Ships;

namespace PirateGame.Upgrades
{
    internal class HullUpgrade : Upgrade, IUpgrade
    {
        public HullUpgrade(ContentManager content, string font, string text, Ship ship) : base(content, font, text,ship)
        {
        }

        public override void UpgradeShip()
        {
            if ((int)this.ship.Hull < 3)
            {
                this.ship.Hull++;
            }
        }
    }
}
