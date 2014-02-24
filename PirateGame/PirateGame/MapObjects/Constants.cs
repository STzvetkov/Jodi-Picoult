using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PirateGame.MapObjects
{
    // Container for global constants and enumerations

    // define enumerations
    public enum Coutries { Kenya, Oman, Somalia, Tanzania, Yemen };
    public enum ProductionGoods { Fish, Oil, Fruits, Cereals};
    public class Constants
    {
        // use private constructor to avoid instantiation of or inheritance
        private Constants()
        { }

        // define the constants:

        public const int MapSizeX = 500; // width of map in playing sectors
        public const int MapSizeY = 500; // height of map in playing sectors

    }
}
