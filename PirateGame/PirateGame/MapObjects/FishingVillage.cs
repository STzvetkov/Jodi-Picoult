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
    public class FishingVillage : CivilianSettlement
    {
        // contructor
        public FishingVillage(int initialFishfactor,
                              int initialGoodsAmount, int initialProductionRate, int initialStorageCapacity,
                              int startPopulation, int initialWealth, int defence, Coutries stCountry,
                              ContentManager content, string texture, int x, int y, int width, int height)
            : base(initialGoodsAmount, initialProductionRate, initialStorageCapacity, startPopulation, initialWealth, defence, stCountry, content, texture, x, y, width, height)
        {
            this.ProductionType = ProductionGoods.Fish;
            this.FishPopulationFactor = initialFishfactor;
        }

        // properties:
        public int FishPopulationFactor { get; private set; }

        // methods:
        public override void PoduceGoods()
        {
            int accumulatedAmount = this.GoodsAmount + this.ProductionRate*this.FishPopulationFactor;
            this.GoodsAmount = accumulatedAmount <= this.StorageCapacity ? accumulatedAmount : this.StorageCapacity;
        }

    }
}
