namespace PirateGame
{
    using System;

    public class MenuItem
    {
        private string title;

        // An event that clients can use to be notified when the menu item is selected
        private event EventHandler itemSelectedHandler;

        public MenuItem(string title, EventHandler itemSelected)
        {
            this.Title = title;
            this.AddHandler(itemSelected);
        }

        // Item title property
        public string Title
        {
            get
            {
                return this.title;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Title can't be null or empty string.");
                }
                this.title = value;
            }
        }

        // Add item selected handler
        public void AddHandler(EventHandler itemSelected)
        {
            this.itemSelectedHandler += itemSelected;
        }

        // Select item method - calls all handlers
        public void Select()
        {
            if (itemSelectedHandler != null)
            {
                itemSelectedHandler(this, null);
            }
        }
    }
}
