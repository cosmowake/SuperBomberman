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
    abstract class Entity
    {
        public AnimationImage Image;
        public Vector2 Velocity;
        public float MoveSpeed {  get; protected set; }

        protected int tileSize;

        public Rectangle CollisionRectangle;
        public Vector2 Position { get { return new Vector2(CollisionRectangle.X, CollisionRectangle.Y); } }

        public Action Death;

        public bool CanMove = true;

        public virtual void LoadContent()
        {
            Image.LoadContent();
        }

        public virtual void UnloadContent()
        {
            Image.UnloadContent();
        }

        public virtual void Update(GameTime gameTime)
        {
            Image.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }

        public void ChangePosition(Point point)
        {
            Image.image.Position = new Vector2(point.X, point.Y - (tileSize / 2));
            CollisionRectangle.X = point.X;
            CollisionRectangle.Y = point.Y;
        }

        public void ChangePosition(Vector2 velocity)
        {
            Image.image.Position += velocity;
            CollisionRectangle.X = (int)Image.image.Position.X;
            CollisionRectangle.Y = (int)Image.image.Position.Y + (tileSize / 2);
        }
    }
}

//CollisionRectangle.X = ((int) Image.image.Position.X + Image.image.SourceRectangle.Width - CollisionRectangle.Width);
//CollisionRectangle.Y = ((int) Image.image.Position.Y + Image.image.SourceRectangle.Height - CollisionRectangle.Height);