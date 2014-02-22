using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PirateGame.Stuff;

namespace PirateGame.Interfaces
{
    interface IUpgradable
    {
        void Upgrade(Upgrade upg);

        Upgrade[] GetUpgrades();
    }
}
