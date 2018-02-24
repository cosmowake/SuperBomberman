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
    class Map
    {
        public Image Sprite;

        public MapTile[,] mapTiles;

        public Vector2 Position;

        int tileSize;

        public Map(string spritePath)
        {
            Sprite = new Image(spritePath);
            Position = Vector2.Zero;
            MapGenerator(new Vector2(128,128), 13,15,48);
        }

        void MapGenerator(Vector2 position, int heightMap, int widthMap, int tileSize)
        {
            Position = position;
            this.tileSize = tileSize;

            Vector2 positionTemp = Vector2.Zero;

            mapTiles = new MapTile[heightMap, widthMap];
            for (int i = 0; i < heightMap; i++)
            {
                for (int j = 0; j < widthMap; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        mapTiles[i, j] = new MapTile(new Rectangle(tileSize * 0, tileSize * 2, tileSize, tileSize), position + positionTemp, true, true);
                    }
                    else if (i == 0 && j == widthMap - 1)
                    {
                        mapTiles[i, j] = new MapTile(new Rectangle(tileSize * 2, tileSize * 2, tileSize, tileSize), position + positionTemp, true, true);
                    }
                    else if (i == heightMap - 1 && j == 0)
                    {
                        mapTiles[i, j] = new MapTile(new Rectangle(tileSize * 0, tileSize * 4, tileSize, tileSize), position + positionTemp, true, true);
                    }
                    else if (i == heightMap - 1 && j == widthMap - 1)
                    {
                        mapTiles[i, j] = new MapTile(new Rectangle(tileSize * 2, tileSize * 4, tileSize, tileSize), position + positionTemp, true, true);
                    }
                    else if (i == 0)
                    {
                        mapTiles[i, j] = new MapTile(new Rectangle(tileSize * 1, tileSize * 2, tileSize, tileSize), position + positionTemp, true, true);
                    }
                    else if (i == heightMap - 1)
                    {
                        mapTiles[i, j] = new MapTile(new Rectangle(tileSize * 1, tileSize * 4, tileSize, tileSize), position + positionTemp, true, true);
                    }
                    else if (j == 0)
                    {
                        mapTiles[i, j] = new MapTile(new Rectangle(tileSize * 0, tileSize * 3, tileSize, tileSize), position + positionTemp, true, true);
                    }
                    else if (j == widthMap - 1)
                    {
                        mapTiles[i, j] = new MapTile(new Rectangle(tileSize * 2, tileSize * 3, tileSize, tileSize), position + positionTemp, true, true);
                    }
                    else if ((i > 1 && i < heightMap - 2 && i % 2 == 0) && (j > 1 && j < widthMap - 2 && j % 2 == 0))
                    {
                        mapTiles[i, j] = new MapTile(new Rectangle(tileSize * 1, tileSize * 3, tileSize, tileSize), position + positionTemp, true, true);
                    }
                    else
                    {
                        mapTiles[i, j] = new MapTile(new Rectangle(tileSize * 0, tileSize * 0, tileSize, tileSize), position + positionTemp, false, false);
                    }

                    positionTemp.X += tileSize;
                }

                positionTemp.X = 0;
                positionTemp.Y += tileSize;
            }

            positionTemp.X = 0;
            positionTemp.Y = 0;
        }

        public Vector2 GetVectorByXAndY(int x, int y)
        {
            Vector2 vector = Position;
            vector.X += x * tileSize;
            vector.Y += y * tileSize;
            return vector;
        }

        public Point GetXAndYByVector(Vector2 position)
        {
            position -= Position;
            Point positionOnMap = new Point(((int)(position.X + (tileSize / 2)) / tileSize), ((int)(position.Y + (tileSize / 2)) / tileSize));
            
            return positionOnMap;
        }

        public void LoadContent()
        {
            Sprite.LoadContent();
        }

        public void UnloadContent()
        {
            Sprite.UnloadContent();
        }

        public void Update(GameTime gameTime, ref Entity entity)
        {
            //Console.WriteLine(mapTiles.GetLength(0).ToString() + " " + mapTiles.GetLength(1).ToString());

            foreach (MapTile mapTile in mapTiles)
            {
                mapTile.Update(gameTime, ref entity);

                if (mapTile.IsSolid)
                {
                    Rectangle tileRect = new Rectangle((int)mapTile.Position.X, (int)mapTile.Position.Y, mapTile.SourceRectangle.Width, mapTile.SourceRectangle.Height);

                    if (entity.CollisionRectangle.Intersects(tileRect))
                    {
                        Point tilePoint = GetXAndYByVector(mapTile.Position);
                        Point entityPoint = GetXAndYByVector(new Vector2(entity.CollisionRectangle.X, entity.CollisionRectangle.Y));
                        Vector2 correctVector = Vector2.Zero;

                        if (entity.Velocity.X < 0)
                        {
                            entity.ChangePosition(new Point(tileRect.Right, entity.CollisionRectangle.Y));

                            if (tilePoint.Y != entityPoint.Y && entityPoint.X - 1 >= 0 && !mapTiles[entityPoint.Y, entityPoint.X - 1].IsSolid)
                            {
                                entity.ChangePosition(GetVectorByXAndY(entityPoint.X, entityPoint.Y).ToPoint());
                            }
                        }
                        else if (entity.Velocity.X > 0)
                        {
                            entity.ChangePosition(new Point(tileRect.Left - entity.CollisionRectangle.Width, entity.CollisionRectangle.Y));

                            if (tilePoint.Y != entityPoint.Y && entityPoint.X + 1 < mapTiles.GetLength(1) && !mapTiles[entityPoint.Y, entityPoint.X + 1].IsSolid)
                            {
                                entity.ChangePosition(GetVectorByXAndY(entityPoint.X, entityPoint.Y).ToPoint());
                            }
                        }
                        else if (entity.Velocity.Y < 0)
                        {
                            entity.ChangePosition(new Point(entity.CollisionRectangle.X, tileRect.Bottom));

                            if (tilePoint.X != entityPoint.X && entityPoint.Y - 1 >= 0 && !mapTiles[entityPoint.Y - 1, entityPoint.X].IsSolid)
                            {
                                entity.ChangePosition(GetVectorByXAndY(entityPoint.X, entityPoint.Y).ToPoint());
                            }
                        }
                        else if (entity.Velocity.Y > 0)
                        {
                            entity.ChangePosition(new Point(entity.CollisionRectangle.X, tileRect.Top - entity.CollisionRectangle.Height));

                            if (tilePoint.X != entityPoint.X && entityPoint.Y + 1 < mapTiles.GetLength(0) && !mapTiles[entityPoint.Y + 1, entityPoint.X].IsSolid)
                            {
                                entity.ChangePosition(GetVectorByXAndY(entityPoint.X, entityPoint.Y).ToPoint());
                                //if (entity.CollisionRectangle.X > GetVectorByXAndY(entityPoint.X, entityPoint.Y).ToPoint().X)
                                //{
                                //    correctVector.X = -entity.MoveSpeed / 2.5f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                                //}
                                //else if (entity.CollisionRectangle.X < GetVectorByXAndY(entityPoint.X, entityPoint.Y).ToPoint().X)
                                //{
                                //    correctVector.X = entity.MoveSpeed / 2.5f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                                //}
                            }
                        }
                        entity.ChangePosition(correctVector);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (MapTile mapTile in mapTiles)
            {
                spriteBatch.Draw(Sprite.Texture, mapTile.Position, mapTile.SourceRectangle, Color.White);
            }
        }
    }

    class MapTile
    {
        public Rectangle SourceRectangle { get; set; }
        public Vector2 Position { get; set; }
        public bool IsDestructible { get; set; }
        public bool IsSolid { get; set; }



        public MapTile(Rectangle sourceRectangle, Vector2 position, bool isDestructible, bool isSolid)
        {
            SourceRectangle = sourceRectangle;
            Position = position;
            IsDestructible = isDestructible;
            IsSolid = isSolid;
        }

        public Point PointPositionOnMap(Vector2 position)
        {

            Vector2 positionVector = Position - position;
            Point positionOnMap = new Point((int)(positionVector.X / SourceRectangle.Width), (int)(positionVector.Y / SourceRectangle.Height));
            
            return positionOnMap;
        }

        public void Update(GameTime gameTime, ref Entity entity)
        {

        }
    }
}
