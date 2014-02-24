using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PirateGame.MapObjects
{
    public abstract class MapObject
    {
        // fields

        private int locationX;
        private int locationY;

        // properties
        public int LocationX
        {
            get { return this.locationX; }
            set
            {
                if (value < 0 || value > Constants.MapSizeX)
                {
                    throw new IndexOutOfRangeException("The provided coordinates aren't within the map!");
                }
                this.locationX = value;
            }
        }
        public int LocationY
        {
            get { return this.locationY; }
            set
            {
                if (value < 0 || value > Constants.MapSizeY)
                {
                    throw new IndexOutOfRangeException("The provided coordinates aren't within the map!");
                }
                this.locationY = value;
            }
        }
    }
}
