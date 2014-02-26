using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PirateGame.Interfaces
{
    interface IDestroyable
    {
        int Hitpoints { get; }
        bool IsDestroyed { get; }

        void TakeDamage(int damageCaused);

    }
}
