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
    class PlayScreen : GameScreen
    {
        List<Entity> entityList = new List<Entity>();
        Map map;


        public override void LoadContent()
        {
            base.LoadContent();

            map = new Map("Play/MapTile1_3x","Play/Bomb3x");
            map.LoadContent();

            Vector2 playerVector = map.GetVectorByXAndY(1, 1);
            Point playerPoint = new Point((int)playerVector.X, (int)playerVector.Y);
            Player player = new Player("Play/BombermanWhite3x", playerPoint, 48);
            player.BombStandEvent += ((Vector2 position) => 
                {
                    Point p = map.GetXAndYByVector(position);
                    
                    return map.GetVectorByXAndY(p.X, p.Y);
                });

            entityList.Add(player);

            foreach (Entity entity in entityList)
            {
                entity.LoadContent();
            }
            
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            foreach (Entity entity in entityList)
            {
                entity.UnloadContent();
            }
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (Entity entity in entityList)
            {
                entity.Update(gameTime);

                Entity tempEntity = entity;
                map.Update(gameTime,ref tempEntity);
            }
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            map.Draw(spriteBatch);

            foreach (Entity entity in entityList)
            {
                entity.Draw(spriteBatch);
            }
            
        }
    }
}
