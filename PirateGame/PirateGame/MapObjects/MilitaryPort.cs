using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PirateGame.Enums;

namespace PirateGame.MapObjects
{
    public class MilitaryPort:MilitarySettlement
    {
        public MilitaryPort(ContentManager content, string texture, int x, int y, int width, int height)
            : base(content, texture, x, y, width, height)
        {}

        public MilitaryPort (int shipsNumber,
                             int initialAttack, int influence,
                             int startPopulation, int initialWealth, int defence, Coutries stCountry,
                             ContentManager content, string texture, int x, int y, int width, int height)
              : base(initialAttack, influence, startPopulation, initialWealth, defence, stCountry, content, texture, x, y, width, height)
        {
            this.ShipsOnDock = shipsNumber;
        }

        public int ShipsOnDock { get; private set; }

    }
}
