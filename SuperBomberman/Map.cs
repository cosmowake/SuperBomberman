﻿using System;
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
        Texture2D sprite;
        MapTile[,] mapTiles;
        public Vector2 Position;
        int tileSize;
        public ExplosionTilesManager explosionList;

        List<Entity> entityList = new List<Entity>();

        public AnimatedMapTiles animatedWalls;

        public Map(string spritePathMap, string spritePathExplosion)
        {
            ContentManager content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content"); ;
            sprite = content.Load<Texture2D>(spritePathMap);

            explosionList = new ExplosionTilesManager(spritePathExplosion);
            Position = Vector2.Zero;
            MapGenerator(new Vector2(0, 0), 15, 21, 48, spritePathMap);
        }

        void Explosion(Vector2 position, int power)
        {
            Point startExplosionPosition = GetXAndYByVector(position);

            if (mapTiles[startExplosionPosition.Y, startExplosionPosition.X].IsSolid)
            {
                return;
            }

            ExplosionTilesList explosionTilesList = new ExplosionTilesList();
            explosionTilesList.SequenceFrame = new List<int>(new int[12] { 4, 3, 2, 1, 0, 0, 0, 0, 1, 2, 3, 4 });
            List<ExplosionTile> tempExplosionList = new List<ExplosionTile>();

            ExplosionTile ExplosionTile = new ExplosionTile(
                new Rectangle(0, 1 * tileSize, tileSize, tileSize),
                GetVectorByXAndY(startExplosionPosition.X, startExplosionPosition.Y));

            tempExplosionList.Add(ExplosionTile);


            for (int i = 0; i < power; i++)
            {
                if (i == power - 1)
                {
                    if (startExplosionPosition.X - i - 1 >= 0 && !mapTiles[startExplosionPosition.Y, startExplosionPosition.X - i - 1].IsSolid)
                    {
                        ExplosionTile = new ExplosionTile(
                        new Rectangle(4 * tileSize, 7 * tileSize, tileSize, tileSize),
                        GetVectorByXAndY(startExplosionPosition.X - i - 1, startExplosionPosition.Y));

                        tempExplosionList.Add(ExplosionTile);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    if (startExplosionPosition.X - i - 1 >= 0 && !mapTiles[startExplosionPosition.Y, startExplosionPosition.X - i - 1].IsSolid)
                    {
                        ExplosionTile = new ExplosionTile(
                        new Rectangle(4 * tileSize, 3 * tileSize, tileSize, tileSize),
                        GetVectorByXAndY(startExplosionPosition.X - i - 1, startExplosionPosition.Y));

                        tempExplosionList.Add(ExplosionTile);
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
                    if (startExplosionPosition.X + i + 1 < mapTiles.GetLength(0) && !mapTiles[startExplosionPosition.Y, startExplosionPosition.X + i + 1].IsSolid)
                    {
                        ExplosionTile = new ExplosionTile(
                        new Rectangle(4 * tileSize, 6 * tileSize, tileSize, tileSize),
                        GetVectorByXAndY(startExplosionPosition.X + i + 1, startExplosionPosition.Y));

                        tempExplosionList.Add(ExplosionTile);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    if (startExplosionPosition.X + i + 1 < mapTiles.GetLength(0) && !mapTiles[startExplosionPosition.Y, startExplosionPosition.X + i + 1].IsSolid)
                    {
                        ExplosionTile = new ExplosionTile(
                        new Rectangle(4 * tileSize, 3 * tileSize, tileSize, tileSize),
                        GetVectorByXAndY(startExplosionPosition.X + i + 1, startExplosionPosition.Y));

                        tempExplosionList.Add(ExplosionTile);
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
                    if (startExplosionPosition.Y - i - 1 >= 0 && !mapTiles[startExplosionPosition.Y - i - 1, startExplosionPosition.X].IsSolid)
                    {
                        ExplosionTile = new ExplosionTile(
                        new Rectangle(4 * tileSize, 4 * tileSize, tileSize, tileSize),
                        GetVectorByXAndY(startExplosionPosition.X, startExplosionPosition.Y - i - 1));

                        tempExplosionList.Add(ExplosionTile);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    if (startExplosionPosition.Y - i - 1 >= 0 && !mapTiles[startExplosionPosition.Y - i - 1, startExplosionPosition.X].IsSolid)
                    {
                        ExplosionTile = new ExplosionTile(
                        new Rectangle(4 * tileSize, 2 * tileSize, tileSize, tileSize),
                        GetVectorByXAndY(startExplosionPosition.X, startExplosionPosition.Y - i - 1));

                        tempExplosionList.Add(ExplosionTile);
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
                    if (startExplosionPosition.Y + i + 1 < mapTiles.GetLength(0) && !mapTiles[startExplosionPosition.Y + i + 1, startExplosionPosition.X].IsSolid)
                    {
                        ExplosionTile = new ExplosionTile(
                        new Rectangle(4 * tileSize, 5 * tileSize, tileSize, tileSize),
                        GetVectorByXAndY(startExplosionPosition.X, startExplosionPosition.Y + i + 1));

                        tempExplosionList.Add(ExplosionTile);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    if (startExplosionPosition.Y + i + 1 < mapTiles.GetLength(0) && !mapTiles[startExplosionPosition.Y + i + 1, startExplosionPosition.X].IsSolid)
                    {
                        ExplosionTile = new ExplosionTile(
                        new Rectangle(4 * tileSize, 2 * tileSize, tileSize, tileSize),
                        GetVectorByXAndY(startExplosionPosition.X, startExplosionPosition.Y + i + 1));

                        tempExplosionList.Add(ExplosionTile);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            explosionTilesList.ExplosionTileList = tempExplosionList;
            explosionTilesList.EndAnimate += () => { explosionList.ExplosionTilesList.Remove(explosionTilesList); };

            explosionList.ExplosionTilesList.Add(explosionTilesList);
        }

        void MapGenerator(Vector2 position, int heightMap, int widthMap, int tileSize, string spritePathMap)
        {
            Position = position;
            this.tileSize = tileSize;

            Vector2 positionTemp = Vector2.Zero;

            animatedWalls = new AnimatedMapTiles(spritePathMap, widthMap, heightMap, new List<int>(new int[] { 0, 1, 2, 3 }));
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

            animatedWalls.Add(1,3,tileSize,GetVectorByXAndY(1,3));
            animatedWalls.Add(3, 3, tileSize, GetVectorByXAndY(3, 3));
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
            Vector2 playerVector = GetVectorByXAndY(1, 1);
            Point playerPoint = new Point((int)playerVector.X, (int)playerVector.Y);
            Player player = new Player("Play/BombermanWhite3x", playerPoint, 48);
            player.explosion = Explosion;
            player.BombStandEvent += ((Vector2 position) =>
            {
                Point p = GetXAndYByVector(position);
                return GetVectorByXAndY(p.X, p.Y);
            });

            entityList.Add(player);

            foreach (Entity entity in entityList)
            {
                entity.LoadContent();
            }
        }

        public void UnloadContent()
        {

            foreach (Entity entity in entityList)
            {
                entity.UnloadContent();
            }
        }

        void Collision(ref Entity entity, ref MapTile tile)
        {
            if (tile.IsSolid)
            {
                Rectangle tileRect = new Rectangle((int)tile.Position.X, (int)tile.Position.Y, tile.SourceRectangle.Width, tile.SourceRectangle.Height);

                if (entity.CollisionRectangle.Intersects(tileRect))
                {
                    Point tilePoint = GetXAndYByVector(tile.Position);
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


        public void Update(GameTime gameTime)
        {
            if (InputManager.Instance.KeyPressed(Keys.Q))
            {
                Explosion(GetVectorByXAndY(0, 0), 2);
            }

            if (InputManager.Instance.KeyPressed(Keys.W))
            {
                Explosion(GetVectorByXAndY(14, 12), 2);
            }
            if (InputManager.Instance.KeyPressed(Keys.E))
            {
                Explosion(GetVectorByXAndY(3, 5), 8);
            }


            foreach (Entity e in entityList)
            {
                e.Update(gameTime);
                Entity entity = e;
                
                foreach (MapTile mapTile in mapTiles)
                {
                    mapTile.Update(gameTime);
                    MapTile m = mapTile;
                    Collision(ref entity, ref m);
                }

                foreach (MapTile mapTile in animatedWalls.mapTilesList)
                {
                    if (mapTile != null)
                    {
                        mapTile.Update(gameTime);
                        MapTile m = mapTile;
                        Collision(ref entity, ref m);
                    }
                }
            }
            explosionList.Update(gameTime);
            animatedWalls.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (MapTile mapTile in mapTiles)
            {
                spriteBatch.Draw(sprite, mapTile.Position, mapTile.SourceRectangle, Color.White);
            }

            explosionList.Draw(spriteBatch);

            foreach (Entity entity in entityList)
            {
                entity.Draw(spriteBatch);
            }

            animatedWalls.Draw(spriteBatch);
        }
    }

    public class MapTile
    {
        public Rectangle SourceRectangle;
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

        public void Update(GameTime gameTime)
        {

        }
    }


    public class AnimatedMapTiles
    {
        Texture2D Texture;
        public MapTile[,] mapTilesList;

        public int CurrentFrame = 0;
        int SwitchTime = 200;
        int switchCounter = 0;
        List<int> SequenceFrame = new List<int>();
        bool IsAnimate = true;

        public AnimatedMapTiles(string texturePath, int weight, int height, List<int> sequenceFrame)
        {
            ContentManager content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content"); ;
            Texture = content.Load<Texture2D>(texturePath);
            SequenceFrame = sequenceFrame;
            mapTilesList = new MapTile[height, weight];
        }

        public void Add(int x, int y, int tileSize, Vector2 position)
        {
            MapTile mapTileTemp = new MapTile(new Rectangle(0, 1 * tileSize, tileSize, tileSize), position, true, true);
            mapTilesList[y, x] = mapTileTemp;
        }

        public void StartAnimation()
        {
            IsAnimate = true;
        }

        public void EndAnimation()
        {
            IsAnimate = false;
        }

        public void Update(GameTime gameTime)
        {
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

            for (int i = 0; i < mapTilesList.GetLength(0); i++)
            {
                for (int j = 0; j < mapTilesList.GetLength(1); j++)
                {
                    if (mapTilesList[i, j] != null)
                        mapTilesList[i, j].SourceRectangle.X = SequenceFrame[CurrentFrame] * mapTilesList[i, j].SourceRectangle.Width;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < mapTilesList.GetLength(0); i++)
            {
                for (int j = 0; j < mapTilesList.GetLength(1); j++)
                {
                    if (mapTilesList[i, j] != null)
                        spriteBatch.Draw(Texture, mapTilesList[i, j].Position, mapTilesList[i, j].SourceRectangle, Color.White);
                }
            }
        }
    }

    public class ExplosionTilesManager
    {
        Texture2D texture;
        public List<ExplosionTilesList> ExplosionTilesList;

        public ExplosionTilesManager(string texturePath)
        {
            ContentManager content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content"); ;
            texture = content.Load<Texture2D>(texturePath);
            ExplosionTilesList = new List<ExplosionTilesList>();
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < ExplosionTilesList.Count; i++)
            {
                ExplosionTilesList[i].Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < ExplosionTilesList.Count; i++)
            {
                ExplosionTilesList[i].Draw(spriteBatch, ref texture);
            }
        }
    }

    public class ExplosionTilesList
    {
        public List<ExplosionTile> ExplosionTileList;
        bool IsAnimate = true;


        public int CurrentFrame = 0;
        public int SwitchTime = 50;
        int switchCounter = 0;
        public List<int> SequenceFrame = new List<int>();

        public event Action EndAnimate;

        public ExplosionTilesList()
        {
            ExplosionTileList = new List<ExplosionTile>();
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
                    CurrentFrame++;

                    if (CurrentFrame >= SequenceFrame.Count)
                    {
                        EndAnimate();
                        CurrentFrame = 0;
                    }

                    for (int i = 0; i < ExplosionTileList.Count; i++)
                    {
                        ExplosionTileList[i].SourceRectangle.X = SequenceFrame[CurrentFrame] * ExplosionTileList[i].SourceRectangle.Width;
                    }

                }

            }
        }

        public void Draw(SpriteBatch spriteBatch, ref Texture2D texture)
        {
            for (int i = 0; i < ExplosionTileList.Count; i++)
            {
                spriteBatch.Draw(texture, ExplosionTileList[i].Position, ExplosionTileList[i].SourceRectangle, Color.White);
            }
        }


    }

    public class ExplosionTile
    {

        public Rectangle SourceRectangle;
        public Vector2 Position { get; set; }



        public ExplosionTile(Rectangle sourceRectangles, Vector2 position)
        {
            SourceRectangle = sourceRectangles;
            Position = position;
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}

