﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PirateGame.Interfaces
{
    public interface IMovable
    {
        void Move(Keys key,int width, int height);
    }
}
