using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PirateGame.Ship;

namespace PirateGame.MapObjects
{

    public abstract class Settlement : LandObject
    {
        // fields
        private List<PlayerShip> shipsOnDock = new List<PlayerShip>(); // this field remains hidden as it will be used only within methods of the class       

        // constructors
        public Settlement(ContentManager content, string texture, int x, int y, int width, int height)
            : this(1, 0, 1, Coutries.Somalia, content, texture, x, y, width, height)
        { }

        public Settlement(int startPopulation, int initialWealth, int defence, Coutries stCountry, 
            ContentManager content, string texture, int x, int y, int width, int height):base(content, texture, x, y, width, height)
        {
            this.Population = startPopulation;
            this.Wealth = initialWealth;
            this.DefencePower = defence;
            this.Country = stCountry;
        }

        // abstract methods
        

        // virtual methods
        public virtual  PlayerShip BuildShip(ContentManager content, string texture) // ship details to be added here
        {
            return new PlayerShip(content, texture, this.LocationX, this.LocationY);
        }
        public virtual PlayerShip SendShip(ContentManager content, string texture)
        {
            //this.shipsOnDock.Find();
            return null;
        }


        // properties
        public int Population { get; private set; }
        public int Wealth { get; private set; }
        public int DefencePower { get; private set; }
        public Coutries Country { get; set; }

        
    }
}
