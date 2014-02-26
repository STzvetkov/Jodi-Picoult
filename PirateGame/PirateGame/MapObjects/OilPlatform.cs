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
    public class OilPlatform:CivilianSettlement
    {

        //constructors
        public OilPlatform(int initialDeposit,
                           int initialGoodsAmount, int initialProductionRate, int initialStorageCapacity, 
                           int startPopulation, int initialWealth, int defence, Coutries stCountry, 
                           ContentManager content, string texture, int x, int y, int width, int height)
            : base( initialGoodsAmount, initialProductionRate, initialStorageCapacity, startPopulation, initialWealth, defence, stCountry, content, texture, x, y, width, height)
        {
            this.ProductionType = ProductionGoods.Oil;
            this.OilDeposit = initialDeposit;
        }

        // properties
        public int OilDeposit { get; private set; }

        // methods

        public override void ProduceGoods(GameTime gameTime)
        {
            if (this.OilDeposit > 0)
            {
                base.ProduceGoods(gameTime);
                if(CheckTime(gameTime, 5))
                this.OilDeposit -= this.ProductionRate;
            }
        }
    }
}
