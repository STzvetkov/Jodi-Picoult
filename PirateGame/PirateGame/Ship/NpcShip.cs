﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using PirateGame.Enums;
using PirateGame.Interfaces;

namespace PirateGame.Ship
{
    internal class NpcShip : Ship, IDrawableCustom
    {
        private static Random rnd;
        private double time;
        private MoveAction handler;

        public NpcShip(ContentManager content, string texture, int x, int y) : base(content, texture, x, y)
        {
            this.time = 0;
            this.handler = new MoveAction(this.MoveRight);
            this.IsInCombat = false;
        }

        private delegate void MoveAction(List<IDrawableCustom> d);

        public bool IsInCombat { get; set; }

        public override void Update(Ship playerShip, ref GameState gameState, GameTime gameTime)
        {
            if (gameState == GameState.FreeRoam)
            {
                if (this.Rectangle.Intersects(playerShip.Rectangle))
                {
                    gameState = GameState.Combat;
                    this.IsInCombat = true;
                }
            }
            if (gameState == GameState.Combat)
            {
                this.RandomMovement(gameTime, playerShip);
            }
            base.Update(playerShip, ref gameState, gameTime);
        }

        private void RandomMovement(GameTime gameTime, Ship playership)
        {
            List<IDrawableCustom> ships = new List<IDrawableCustom> { playership };
            rnd = new Random();
            int direction = rnd.Next(1, 5);
            int fireDelay = rnd.Next(1, 3);
            if (Math.Abs(gameTime.TotalGameTime.TotalSeconds - this.time) > fireDelay)
            {
                this.Fire(gameTime);
            }
            if (Math.Abs(gameTime.TotalGameTime.TotalSeconds - this.time) > 1)
            {
                switch (direction)
                {
                    case 1:
                        this.handler = new MoveAction(this.MoveUp);
                        this.time = gameTime.TotalGameTime.TotalSeconds;
                        break;
                    case 2:
                        this.handler = new MoveAction(this.MoveDown);
                        this.time = gameTime.TotalGameTime.TotalSeconds;
                        break;
                    case 3:
                        this.handler = new MoveAction(this.MoveRight);
                        this.time = gameTime.TotalGameTime.TotalSeconds;
                        break;
                    case 4:
                        this.handler = new MoveAction(this.MoveLeft);
                        this.time = gameTime.TotalGameTime.TotalSeconds;
                        break;
                }
            }
            this.handler(ships);
        }

        private void MoveLeft(List<IDrawableCustom> ships)
        {
            this.Move(Keys.Left, ships);
        }

        private void MoveRight(List<IDrawableCustom> ships)
        {
            this.Move(Keys.Right, ships);
        }

        private void MoveUp(List<IDrawableCustom> ships)
        {
            this.Move(Keys.Up, ships);
        }

        private void MoveDown(List<IDrawableCustom> ships)
        {
            this.Move(Keys.Down, ships);
        }
    }
}