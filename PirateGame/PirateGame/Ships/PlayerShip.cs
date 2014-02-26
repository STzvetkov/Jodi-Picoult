using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using PirateGame.Interfaces;
using PirateGame.Ships;
using PirateGame.Upgrades;

namespace PirateGame.Ships
{
    public class PlayerShip : Ship
    {
        private List<Upgrade> upgrades;

        public PlayerShip(ContentManager content, string texture, int x, int y) : base(content,texture,x,y)
        {
            this.upgrades = new List<Upgrade>();
        }

        public void Upgrade(Upgrade upg)
        {
            this.upgrades.Add(upg);
        }

        public Upgrade[] GetUpgrades()
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
    }
}