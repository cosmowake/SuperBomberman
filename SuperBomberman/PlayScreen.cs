using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperBomberman
{
    class PlayScreen : GameScreen
    {
        Map map;


        public override void LoadContent()
        {
            base.LoadContent();

            map = new Map("Play/MapTile1_3x","Play/Explosion3x");
            map.LoadContent();

            
            
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            map.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            map.Draw(spriteBatch);
            
        }
    }
}
