using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperBomberman
{
    class ScreenManager
    {
        private static ScreenManager instance;
        public Vector2 Dimensions { private set; get; }
        public ContentManager Content { private set; get; }

        public GraphicsDevice GraphicsDevice;
        public SpriteBatch SpriteBatch;
        
        GameScreen gameScreen;

        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScreenManager();
                    
                }
                return instance;
            }
        }

        public ScreenManager()
        {
            Dimensions = new Vector2(1200, 720);
            gameScreen = new PlayScreen();
        }

        public void ChangeScreen(GameScreen newGameScreen)
        {
            Content.Unload();
            gameScreen.UnloadContent();
            gameScreen = newGameScreen;
            gameScreen.LoadContent();
        }



        public void LoadContent(ContentManager content)
        {
            Content = new ContentManager(content.ServiceProvider, "Content");
            gameScreen.LoadContent();
        }

        public void UnloadContent()
        {
            gameScreen.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            gameScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            gameScreen.Draw(spriteBatch);
        }
    }
}
