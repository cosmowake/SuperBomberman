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
    class Player : Entity
    {
        List<Bomb> bombList = new List<Bomb>();

        public delegate Vector2 BombStand(Vector2 position);
        public event BombStand BombStandEvent;

        public Player(string spritePath,Point startPoint, int tileSize)
        {
            MoveSpeed = 150.0f;

            this.tileSize = tileSize;

            CollisionRectangle = new Rectangle(startPoint.X, startPoint.Y, tileSize, tileSize);

            Image imageTemp = new Image(spritePath, new Rectangle(0, 0, tileSize, (int)(tileSize * 1.5)), new Vector2(startPoint.X, startPoint.Y - (int)(tileSize * 0.5)));
            Image = new AnimationImage(imageTemp, new List<int>(new int[4] { 1, 2, 1, 3 }));
            
        }

        void MoveUpdate(GameTime gameTime)
        {

            if (Velocity.X == 0)
            {
                if (InputManager.Instance.KeyDown(Keys.Down))
                {
                    Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Image.ChangeAnimation(0, new List<int>(new int[4] { 0, 1, 0, 2 }));
                    Image.StartAnimation();
                }
                else if (InputManager.Instance.KeyDown(Keys.Up))
                {
                    Velocity.Y = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Image.ChangeAnimation(3, new List<int>(new int[4] { 0, 1, 0, 2 }));
                    Image.StartAnimation();
                }
                else
                {
                    Velocity.Y = 0;
                    Image.EndAnimation();
                }
            }

            if (Velocity.Y == 0)
            {
                if (InputManager.Instance.KeyDown(Keys.Right))
                {
                    Velocity.X = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Image.ChangeAnimation(2, new List<int>(new int[4] { 0, 1, 0, 2 }));
                    Image.StartAnimation();
                }
                else if (InputManager.Instance.KeyDown(Keys.Left))
                {
                    Velocity.X = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Image.ChangeAnimation(1, new List<int>(new int[4] { 0, 1, 0, 2 }));
                    Image.StartAnimation();
                }
                else
                {
                    Velocity.X = 0;
                    Image.EndAnimation();
                }
            }


            ChangePosition(Velocity);


            // Console.WriteLine(Image.image.Position.ToString() + "|||||" + CollisionRectangle.ToString());

        }

        void BombUpdate(GameTime gameTime)
        {
            if (InputManager.Instance.KeyPressed(Keys.Space))
            {
                Vector2 position = BombStandEvent(new Vector2(CollisionRectangle.X,CollisionRectangle.Y));

                Bomb bomb = new Bomb("Play/Bomb3x", position, 48);
                bomb.DestroyBomb += () =>
                {
                    bombList.Remove(bomb);
                    bomb.UnloadContent();
                };
                bomb.LoadContent();

                bombList.Add(bomb);
            }
        }



        public override void LoadContent()
        {
            base.LoadContent();

            foreach (Bomb bomb in bombList)
            {
                bomb.LoadContent();
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            foreach (Bomb bomb in bombList)
            {
                bomb.UnloadContent();
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            MoveUpdate(gameTime);
            BombUpdate(gameTime);
            Console.WriteLine(CollisionRectangle.ToString());

            for (int i = 0;i < bombList.Count ;i++)
            {
                bombList[i].Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (Bomb bomb in bombList)
            {
                bomb.Draw(spriteBatch);
            }
        }
        
    }
}
