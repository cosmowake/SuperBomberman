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

        int Level = 1;

        public PlayScreen(int level)
        {
            Level = level;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            map = new Map(Level, "Play/Explosion3x");
            map.LoadContent();
            
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.Instance.KeyDown(Keys.Escape))
            {
                ScreenManager.Instance.ChangeScreen(new MenuScreen());
            }

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
