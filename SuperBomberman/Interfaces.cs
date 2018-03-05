using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SuperBomberman
{
    interface IDraw
    {
        void Draw(SpriteBatch spriteBatch);
    }

    interface IUpdate
    {
        void Update(GameTime gameTime);
    }

    interface ILoadContent
    {
        void LoadContent();
    }

    interface IUnloadContent
    {
        void UnloadContent();
    }

    interface IAllIntrface : IDraw, IUpdate, ILoadContent, IUnloadContent
    {

    }

    interface ITile
    {
        Vector2 Position { get; set; }
        bool IsDestructible { get; }
        bool IsSolid { get; }
    }
}
