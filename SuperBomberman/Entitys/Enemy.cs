using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBomberman
{
    abstract class Enemy : Entity
    {
        public int HitPoints;

        public DirectionState DirectionState = DirectionState.None;
        
        public void MoveUp() 
        {
            DirectionState = DirectionState.Up;
        }
        public void MoveDown()
        {
            DirectionState = DirectionState.Down;
        }
        public void MoveRight()
        {
            DirectionState = DirectionState.Right;
        }
        public void MoveLeft()
        {
            DirectionState = DirectionState.Left;
        }
        public void MoveStop() 
        {
            DirectionState = DirectionState.None;
        }

        public abstract void CollisionChangePosition();
        
        public override void LoadContent()
        {
            base.LoadContent();
            Image.ChangeAnimation(0, new List<int>(new int[] { 0, 1, 2 }));
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
                case DirectionState.Up : 
                    {
                        Velocity = new Vector2(0, -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        break;
                    }
                case DirectionState.Down:
                    {
                        Velocity = new Vector2(0, MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        break;
                    }
                case DirectionState.Right:
                    {
                        Velocity = new Vector2(MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
                        break;
                    }
                case DirectionState.Left:
                    {
                        Velocity = new Vector2(-MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
                        break;
                    }
                case DirectionState.None:
                    {
                        Velocity = Vector2.Zero;
                        break;
                    }
            }
            ChangePosition(Velocity);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        } 
    }
    enum DirectionState {Right,Left, Down,Up, None }
}
