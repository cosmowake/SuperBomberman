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
        public List<Bomb> bombList = new List<Bomb>();
        public int bombMaxCount = 1;
        public int power = 1;

        public delegate Vector2 BombStand(Vector2 position);
        public BombStand BombStandDel;

        public delegate void Explosion(Vector2 pos, int power);
        public Explosion explosion;

        public DelayedAction afterDeath;

        public Player(string spritePath, Point startPoint, int tileSize)
        {
            MoveSpeed = 150.0f;
            IsInvulnerable = false;
            this.tileSize = tileSize;

            CollisionRectangle = new Rectangle(startPoint.X, startPoint.Y, tileSize, tileSize);

            Image imageTemp = new Image(spritePath, new Rectangle(0, 0, tileSize, (int)(tileSize * 1.5)), new Vector2(startPoint.X, startPoint.Y - (int)(tileSize * 0.5)));
            Image = new AnimationImage(imageTemp, new List<int>(new int[4] { 0, 1, 0, 2 }));

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
                    if (!isDead)
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
                    if (!isDead)
                        Image.EndAnimation();
                }
            }


            ChangePosition(Velocity);


        }

        void BombUpdate(GameTime gameTime)
        {
            if (InputManager.Instance.KeyPressed(Keys.Space))
            {
                if (bombMaxCount > bombList.Count)
                {
                    Vector2 position = BombStandDel(new Vector2(CollisionRectangle.X, CollisionRectangle.Y));

                    Bomb bomb = new Bomb("Play/Bomb3x", position, 48);
                    bomb.ExplosionBomb += () =>
                    {
                        bombList.Remove(bomb);
                        explosion(bomb.Position, power);
                        bomb.UnloadContent();
                    };
                    bomb.LoadContent();

                    bombList.Add(bomb);
                }
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

            if (CanMove)
            {
                MoveUpdate(gameTime);
                BombUpdate(gameTime);
            }

            Player p = this;

            for (int i = 0; i < bombList.Count; i++)
            {
                bombList[i].Update(gameTime);
            }

            if (afterDeath != null)
                afterDeath.Update(gameTime.ElapsedGameTime.Milliseconds);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bomb bomb in bombList)
            {
                bomb.Draw(spriteBatch);
            }

            base.Draw(spriteBatch);
        }

    }
}
