using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace SuperBomberman
{
    class LogoScreen : GameScreen
    {
        Image logo;

        public override void LoadContent()
        {
            base.LoadContent();

            Rectangle rect = new Rectangle(0,0,541,257);
            Vector2 pos = new Vector2((ScreenManager.Instance.Dimensions.X / 2) - (rect.Width / 2), (ScreenManager.Instance.Dimensions.Y / 2) - (rect.Height / 2));
            
            logo = new Image("Logo/logo", rect, pos);
            logo.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.Instance.KeyPressed(Keys.Space, Keys.Enter))
                ScreenManager.Instance.ChangeScreen(new MenuScreen());
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            logo.Draw(spriteBatch);
        }
    }
}
