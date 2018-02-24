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
    class Image
    {
        string texturePath;
        public float Alpha;
        public Vector2 Position;
        public Vector2 Scale;
        public Rectangle SourceRectangle;
        public Color Color;
        public Texture2D Texture;

        Vector2 origin;

        ContentManager content;
        RenderTarget2D renderTarget;

        public Image(string texturePath)
        {
            this.texturePath = texturePath;
            Alpha = 1.0f;
            Position = Vector2.Zero;
            Scale = Vector2.One;
            SourceRectangle = Rectangle.Empty;
            Color = Color.White;
        }

        public Image(string texturePath, Rectangle sourceRectangle)
        {
            this.texturePath = texturePath;
            Alpha = 1.0f;
            Position = Vector2.Zero;
            Scale = Vector2.One;
            SourceRectangle = sourceRectangle;
            Color = Color.White;
        }

        public Image(string texturePath, Rectangle sourceRectangle, Vector2 position)
        {
            this.texturePath = texturePath;
            Alpha = 1.0f;
            Position = position;
            Scale = Vector2.One;
            SourceRectangle = sourceRectangle;
            Color = Color.White;
        }

        public virtual void LoadContent()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            
            Texture = content.Load<Texture2D>(texturePath);

            if (SourceRectangle == Rectangle.Empty && Texture != null)
            {
                SourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            }
        }

        public virtual void UnloadContent()
        {
            content.Unload();
            Texture.Dispose();
        }


        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            origin = new Vector2(SourceRectangle.Width / 2, SourceRectangle.Height / 2);
            
            spriteBatch.Draw(Texture,Position, SourceRectangle, Color * Alpha, 0.0f, Vector2.Zero, Scale, SpriteEffects.None,0.0f);
        }
    }
}
