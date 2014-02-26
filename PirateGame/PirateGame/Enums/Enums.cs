using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PirateGame.Enums
{
    public enum GameState
    {
        FreeRoam,
        Combat,
        GameOver,
        YouWin
    }
    public enum Hull
    {
        Basic=1,
        Reinforced,
        Steel
    }
    public enum Weapons
    {
        Basic=1,
        Advanced,
        Shredder
    }

    public enum PopupPosition
    {
        Top,
        Bottom,
        Left,
        Right
    }

    public enum Coutries 
    { 
        Kenya, 
        Oman, 
        Somalia, 
        Tanzania, 
        Yemen 
    };
    public enum ProductionGoods 
    { 
        Fish, 
        Oil, 
        Fruits, 
        Cereals, 
        TradingGoods
    };
}
