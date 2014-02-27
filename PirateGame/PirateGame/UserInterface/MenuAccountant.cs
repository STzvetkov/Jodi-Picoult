namespace PirateGame.UserInterface
{
    using System;

    public struct MenuAccountant
    {
        public int openCount;

        public bool AllClosed
        {
            get
            {
                if (this.openCount == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void OnOpen(object menuItem = null, EventArgs e = null)
        {
            openCount += 1;
        }

        public void OnClose(object menuItem = null, EventArgs e = null)
        {
            openCount -= 1;
        }
    }
}
