using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PirateGame.Ships;
using PirateGame.Upgrades;

namespace PirateGame.Upgrades
{
    class CrewUpgrade:Upgrade
    {
        public CrewUpgrade(ContentManager content, string font, Vector2 position, string text, Ship ship)
            : base(content, font, position, text, ship)
        {
        }
        public override void UpgradeShip()
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
    }
}
