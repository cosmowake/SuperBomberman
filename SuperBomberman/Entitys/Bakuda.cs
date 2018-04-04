using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBomberman.Entitys
{
    class Bakuda : Enemy
    {
        Random random = new Random();

        public Bakuda(Point startPoint, int tileSize)
        {
            MoveSpeed = 75.0f;
            HitPoints = 0;
            DirectionState = DirectionState.Left;
            HitPoints = 1;

            this.tileSize = tileSize;

            CollisionRectangle = new Rectangle(startPoint.X, startPoint.Y, tileSize, tileSize);

            Image imageTemp = new Image("Play/Bakuda3x", new Rectangle(0, 0, tileSize, tileSize), new Vector2(startPoint.X, startPoint.Y));
            Image = new AnimationImage(imageTemp, new List<int>(new int[] { 0, 1}));
            Image.SwitchTime = 100;
        }

        public override void CollisionChangePosition()
        {
            if (DirectionState == DirectionState.Left)
            {
                if (random.Next(100) >= 50)
                {
                    DirectionState = DirectionState.Right;
                }
                else
                {
                    if (random.Next(100) >= 50)
                    {
                        DirectionState = DirectionState.Up;
                    }
                    else
                    {
                        DirectionState = DirectionState.Down;
                    }
                }
            }
            else if (DirectionState == DirectionState.Right)
            {
                if (random.Next(100) >= 20)
                {
                    DirectionState = DirectionState.Left;
                }
                else
                {
                    if (random.Next(100) >= 50)
                    {
                        DirectionState = DirectionState.Up;
                    }
                    else
                    {
                        DirectionState = DirectionState.Down;
                    }
                }
            }
            else if (DirectionState == DirectionState.Down)
            {
                if (random.Next(100) >= 20)
                {
                    DirectionState = DirectionState.Up;
                }
                else
                {
                    if (random.Next(100) >= 50)
                    {
                        DirectionState = DirectionState.Left;
                    }
                    else
                    {
                        DirectionState = DirectionState.Right;
                    }
                }
            }
            else if (DirectionState == DirectionState.Up)
            {
                if (random.Next(100) >= 20)
                {
                    DirectionState = DirectionState.Down;
                }
                else
                {
                    if (random.Next(100) >= 50)
                    {
                        DirectionState = DirectionState.Left;
                    }
                    else
                    {
                        DirectionState = DirectionState.Right;
                    }
                }
            }
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            switch (DirectionState)
            {
                case DirectionState.Up:
                    {
                        Image.ChangeAnimation(1, new List<int>(new int[] { 0, 1 }));
                        Image.StartAnimation();
                        break;
                    }
                case DirectionState.Down:
                    {
                        Image.ChangeAnimation(1, new List<int>(new int[] { 0, 1 }));
                        Image.StartAnimation();
                        break;
                    }
                case DirectionState.Right:
                    {
                        Image.ChangeAnimation(1, new List<int>(new int[] { 0, 1 }));
                        Image.StartAnimation();
                        break;
                    }
                case DirectionState.Left:
                    {
                        Image.ChangeAnimation(1, new List<int>(new int[] { 0, 1 }));
                        Image.StartAnimation();
                        break;
                    }
                case DirectionState.None:
                    {
                        Image.StartAnimation();
                        break;
                    }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
