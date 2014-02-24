using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PirateGame.MapObjects
{

    public abstract class CivilianSettlement:Settlement
    {
        // fields
        private int goodsAmount;

        // constructors

        public CivilianSettlement (ContentManager content, string texture, int x, int y, int width, int height)
            : base(content, texture, x, y, width, height)
        {}
        // methods
        public virtual void PoduceGoods ()
        {
            this.GoodsAmount += this.ProductionRate;
        }

        // properties
        public ProductionGoods productionType { get; private set; }
        public int ProductionRate { get; private set; }

        public int GoodsAmount
        {
            get { return this.goodsAmount; }
            set 
            {
                if (value < 0)
                    throw new IndexOutOfRangeException("Sorage can not contain negative amount of goods.");
                this.goodsAmount = value;
            }
        }
    }
}
