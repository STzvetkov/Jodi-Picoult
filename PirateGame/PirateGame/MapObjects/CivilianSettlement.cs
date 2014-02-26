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

    public abstract class CivilianSettlement:Settlement
    {
        // fields
        private int goodsAmount;

        // constructors

        protected CivilianSettlement(int startPopulation, int initialWealth, int defence, Coutries stCountry, 
                                     ContentManager content, string texture, int x, int y, int width, int height)
            : base(startPopulation, initialWealth, defence, stCountry, content, texture, x, y, width, height)
        {
            this.GoodsAmount = 0;
            this.ProductionRate = 10;
            this.StorageCapacity = 1000;
            this.ProductionType = ProductionGoods.Cereals;
        }
        protected CivilianSettlement(int initialGoodsAmount, int initialProductionRate, int initialStorageCapacity,
            int startPopulation, int initialWealth, int defence, Coutries stCountry,
            ContentManager content, string texture, int x, int y, int width, int height)
            : this(startPopulation, initialWealth, defence, stCountry, content, texture, x, y, width, height)
        {
            this.StorageCapacity = initialStorageCapacity;
            this.GoodsAmount = initialGoodsAmount;
            this.ProductionRate = initialProductionRate;            
            this.ProductionType = ProductionGoods.TradingGoods;
        }
       
        // properties
        public ProductionGoods ProductionType { get; protected set; }
        public int ProductionRate { get; protected set; }
        public int StorageCapacity { get; protected set; }

        public int GoodsAmount
        {
            get { return this.goodsAmount; }
            set 
            {
                if (value < 0)
                    throw new IndexOutOfRangeException("Sorage can not contain negative amount of goods.");
                else
                    if (value > this.StorageCapacity)
                    {
                        this.goodsAmount = this.StorageCapacity;
                        throw new IndexOutOfRangeException("Sorage is filled to its capacity.");
                    }

                this.goodsAmount = value;
            }
        }

        // methods
        public virtual void PoduceGoods()
        {
            int accumulatedAmount = this.GoodsAmount + this.ProductionRate;
            this.GoodsAmount = accumulatedAmount <= this.StorageCapacity ? accumulatedAmount : this.StorageCapacity;
        }
    }
}
