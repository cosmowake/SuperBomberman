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
    class AnimationImage
    {
        public Image image;

        public int CurrentFrame = 0;
        public int SwitchTime = 200;
        int switchCounter = 0;
        public List<int> SequenceFrame = new List<int>();
        bool IsAnimate = false;

        public void StartAnimation()
        {
            IsAnimate = true;
        }

        public void EndAnimation()
        {
            IsAnimate = false;
        }

        public void ChangeAnimation(int NumberHeightFrame, List<int> sequenceFrame)
        {
            if (image.SourceRectangle.Width * NumberHeightFrame < image.Texture.Width)
            {
                image.SourceRectangle.Y = image.SourceRectangle.Height * NumberHeightFrame;
                SequenceFrame = sequenceFrame;
            }
        }

        public AnimationImage(Image image, List<int> sequenceFrame)
        {
            this.image = image;
            SequenceFrame = sequenceFrame;
        }

        public void LoadContent()
        {
            image.LoadContent();
        }

        public void UnloadContent()
        {
            image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            image.Update(gameTime);

            if (IsAnimate)
            {
                switchCounter += (int)gameTime.ElapsedGameTime.Milliseconds;
                if (switchCounter >= SwitchTime)
                {
                    switchCounter = 0;
                    CurrentFrame++;

                    if (CurrentFrame >= SequenceFrame.Count)
                    {
                        CurrentFrame = 0;
                    }
                }
            }
            else
            {
                CurrentFrame = 0;
            }

            image.SourceRectangle.X = (SequenceFrame[CurrentFrame] * image.SourceRectangle.Width);
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            image.Draw(spriteBatch);
        }
    }
}
