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
    class Map
    {
        public Image Sprite;
        public MapTile[,] mapTiles;
        public Vector2 Position;
        int tileSize;
        public MapTilesAnimation explosionList;

        public Map(string spritePathMap, string spritePathExplosion)
        {
            Sprite = new Image(spritePathMap);
            explosionList = new MapTilesAnimation(spritePathExplosion);
            Position = Vector2.Zero;
            MapGenerator(new Vector2(128, 128), 13, 15, 48);
        }

        void Explosion(Vector2 position, int power)
        {
            MapTilesAnimationList animationListTemp = new MapTilesAnimationList();
            animationListTemp.SequenceFrame = new List<int>(new int[5] { 0, 1, 2, 3, 4 });
            List<MapTileAnimationElement> tempElementList = new List<MapTileAnimationElement>();
            Point startExplosionPosition = GetXAndYByVector(position);

            MapTileAnimationElement mapTileAnimationElement = new MapTileAnimationElement(
                new Rectangle(0, 1 * tileSize, tileSize, tileSize),
                GetVectorByXAndY(startExplosionPosition.X, startExplosionPosition.Y));

            tempElementList.Add(mapTileAnimationElement);
            

            for (int i = 0; i < power; i++)
            {
                if (i == power - 1)
                {
                    if (startExplosionPosition.X - i >= 0 && !mapTiles[startExplosionPosition.X - i - 1, startExplosionPosition.Y].IsSolid)
                    {
                        mapTileAnimationElement = new MapTileAnimationElement(
                        new Rectangle(0, 7 * tileSize, tileSize, tileSize),
                        GetVectorByXAndY(startExplosionPosition.X - i - 1, startExplosionPosition.Y));

                        tempElementList.Add(mapTileAnimationElement);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    if (startExplosionPosition.X - i >= 0 && !mapTiles[startExplosionPosition.X - i - 1, startExplosionPosition.Y].IsSolid)
                    {
                        mapTileAnimationElement = new MapTileAnimationElement(
                        new Rectangle(0, 3 * tileSize, tileSize, tileSize),
                        GetVectorByXAndY(startExplosionPosition.X - i - 1, startExplosionPosition.Y));

                        tempElementList.Add(mapTileAnimationElement);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            for (int i = 0; i < power; i++)
            {
                if (i == power - 1)
                {
                    if (startExplosionPosition.X + i < mapTiles.GetLength(0) && !mapTiles[startExplosionPosition.X + i + 1, startExplosionPosition.Y].IsSolid)
                    {
                        mapTileAnimationElement = new MapTileAnimationElement(
                        new Rectangle(0, 6 * tileSize, tileSize, tileSize),
                        GetVectorByXAndY(startExplosionPosition.X + i + 1, startExplosionPosition.Y));

                        tempElementList.Add(mapTileAnimationElement);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    if (startExplosionPosition.X + i < mapTiles.GetLength(0) && !mapTiles[startExplosionPosition.X + i + 1, startExplosionPosition.Y].IsSolid)
                    {
                        mapTileAnimationElement = new MapTileAnimationElement(
                        new Rectangle(0, 3 * tileSize, tileSize, tileSize),
                        GetVectorByXAndY(startExplosionPosition.X + i + 1, startExplosionPosition.Y));

                        tempElementList.Add(mapTileAnimationElement);
                    }
                    else
                    {
                        break;
                    }
                }
            }


            for (int i = 0; i < power; i++)
            {
                if (i == power - 1)
                {
                    if (startExplosionPosition.Y - i >= 0 && !mapTiles[startExplosionPosition.X, startExplosionPosition.Y - i - 1].IsSolid)
                    {
                        mapTileAnimationElement = new MapTileAnimationElement(
                        new Rectangle(0, 4 * tileSize, tileSize, tileSize),
                        GetVectorByXAndY(startExplosionPosition.X, startExplosionPosition.Y - i - 1));

                        tempElementList.Add(mapTileAnimationElement);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    if (startExplosionPosition.Y - i >= 0 && !mapTiles[startExplosionPosition.X, startExplosionPosition.Y - i - 1].IsSolid)
                    {
                        mapTileAnimationElement = new MapTileAnimationElement(
                        new Rectangle(0, 2 * tileSize, tileSize, tileSize),
                        GetVectorByXAndY(startExplosionPosition.X, startExplosionPosition.Y - i - 1));

                        tempElementList.Add(mapTileAnimationElement);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            for (int i = 0; i < power; i++)
            {
                if (i == power - 1)
                {
                    if (startExplosionPosition.Y + i < mapTiles.GetLength(1) && !mapTiles[startExplosionPosition.X, startExplosionPosition.Y + i + 1].IsSolid)
                    {
                        mapTileAnimationElement = new MapTileAnimationElement(
                        new Rectangle(0, 5 * tileSize, tileSize, tileSize),
                        GetVectorByXAndY(startExplosionPosition.X, startExplosionPosition.Y + i + 1));

                        tempElementList.Add(mapTileAnimationElement);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    if (startExplosionPosition.Y + i < mapTiles.GetLength(1) && !mapTiles[startExplosionPosition.X, startExplosionPosition.Y + i + 1].IsSolid)
                    {
                        mapTileAnimationElement = new MapTileAnimationElement(
                        new Rectangle(0, 2 * tileSize, tileSize, tileSize),
                        GetVectorByXAndY(startExplosionPosition.X, startExplosionPosition.Y + i + 1));

                        tempElementList.Add(mapTileAnimationElement);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            animationListTemp.MapTileAnimationElementList = tempElementList;
            animationListTemp.EndAnimate += () => { explosionList.mapTilesAnimationList.Remove(animationListTemp); };

            explosionList.mapTilesAnimationList.Add(animationListTemp);
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
            if (InputManager.Instance.KeyPressed(Keys.Q))
            {
                Explosion(GetVectorByXAndY(3,3), 2);
            }

            if (InputManager.Instance.KeyPressed(Keys.W))
            {
                Explosion(GetVectorByXAndY(1, 1), 2);
            }
            if (InputManager.Instance.KeyPressed(Keys.E))
            {
                Explosion(GetVectorByXAndY(3, 2), 2);
            }

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
                            }
                        }
                        entity.ChangePosition(correctVector);
                    }
                }
            }
            explosionList.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (MapTile mapTile in mapTiles)
            {
                spriteBatch.Draw(Sprite.Texture, mapTile.Position, mapTile.SourceRectangle, Color.White);
            }
            explosionList.Draw(spriteBatch);

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

    public class MapTilesAnimation
    {
        Texture2D texture;
        public List<MapTilesAnimationList> mapTilesAnimationList;

        public MapTilesAnimation(string texturePath)
        {
            ContentManager content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content"); ;
            texture = content.Load<Texture2D>(texturePath);
            mapTilesAnimationList = new List<MapTilesAnimationList>();
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < mapTilesAnimationList.Count; i++)
            {
                mapTilesAnimationList[i].Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < mapTilesAnimationList.Count; i++)
            {
                mapTilesAnimationList[i].Draw(spriteBatch, ref texture);
            }
        }
    }

    public class MapTilesAnimationList
    {
        public List<MapTileAnimationElement> MapTileAnimationElementList;
        bool IsAnimate = true;

        bool loop = false;

        public int CurrentFrame = 0;
        public int SwitchTime = 300;
        int switchCounter = 0;
        public List<int> SequenceFrame = new List<int>();

        public event Action EndAnimate;

        public MapTilesAnimationList()
        {
            MapTileAnimationElementList = new List<MapTileAnimationElement>();
            this.loop = loop;
        }

        public void StartAnimation()
        {
            IsAnimate = true;
        }

        public void EndAnimation()
        {
            IsAnimate = false;
        }

        public void LoadContent()
        {

        }

        public void Update(GameTime gameTime)
        {
            if (IsAnimate)
            {
                switchCounter += (int)gameTime.ElapsedGameTime.Milliseconds;
                if (switchCounter >= SwitchTime)
                {
                    switchCounter = 0;
                    SwitchTime = 100;


                    CurrentFrame++;

                    if (CurrentFrame >= SequenceFrame.Count)
                    {
                        if (loop)
                        {
                            CurrentFrame = 0;
                        }
                        else
                        {
                            EndAnimate();
                            CurrentFrame = 0;
                        }
                    }

                    for (int i = 0; i < MapTileAnimationElementList.Count; i++)
                    {
                        MapTileAnimationElementList[i].SourceRectangle.X = SequenceFrame[CurrentFrame] * MapTileAnimationElementList[i].SourceRectangle.Width;
                    }

                }

            }
        }

        public void Draw(SpriteBatch spriteBatch, ref Texture2D texture)
        {
            for (int i = 0; i < MapTileAnimationElementList.Count; i++)
            {
                spriteBatch.Draw(texture, MapTileAnimationElementList[i].Position, MapTileAnimationElementList[i].SourceRectangle, Color.White);

            }
        }


    }

    public class MapTileAnimationElement
    {

        public Rectangle SourceRectangle;
        public Vector2 Position { get; set; }


        public MapTileAnimationElement(Rectangle sourceRectangles, Vector2 position)
        {
            SourceRectangle = sourceRectangles;
            Position = position;
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}

