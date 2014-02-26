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
    public abstract class MilitarySettlement : Settlement
    {
        public MilitarySettlement(ContentManager content, string texture, int x, int y, int width, int height)
            : base(content, texture, x, y, width, height)
        { }
        public MilitarySettlement(int initialAttack, int influence,
                                  int startPopulation, int initialWealth, int defence, Coutries stCountry,
                                  ContentManager content, string texture, int x, int y, int width, int height)
            : base(startPopulation, initialWealth, defence, stCountry, content, texture, x, y, width, height)
        {
            this.AttackPower = initialAttack;
            this.InfluenceArea = influence;
        }

        // properties

        public int AttackPower { get; private set; }
        public int InfluenceArea { get; private set; }
    }
}
