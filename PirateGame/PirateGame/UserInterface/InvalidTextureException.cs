namespace PirateGame.UserInterface
{
    using Microsoft.Xna.Framework.Graphics;
    using System;

    class InvalidTextureException : ApplicationException
    {
        private Texture2D texture;

        public InvalidTextureException(string message, Texture2D texture = null)
            : base(message)
        {
            this.Texture = texture;
        }

        public Texture2D Texture { get; private set; }
    }
}
