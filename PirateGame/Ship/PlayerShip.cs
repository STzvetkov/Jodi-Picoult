using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using PirateGame;
using PirateGame.Interfaces;
using PirateGame.Stuff;

namespace PirateGame.Ship
{
    public class PlayerShip : Ship,IUpgradable
    {
        private List<Upgrade> upgrades;

        public PlayerShip(ContentManager content, string texture, int x, int y) : base(content,texture,x,y)
        {
            this.upgrades = new List<Upgrade>();
        }
        public void Upgrade(Upgrade upg)
        {
            this.upgrades.Add(upg);
            Actualize();
        }

        private void Actualize()
        {
            
        }
        public Upgrade[] GetUpgrades()
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
    }
}
