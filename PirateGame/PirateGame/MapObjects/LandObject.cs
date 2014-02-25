using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PirateGame.Interfaces;

namespace PirateGame.MapObjects
{
    public abstract class LandObject:MapObject, IDestroyable
    {

        // constructors
        public LandObject(ContentManager content, string texture, int x, int y, int width, int height) : base(content, texture, x, y, width, height)
        {
            
        }

        // properties
        public int Hitpoints { get; private set; }
        public bool IsDestroyed { get; private set; }

        // methods
        public void TakeDamage(int damageCaused)
        {
            this.Hitpoints -= damageCaused;
            this.IsDestroyed = this.Hitpoints <= 0;
        }
    }
}
