using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperBomberman
{
    class MenuScreen : GameScreen
    {
        Image cover;
        public class MenuItem
        {
            public string Title { set; get; }
            public Color Color { set; get; }
            public MenuAppointment Appointment { set; get; }

            public MenuItem(string title, Color color, MenuAppointment appointment)
            {
                Title = title;
                Color = color;
                Appointment = appointment;
            }
        }

        public class MenuManager
        {
            private int isActiveNumber = 0;
            public List<MenuItem> activeMenuList = new List<MenuItem>();
            private List<MenuItem> startMenuList = new List<MenuItem>();

            private static MenuManager instance;

            public static MenuManager Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new MenuManager();
                    }
                    return instance;
                }
            }

            MenuManager()
            {
                activeMenuList = startMenuList;

                startMenuList.Add(new MenuItem("Level one", Color.Blue, MenuAppointment.LevelOne));
                startMenuList.Add(new MenuItem("Level two", Color.White, MenuAppointment.LevelTwo));
                startMenuList.Add(new MenuItem("Level three", Color.White, MenuAppointment.LevelThree));
                startMenuList.Add(new MenuItem("Exit", Color.White, MenuAppointment.Exit));
            }

            public void ChangeActiveMenu(List<MenuItem> newList)
            {
                isActiveNumber = 0;
                activeMenuList = newList;
                activeMenuList[isActiveNumber].Color = Color.Blue;
            }

            public void ChangeActiveNumber(bool isUp)
            {
                if (isUp)
                {
                    if (isActiveNumber > 0)
                    {
                        activeMenuList[isActiveNumber].Color = Color.White;
                        isActiveNumber--;
                        activeMenuList[isActiveNumber].Color = Color.Blue;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    if (isActiveNumber < activeMenuList.Count - 1)
                    {
                        activeMenuList[isActiveNumber].Color = Color.White;
                        isActiveNumber++;
                        activeMenuList[isActiveNumber].Color = Color.Blue;
                    }
                    else
                    {
                        return;
                    }
                }
            }

            public MenuAppointment GetActiveMenuAppointment()
            {
                return activeMenuList[isActiveNumber].Appointment;
            }
        }

        public enum MenuAppointment {LevelOne, LevelTwo, LevelThree, Exit}


        private SpriteFont font;
        

        public override void LoadContent()
        {
            base.LoadContent();

            font = content.Load<SpriteFont>("Menu/Menu");

            Rectangle rectCover = new Rectangle(540, 400, 720, 624);
            cover = new Image("Menu/cover", rectCover);
            cover.LoadContent();

        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            cover.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.Instance.KeyPressed(Keys.Up, Keys.W))
            {
                MenuManager.Instance.ChangeActiveNumber(true);
            }
            if (InputManager.Instance.KeyPressed(Keys.Down, Keys.S))
            {
                MenuManager.Instance.ChangeActiveNumber(false);
            }
            if (InputManager.Instance.KeyPressed(Keys.Enter))
            {
                switch (MenuManager.Instance.GetActiveMenuAppointment())
                {
                    case MenuAppointment.LevelOne:
                        ScreenManager.Instance.ChangeScreen(new PlayScreen(1));
                        break;
                    case MenuAppointment.LevelTwo:
                        ScreenManager.Instance.ChangeScreen(new PlayScreen(2));
                        break;
                    case MenuAppointment.LevelThree:
                        ScreenManager.Instance.ChangeScreen(new PlayScreen(3));
                        break;
                    case MenuAppointment.Exit:
                        break;
                }

            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            cover.Draw(spriteBatch);
            for(int i = 0; i < MenuManager.Instance.activeMenuList.Count; i++)
            {
                spriteBatch.DrawString(
                  font,
                  MenuManager.Instance.activeMenuList[i].Title,
                  new Vector2(100, 150 + (i * 50)),
                  MenuManager.Instance.activeMenuList[i].Color
                  );
            }
        }
    }
}
