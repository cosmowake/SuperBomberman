using System;
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
        Point Position;
        public int Power { get; private set; }

        public event Action ExplosionBomb;

        DelayedAction explosionDelayed;

        public Bomb(string spritePath, Vector2 position, int tileSize)
        {
            if (animationImage == null)
            { 
                Image imageTemp = new Image(spritePath,new Rectangle(0,0,tileSize, tileSize), position);
                animationImage = new AnimationImage(imageTemp, new List<int>(new int[4] { 0, 1, 2, 1 }));
            }
            explosionDelayed = new DelayedAction(
                new Action(() => 
                {
                    ExplosionBomb();
                    
                    UnloadContent();
                    
                }),
                4000);
        }

        public void LoadContent()
        {
            animationImage.LoadContent();
            animationImage.StartAnimation();
            explosionDelayed.Start();
        }

        public void UnloadContent()
        {
            animationImage.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            animationImage.Update(gameTime);
            explosionDelayed.Update(gameTime.ElapsedGameTime.Milliseconds);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animationImage.Draw(spriteBatch);
        }
    }
}
