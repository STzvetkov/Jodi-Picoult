using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PirateGame.Ships;

namespace PirateGame.Upgrades
{
    internal class CrewUpgrade : Upgrade
    {
        public CrewUpgrade(ContentManager content, string font, string text, Ship ship) : base(content, font, text, ship)
        {
        }

        public override void UpgradeShip()
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
    }
}
