using System;

namespace PirateGame.UserInterface
{
    public class SelectableItem<T>
    {
        private T item;

        // An event that clients can use to be notified when the menu item is selected
        private event EventHandler itemSelectedHandler;

        public SelectableItem(T item, EventHandler itemSelected)
        {
            this.Item = item;
            this.AddHandler(itemSelected);
        }

        // Item property
        public T Item
        {
            get
            {
                return this.item;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentException("Item can't be null.");
                }
                this.item = value;
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
