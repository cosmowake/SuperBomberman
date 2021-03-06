﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SuperBomberman
{
    class Bomb
    {
        AnimationImage animationImage;
        public int Power { get; private set; }
        public bool IsDetonatorBomb;
        Rectangle collisionRectangle;
        public Vector2 Position { get { return new Vector2(collisionRectangle.X, collisionRectangle.Y); } }

        public bool IsDestructible { get; private set; }
        public bool IsSolid { get; set; }

        public Action ExplosionBomb;

        DelayedAction explosionDelayed;
        public DelayedAction explosionAfterDelayed;
        public Bomb(string spritePath, Vector2 position, int tileSize, bool isDetonatorBomb)
        {
            if (animationImage == null)
            {
                IsDetonatorBomb = isDetonatorBomb;
                if (!IsDetonatorBomb)
                {
                    Image imageTemp = new Image(spritePath, new Rectangle(0, 0, tileSize, tileSize), position);
                    animationImage = new AnimationImage(imageTemp, new List<int>(new int[] { 0, 1, 2, 1 }));
                }
                else
                {
                    Image imageTemp = new Image(spritePath, new Rectangle(0, 1 * tileSize, tileSize, tileSize), position);
                    animationImage = new AnimationImage(imageTemp, new List<int>(new int[] { 0, 1}));
                }
                collisionRectangle = new Rectangle((int)position.X, (int)position.Y, tileSize, tileSize);

                IsDestructible = true;
                IsSolid = false;
            }

            explosionAfterDelayed = new DelayedAction(
                new Action(() =>
                {
                    ExplosionBomb();
                    UnloadContent();

                }),
                100);

            if (!IsDetonatorBomb)
            {

                explosionDelayed = new DelayedAction(
                    new Action(() =>
                    {
                        explosionAfterDelayed.Start();
                    }),
                    3000);
            }
        }

        public void LoadContent()
        {
            animationImage.LoadContent();
            animationImage.StartAnimation();
            if (explosionDelayed != null)
            {
                explosionDelayed.Start();
            }
        }

        public void UnloadContent()
        {
            animationImage.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            animationImage.Update(gameTime); 
            if (explosionDelayed != null)
            {
                explosionDelayed.Update(gameTime.ElapsedGameTime.Milliseconds);
            }
            explosionAfterDelayed.Update(gameTime.ElapsedGameTime.Milliseconds);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animationImage.Draw(spriteBatch);
        }
    }
}
