using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBomberman
{
    class Teleport
    {
        public AnimationImage Image;

        public Teleport(Vector2 position, int tileSize)
        {
            Rectangle rect = new Rectangle(0,0, tileSize, tileSize);

            Image imgTemp = new Image("Play/Teleport3x", rect, position);
            Image = new AnimationImage(imgTemp,new List<int>( new int[]{ 0, 1}));
            //Image.SwitchTime = 100;
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
}
