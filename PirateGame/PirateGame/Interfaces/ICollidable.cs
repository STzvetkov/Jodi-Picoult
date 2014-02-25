using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PirateGame.MapObjects;

namespace PirateGame.Interfaces
{
    interface ICollidable:IMovable
    {
        bool IsCollidedWith(PirateGame.Interfaces.IDrawableCustom obj);
    }
}
