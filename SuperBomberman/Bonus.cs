﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBomberman
{
    class Bonus
    {
        public AnimationImage Image;

        BonusType bonusType;
        public Action Destroy;

        public Bonus(BonusType bType, Vector2 position, int tileSize)
        {
            Rectangle rect = new Rectangle();
            bonusType = bType;
            switch (bonusType)
            {
                case BonusType.Count:
                    {
                        rect = new Rectangle(0 * tileSize, 0 * tileSize, tileSize, tileSize);
                        break;
                    }
                case BonusType.Power:
                    {
                        rect = new Rectangle(0 * tileSize, 1 * tileSize, tileSize, tileSize);
                        break;
                    }
                case BonusType.Move:
                    {
                        rect = new Rectangle(0 * tileSize, 2 * tileSize, tileSize, tileSize);
                        break;
                    }
                case BonusType.Detonator:
                    {
                        rect = new Rectangle(0 * tileSize, 3 * tileSize, tileSize, tileSize);
                        break;
                    }
            }

            Image imageTemp = new Image("Play/Bonus3x", rect, position);
            Image = new AnimationImage(imageTemp, new List<int>(new int[] { 0, 1 }));
            Image.SwitchTime = 200;
            Image.StartAnimation();
        }

        public void GetBonusToPlayer(ref Player player)
        {
            switch (bonusType)
            {
                case BonusType.Count:
                    {
                        player.bombMaxCount++;
                        break;
                    }
                case BonusType.Power:
                    {
                        player.power++;
                        break;
                    }
                case BonusType.Move:
                    {
                        if(player.MoveSpeed < 300)
                        {
                            player.MoveSpeed += 50;
                        }
                        break;
                    }
                case BonusType.Detonator:
                    {
                        player.isDetonatorBombs = true;
                        break;
                    }
            }
            Destroy();
        }

        public void LoadContent()
        {
            Image.LoadContent();
        }

        public void UnloadContent()
        {
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            Image.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }
    }

    public enum BonusType { Count, Power, Move, Detonator }
}
