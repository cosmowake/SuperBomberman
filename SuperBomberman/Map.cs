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
        Texture2D sprite;
        MapTile[,] mapTiles;
        Vector2 Position;
        int tileSize;
        AnimationNoLoopTilesManager explosionList;
        List<Player> playerList = new List<Player>();
        List<Enemy> enemyList = new List<Enemy>();

        List<Bonus> bonusList = new List<Bonus>();
        Teleport teleport;

        AnimatedMapTiles animatedWalls;
        AnimationNoLoopTilesManager destroyWalls;

        public Map(string spritePathMap, string spritePathExplosion)
        {
            ContentManager content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content"); ;
            sprite = content.Load<Texture2D>(spritePathMap);

            explosionList = new AnimationNoLoopTilesManager(spritePathExplosion);
            destroyWalls = new AnimationNoLoopTilesManager(spritePathMap);
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

            AnimationNoLoopTilesList animationNoLoopTilesList = new AnimationNoLoopTilesList();
            animationNoLoopTilesList.SequenceFrame = new List<int>(new int[] { 4, 3, 2, 1, 0, 0, 0, 0, 1, 2, 3, 4 });
            List<MapTile> tempExplosionList = new List<MapTile>();

            MapTile animationNoLoopTile = new MapTile(
                new Rectangle(0, 0 * tileSize, tileSize, tileSize),
                GetVectorByXAndY(startExplosionPosition.X, startExplosionPosition.Y));

            tempExplosionList.Add(animationNoLoopTile);


            for (int i = 0; i < power; i++)
            {
                bool flag = false;
                for (int j = 0; j < destroyWalls.AnimationNoLoopTilesList.Count; j++)
                {
                    for (int k = 0; k < destroyWalls.AnimationNoLoopTilesList[j].AnimationNoLoopTileList.Count; k++)
                    {
                        if (GetXAndYByVector(destroyWalls.AnimationNoLoopTilesList[j].AnimationNoLoopTileList[k].Position) == new Point(startExplosionPosition.X - 1 - i, startExplosionPosition.Y))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                    {
                        break;
                    }
                }
                if (flag)
                {
                    break;
                }

                for (int j = 0; j < playerList.Count; j++)
                {
                    for (int k = 0; k < playerList[j].bombList.Count; k++)
                    {
                        if (GetXAndYByVector(playerList[j].bombList[k].Position) == new Point(startExplosionPosition.X - 1 - i, startExplosionPosition.Y))
                        {
                            playerList[j].bombList[k].explosionAfterDelayed.Start();
                        }
                    }
                }

                if (i == power - 1)
                {
                    if (startExplosionPosition.X - i - 1 >= 0 && !mapTiles[startExplosionPosition.Y, startExplosionPosition.X - i - 1].IsSolid && (animatedWalls.mapTilesList[startExplosionPosition.Y, startExplosionPosition.X - i - 1] == null || !animatedWalls.mapTilesList[startExplosionPosition.Y, startExplosionPosition.X - i - 1].IsSolid))
                    {
                        animationNoLoopTile = new MapTile(
                        new Rectangle(4 * tileSize, 6 * tileSize, tileSize, tileSize),
                        GetVectorByXAndY(startExplosionPosition.X - i - 1, startExplosionPosition.Y));

                        tempExplosionList.Add(animationNoLoopTile);
                    }
                    else
                    {
                        if (animatedWalls.mapTilesList[startExplosionPosition.Y, startExplosionPosition.X - i - 1] != null)
                        {
                            animatedWalls.mapTilesList[startExplosionPosition.Y, startExplosionPosition.X - i - 1].Destroy(startExplosionPosition.X - i - 1, startExplosionPosition.Y);
                        }
                        break;
                    }
                }
                else
                {
                    if (startExplosionPosition.X - i - 1 >= 0 && !mapTiles[startExplosionPosition.Y, startExplosionPosition.X - i - 1].IsSolid && (animatedWalls.mapTilesList[startExplosionPosition.Y, startExplosionPosition.X - i - 1] == null || !animatedWalls.mapTilesList[startExplosionPosition.Y, startExplosionPosition.X - i - 1].IsSolid))
                    {
                        animationNoLoopTile = new MapTile(
                        new Rectangle(4 * tileSize, 2 * tileSize, tileSize, tileSize),
                        GetVectorByXAndY(startExplosionPosition.X - i - 1, startExplosionPosition.Y));

                        tempExplosionList.Add(animationNoLoopTile);
                    }
                    else
                    {
                        if (animatedWalls.mapTilesList[startExplosionPosition.Y, startExplosionPosition.X - i - 1] != null)
                        {
                            animatedWalls.mapTilesList[startExplosionPosition.Y, startExplosionPosition.X - i - 1].Destroy(startExplosionPosition.X - i - 1, startExplosionPosition.Y);
                        }
                        break;
                    }
                }
            }
            for (int i = 0; i < power; i++)
            {
                bool flag = false;
                for (int j = 0; j < destroyWalls.AnimationNoLoopTilesList.Count; j++)
                {
                    for (int k = 0; k < destroyWalls.AnimationNoLoopTilesList[j].AnimationNoLoopTileList.Count; k++)
                    {
                        if (GetXAndYByVector(destroyWalls.AnimationNoLoopTilesList[j].AnimationNoLoopTileList[k].Position) == new Point(startExplosionPosition.X + 1 + i, startExplosionPosition.Y))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                    {
                        break;
                    }
                }
                if (flag)
                {
                    break;
                }

                for (int j = 0; j < playerList.Count; j++)
                {
                    for (int k = 0; k < playerList[j].bombList.Count; k++)
                    {
                        if (GetXAndYByVector(playerList[j].bombList[k].Position) == new Point(startExplosionPosition.X + 1 + i, startExplosionPosition.Y))
                        {
                            playerList[j].bombList[k].explosionAfterDelayed.Start();
                        }
                    }
                }

                if (i == power - 1)
                {
                    if (startExplosionPosition.X + i + 1 < mapTiles.GetLength(1) && !mapTiles[startExplosionPosition.Y, startExplosionPosition.X + i + 1].IsSolid && (animatedWalls.mapTilesList[startExplosionPosition.Y, startExplosionPosition.X + i + 1] == null || !animatedWalls.mapTilesList[startExplosionPosition.Y, startExplosionPosition.X + i + 1].IsSolid))
                    {
                        animationNoLoopTile = new MapTile(
                        new Rectangle(4 * tileSize, 5 * tileSize, tileSize, tileSize),
                        GetVectorByXAndY(startExplosionPosition.X + i + 1, startExplosionPosition.Y));

                        tempExplosionList.Add(animationNoLoopTile);
                    }
                    else
                    {
                        if (animatedWalls.mapTilesList[startExplosionPosition.Y, startExplosionPosition.X + i + 1] != null)
                        {
                            animatedWalls.mapTilesList[startExplosionPosition.Y, startExplosionPosition.X + i + 1].Destroy(startExplosionPosition.X + i + 1, startExplosionPosition.Y);
                        }
                        break;
                    }
                }
                else
                {
                    if (startExplosionPosition.X + i + 1 < mapTiles.GetLength(1) && !mapTiles[startExplosionPosition.Y, startExplosionPosition.X + i + 1].IsSolid && (animatedWalls.mapTilesList[startExplosionPosition.Y, startExplosionPosition.X + i + 1] == null || !animatedWalls.mapTilesList[startExplosionPosition.Y, startExplosionPosition.X + i + 1].IsSolid))
                    {
                        animationNoLoopTile = new MapTile(
                        new Rectangle(4 * tileSize, 2 * tileSize, tileSize, tileSize),
                        GetVectorByXAndY(startExplosionPosition.X + i + 1, startExplosionPosition.Y));

                        tempExplosionList.Add(animationNoLoopTile);
                    }
                    else
                    {
                        if (animatedWalls.mapTilesList[startExplosionPosition.Y, startExplosionPosition.X + i + 1] != null)
                        {
                            animatedWalls.mapTilesList[startExplosionPosition.Y, startExplosionPosition.X + i + 1].Destroy(startExplosionPosition.X + i + 1, startExplosionPosition.Y);
                        }
                        break;
                    }
                }
            }


            for (int i = 0; i < power; i++)
            {
                bool flag = false;
                for (int j = 0; j < destroyWalls.AnimationNoLoopTilesList.Count; j++)
                {
                    for (int k = 0; k < destroyWalls.AnimationNoLoopTilesList[j].AnimationNoLoopTileList.Count; k++)
                    {
                        if (GetXAndYByVector(destroyWalls.AnimationNoLoopTilesList[j].AnimationNoLoopTileList[k].Position) == new Point(startExplosionPosition.X, startExplosionPosition.Y - 1 - i))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                    {
                        break;
                    }
                }
                if (flag)
                {
                    break;
                }


                for (int j = 0; j < playerList.Count; j++)
                {
                    for (int k = 0; k < playerList[j].bombList.Count; k++)
                    {
                        if (GetXAndYByVector(playerList[j].bombList[k].Position) == new Point(startExplosionPosition.X, startExplosionPosition.Y - 1 - i))
                        {
                            playerList[j].bombList[k].explosionAfterDelayed.Start();
                        }
                    }
                }

                if (i == power - 1)
                {
                    if (startExplosionPosition.Y - i - 1 >= 0 && !mapTiles[startExplosionPosition.Y - i - 1, startExplosionPosition.X].IsSolid && (animatedWalls.mapTilesList[startExplosionPosition.Y - 1 - i, startExplosionPosition.X] == null || !animatedWalls.mapTilesList[startExplosionPosition.Y - i - 1, startExplosionPosition.X].IsSolid))
                    {
                        animationNoLoopTile = new MapTile(
                        new Rectangle(4 * tileSize, 3 * tileSize, tileSize, tileSize),
                        GetVectorByXAndY(startExplosionPosition.X, startExplosionPosition.Y - i - 1));

                        tempExplosionList.Add(animationNoLoopTile);
                    }
                    else
                    {
                        if (animatedWalls.mapTilesList[startExplosionPosition.Y - 1 - i, startExplosionPosition.X] != null)
                        {
                            animatedWalls.mapTilesList[startExplosionPosition.Y - 1 - i, startExplosionPosition.X].Destroy(startExplosionPosition.X, startExplosionPosition.Y - 1 - i);
                        }
                        break;
                    }
                }
                else
                {
                    if (startExplosionPosition.Y - i - 1 >= 0 && !mapTiles[startExplosionPosition.Y - i - 1, startExplosionPosition.X].IsSolid && (animatedWalls.mapTilesList[startExplosionPosition.Y - i - 1, startExplosionPosition.X] == null || !animatedWalls.mapTilesList[startExplosionPosition.Y - i - 1, startExplosionPosition.X].IsSolid))
                    {
                        animationNoLoopTile = new MapTile(
                        new Rectangle(4 * tileSize, 1 * tileSize, tileSize, tileSize),
                        GetVectorByXAndY(startExplosionPosition.X, startExplosionPosition.Y - i - 1));

                        tempExplosionList.Add(animationNoLoopTile);
                    }
                    else
                    {
                        if (animatedWalls.mapTilesList[startExplosionPosition.Y - 1 - i, startExplosionPosition.X] != null)
                        {
                            animatedWalls.mapTilesList[startExplosionPosition.Y - 1 - i, startExplosionPosition.X].Destroy(startExplosionPosition.X, startExplosionPosition.Y - 1 - i);
                        }
                        break;
                    }
                }
            }
            for (int i = 0; i < power; i++)
            {
                bool flag = false;
                for (int j = 0; j < destroyWalls.AnimationNoLoopTilesList.Count; j++)
                {
                    for (int k = 0; k < destroyWalls.AnimationNoLoopTilesList[j].AnimationNoLoopTileList.Count; k++)
                    {
                        if (GetXAndYByVector(destroyWalls.AnimationNoLoopTilesList[j].AnimationNoLoopTileList[k].Position) == new Point(startExplosionPosition.X, startExplosionPosition.Y + 1 + i))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                    {
                        break;
                    }
                }
                if (flag)
                {
                    break;
                }
                for (int j = 0; j < playerList.Count; j++)
                {
                    for (int k = 0; k < playerList[j].bombList.Count; k++)
                    {
                        if (GetXAndYByVector(playerList[j].bombList[k].Position) == new Point(startExplosionPosition.X, startExplosionPosition.Y + 1 + i))
                        {
                            playerList[j].bombList[k].explosionAfterDelayed.Start();
                        }
                    }
                }

                if (i == power - 1)
                {
                    if (startExplosionPosition.Y + i + 1 < mapTiles.GetLength(0) && !mapTiles[startExplosionPosition.Y + i + 1, startExplosionPosition.X].IsSolid && (animatedWalls.mapTilesList[startExplosionPosition.Y + i + 1, startExplosionPosition.X] == null || !animatedWalls.mapTilesList[startExplosionPosition.Y + i + 1, startExplosionPosition.X].IsSolid))
                    {

                        animationNoLoopTile = new MapTile(
                        new Rectangle(4 * tileSize, 4 * tileSize, tileSize, tileSize),
                        GetVectorByXAndY(startExplosionPosition.X, startExplosionPosition.Y + i + 1));

                        tempExplosionList.Add(animationNoLoopTile);
                    }
                    else
                    {
                        if (animatedWalls.mapTilesList[startExplosionPosition.Y + i + 1, startExplosionPosition.X] != null)
                        {
                            animatedWalls.mapTilesList[startExplosionPosition.Y + i + 1, startExplosionPosition.X].Destroy(startExplosionPosition.X, startExplosionPosition.Y + 1 + i);
                        }
                        break;
                    }
                }
                else
                {
                    if (startExplosionPosition.Y + i + 1 < mapTiles.GetLength(0) && !mapTiles[startExplosionPosition.Y + i + 1, startExplosionPosition.X].IsSolid && (animatedWalls.mapTilesList[startExplosionPosition.Y + i + 1, startExplosionPosition.X] == null || !animatedWalls.mapTilesList[startExplosionPosition.Y + i + 1, startExplosionPosition.X].IsSolid))
                    {
                        animationNoLoopTile = new MapTile(
                        new Rectangle(4 * tileSize, 1 * tileSize, tileSize, tileSize),
                        GetVectorByXAndY(startExplosionPosition.X, startExplosionPosition.Y + i + 1));

                        tempExplosionList.Add(animationNoLoopTile);
                    }
                    else
                    {
                        if (animatedWalls.mapTilesList[startExplosionPosition.Y + i + 1, startExplosionPosition.X] != null)
                        {
                            animatedWalls.mapTilesList[startExplosionPosition.Y + i + 1, startExplosionPosition.X].Destroy(startExplosionPosition.X, startExplosionPosition.Y + 1 + i);
                        }
                        break;
                    }
                }
            }

            animationNoLoopTilesList.AnimationNoLoopTileList = tempExplosionList;
            animationNoLoopTilesList.EndAnimate += () => { explosionList.AnimationNoLoopTilesList.Remove(animationNoLoopTilesList); };

            explosionList.AnimationNoLoopTilesList.Add(animationNoLoopTilesList);
        }

        void MapGenerator(Vector2 position, int heightMap, int widthMap, int tileSize, string spritePathMap)
        {
            Random random = new Random();

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
                        mapTiles[i, j] = new MapTile(new Rectangle(tileSize * 0, tileSize * 3, tileSize, tileSize), position + positionTemp, true, true);
                    }
                    else if (i == 0 && j == widthMap - 1)
                    {
                        mapTiles[i, j] = new MapTile(new Rectangle(tileSize * 2, tileSize * 3, tileSize, tileSize), position + positionTemp, true, true);
                    }
                    else if (i == heightMap - 1 && j == 0)
                    {
                        mapTiles[i, j] = new MapTile(new Rectangle(tileSize * 0, tileSize * 5, tileSize, tileSize), position + positionTemp, true, true);
                    }
                    else if (i == heightMap - 1 && j == widthMap - 1)
                    {
                        mapTiles[i, j] = new MapTile(new Rectangle(tileSize * 2, tileSize * 5, tileSize, tileSize), position + positionTemp, true, true);
                    }
                    else if (i == 0)
                    {
                        mapTiles[i, j] = new MapTile(new Rectangle(tileSize * 1, tileSize * 3, tileSize, tileSize), position + positionTemp, true, true);
                    }
                    else if (i == heightMap - 1)
                    {
                        mapTiles[i, j] = new MapTile(new Rectangle(tileSize * 1, tileSize * 5, tileSize, tileSize), position + positionTemp, true, true);
                    }
                    else if (j == 0)
                    {
                        mapTiles[i, j] = new MapTile(new Rectangle(tileSize * 0, tileSize * 4, tileSize, tileSize), position + positionTemp, true, true);
                    }
                    else if (j == widthMap - 1)
                    {
                        mapTiles[i, j] = new MapTile(new Rectangle(tileSize * 2, tileSize * 4, tileSize, tileSize), position + positionTemp, true, true);
                    }
                    else if ((i > 1 && i < heightMap - 2 && i % 2 == 0) && (j > 1 && j < widthMap - 2 && j % 2 == 0))
                    {
                        mapTiles[i, j] = new MapTile(new Rectangle(tileSize * 1, tileSize * 4, tileSize, tileSize), position + positionTemp, true, true);
                    }
                    else
                    {
                        if (!((i == 1 && j == 1) || (i == 1 && j == 2) || (i == 2 && j == 1)))
                        {
                            if (random.Next(100) >= 60)
                            {
                                animatedWalls.Add(j, i, tileSize, GetVectorByXAndY(j, i), DestroyTiles);
                            }
                        }
                        mapTiles[i, j] = new MapTile(new Rectangle(tileSize * 0, tileSize * 0, tileSize, tileSize), position + positionTemp, false, false);
                    }

                    positionTemp.X += tileSize;
                }

                positionTemp.X = 0;
                positionTemp.Y += tileSize;
            }

            positionTemp.X = 0;
            positionTemp.Y = 0;


            int k = 5;
            int p = 3;
            animatedWalls.mapTilesList[p, k] = null;
            Vector2 enemyVector = GetVectorByXAndY(k, p);
            Point enemyPoint = new Point((int)enemyVector.X, (int)enemyVector.Y);
            Puropen enemy1 = new Puropen(enemyPoint, 48);
            enemy1.Hit = (() =>
            {
                enemyList.Remove(enemy1);
                Console.WriteLine(enemy1.GetHashCode());
            });
            enemyList.Add(enemy1);

            k = 9;
            p = 5;
            animatedWalls.mapTilesList[p, k] = null;
            enemyVector = GetVectorByXAndY(k, p);
            enemyPoint = new Point((int)enemyVector.X, (int)enemyVector.Y);
            Puropen enemy2 = new Puropen(enemyPoint, 48);
            enemy2.Hit = (() =>
            {
                enemyList.Remove(enemy2);
                Console.WriteLine(enemy2.GetHashCode());
            });
            enemyList.Add(enemy2);

            k = 11;
            p = 11;
            animatedWalls.mapTilesList[p, k] = null;
            enemyVector = GetVectorByXAndY(k, p);
            enemyPoint = new Point((int)enemyVector.X, (int)enemyVector.Y);
            Puropen enemy3 = new Puropen(enemyPoint, 48);
            enemy3.Hit = (() =>
            {
                enemyList.Remove(enemy3);
                Console.WriteLine(enemy3.GetHashCode());
            });
            enemyList.Add(enemy3);

            k = 5;
            p = 7;
            animatedWalls.mapTilesList[p, k] = null;
            enemyVector = GetVectorByXAndY(k, p);
            enemyPoint = new Point((int)enemyVector.X, (int)enemyVector.Y);
            Puropen enemy4 = new Puropen(enemyPoint, 48);
            enemy4.Hit = (() =>
            {
                enemyList.Remove(enemy4);
                Console.WriteLine(enemy4.GetHashCode());
            });
            enemyList.Add(enemy4);

            k = 13;
            p = 5;
            animatedWalls.mapTilesList[p, k] = null;
            enemyVector = GetVectorByXAndY(k, p);
            enemyPoint = new Point((int)enemyVector.X, (int)enemyVector.Y);
            Puropen enemy5 = new Puropen(enemyPoint, 48);
            enemy5.Hit = (() =>
            {
                enemyList.Remove(enemy5);
                Console.WriteLine(enemy5.GetHashCode());
            });
            enemyList.Add(enemy5);

            foreach (Enemy e in enemyList)
            {
                e.LoadContent();
            }

            teleport = new Teleport(GetVectorByXAndY(3,3),48);
            teleport.LoadContent();
            teleport.Image.StartAnimation();


            Bonus bonus1 = new Bonus(BonusType.Count, GetVectorByXAndY(5, 1), tileSize);
            bonus1.Destroy = () =>
            {
                bonusList.Remove(bonus1);
                bonus1.UnloadContent();
                bonus1 = null;
            };
            bonusList.Add(bonus1);


            Bonus bonus2 = new Bonus(BonusType.Power, GetVectorByXAndY(3, 1), tileSize);
            bonus2.Destroy = () =>
            {
                bonusList.Remove(bonus2);
                bonus2.UnloadContent();
                bonus2 = null;
            };
            bonusList.Add(bonus2);

            foreach (Bonus b in bonusList)
            {
                b.LoadContent();
            }
        }

        void DestroyTiles(int x, int y)
        {
            MapTile tempMapTile = new MapTile(new Rectangle(tileSize * 0, tileSize * 2, tileSize, tileSize), animatedWalls.mapTilesList[y, x].Position);
            tempMapTile.IsSolid = true;
            AnimationNoLoopTilesList tempAnimationNoLoopTilesList = new AnimationNoLoopTilesList();
            tempAnimationNoLoopTilesList.SequenceFrame = new List<int>(new int[] { 0, 1, 2, 3, 4, 5 });
            tempAnimationNoLoopTilesList.SwitchTime = 130;
            tempAnimationNoLoopTilesList.AnimationNoLoopTileList.Add(tempMapTile);
            destroyWalls.AnimationNoLoopTilesList.Add(tempAnimationNoLoopTilesList);
            tempAnimationNoLoopTilesList.EndAnimate += () =>
            {
                destroyWalls.AnimationNoLoopTilesList.Remove(tempAnimationNoLoopTilesList);
            };
            tempAnimationNoLoopTilesList.StartAnimation();
            animatedWalls.mapTilesList[y, x] = null;
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
            Player player = new Player("Play/BombermanWhite3x", playerPoint, tileSize);
            player.explosion = Explosion;
            player.BombStandDel = ((Vector2 position) =>
            {
                Point p = GetXAndYByVector(position);
                return GetVectorByXAndY(p.X, p.Y);
            });

            player.Hit = (() =>
            {
                player.Image.ChangeAnimation(4, new List<int>(new int[6] { 0, 1, 2, 3, 4, 5 }));
                player.Image.IsLoop = true;
                player.CanMove = false;
                player.isDead = true;
                player.Image.StartAnimation();
            });

            playerList.Add(player);

            foreach (Player p in playerList)
            {
                p.LoadContent();
            }


        }

        public void UnloadContent()
        {
            foreach (Entity entity in playerList)
            {
                entity.UnloadContent();
            }

            foreach (Enemy e in enemyList)
            {
                e.UnloadContent();
            }
        }

        void Collision(ref Player player, ref MapTile tile)
        {
            if (tile.IsSolid)
            {
                Rectangle tileRect = new Rectangle((int)tile.Position.X, (int)tile.Position.Y, tile.SourceRectangle.Width, tile.SourceRectangle.Height);

                if (player.CollisionRectangle.Intersects(tileRect))
                {
                    Point tilePoint = GetXAndYByVector(tile.Position);
                    Point entityPoint = GetXAndYByVector(new Vector2(player.CollisionRectangle.X, player.CollisionRectangle.Y));
                    Vector2 correctVector = Vector2.Zero;

                    if (player.Velocity.X < 0)
                    {
                        player.ChangePosition(new Point(tileRect.Right, player.CollisionRectangle.Y));

                        if (tilePoint.Y != entityPoint.Y && entityPoint.X - 1 >= 0 && !mapTiles[entityPoint.Y, entityPoint.X - 1].IsSolid)
                        {
                            player.ChangePosition(GetVectorByXAndY(entityPoint.X, entityPoint.Y).ToPoint());
                        }
                    }
                    else if (player.Velocity.X > 0)
                    {
                        player.ChangePosition(new Point(tileRect.Left - player.CollisionRectangle.Width, player.CollisionRectangle.Y));

                        if (tilePoint.Y != entityPoint.Y && entityPoint.X + 1 < mapTiles.GetLength(1) && !mapTiles[entityPoint.Y, entityPoint.X + 1].IsSolid)
                        {
                            player.ChangePosition(GetVectorByXAndY(entityPoint.X, entityPoint.Y).ToPoint());
                        }
                    }
                    else if (player.Velocity.Y < 0)
                    {
                        player.ChangePosition(new Point(player.CollisionRectangle.X, tileRect.Bottom));

                        if (tilePoint.X != entityPoint.X && entityPoint.Y - 1 >= 0 && !mapTiles[entityPoint.Y - 1, entityPoint.X].IsSolid)
                        {
                            player.ChangePosition(GetVectorByXAndY(entityPoint.X, entityPoint.Y).ToPoint());
                        }
                    }
                    else if (player.Velocity.Y > 0)
                    {
                        player.ChangePosition(new Point(player.CollisionRectangle.X, tileRect.Top - player.CollisionRectangle.Height));

                        if (tilePoint.X != entityPoint.X && entityPoint.Y + 1 < mapTiles.GetLength(0) && !mapTiles[entityPoint.Y + 1, entityPoint.X].IsSolid)
                        {
                            player.ChangePosition(GetVectorByXAndY(entityPoint.X, entityPoint.Y).ToPoint());
                        }
                    }
                    player.ChangePosition(correctVector);
                }
            }
        }

        void Collision(ref Enemy enemy, ref MapTile tile)
        {
            if (tile.IsSolid)
            {
                Rectangle tileRect = new Rectangle((int)tile.Position.X, (int)tile.Position.Y, tile.SourceRectangle.Width, tile.SourceRectangle.Height);

                if (enemy.CollisionRectangle.Intersects(tileRect))
                {
                    Point tilePoint = GetXAndYByVector(tile.Position);
                    Point entityPoint = GetXAndYByVector(new Vector2(enemy.CollisionRectangle.X, enemy.CollisionRectangle.Y));
                    Vector2 correctVector = Vector2.Zero;

                    if (enemy.Velocity.X < 0)
                    {
                        enemy.ChangePosition(new Point(tileRect.Right, enemy.CollisionRectangle.Y));

                        if (tilePoint.Y != entityPoint.Y && entityPoint.X - 1 >= 0 && !mapTiles[entityPoint.Y, entityPoint.X - 1].IsSolid)
                        {
                            enemy.ChangePosition(GetVectorByXAndY(entityPoint.X, entityPoint.Y).ToPoint());
                        }
                    }
                    else if (enemy.Velocity.X > 0)
                    {
                        enemy.ChangePosition(new Point(tileRect.Left - enemy.CollisionRectangle.Width, enemy.CollisionRectangle.Y));

                        if (tilePoint.Y != entityPoint.Y && entityPoint.X + 1 < mapTiles.GetLength(1) && !mapTiles[entityPoint.Y, entityPoint.X + 1].IsSolid)
                        {
                            enemy.ChangePosition(GetVectorByXAndY(entityPoint.X, entityPoint.Y).ToPoint());
                        }
                    }
                    else if (enemy.Velocity.Y < 0)
                    {
                        enemy.ChangePosition(new Point(enemy.CollisionRectangle.X, tileRect.Bottom));

                        if (tilePoint.X != entityPoint.X && entityPoint.Y - 1 >= 0 && !mapTiles[entityPoint.Y - 1, entityPoint.X].IsSolid)
                        {
                            enemy.ChangePosition(GetVectorByXAndY(entityPoint.X, entityPoint.Y).ToPoint());
                        }
                    }
                    else if (enemy.Velocity.Y > 0)
                    {
                        enemy.ChangePosition(new Point(enemy.CollisionRectangle.X, tileRect.Top - enemy.CollisionRectangle.Height));

                        if (tilePoint.X != entityPoint.X && entityPoint.Y + 1 < mapTiles.GetLength(0) && !mapTiles[entityPoint.Y + 1, entityPoint.X].IsSolid)
                        {
                            enemy.ChangePosition(GetVectorByXAndY(entityPoint.X, entityPoint.Y).ToPoint());
                        }
                    }
                    enemy.ChangePosition(correctVector);
                    enemy.CollisionChangePosition();
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Player p in playerList)
            {
                p.Update(gameTime);
                Player player = p;

                foreach (Bomb b in player.bombList)
                {
                    Bomb bomb = b;
                    MapTile m = new MapTile(new Rectangle(0, 0, tileSize, tileSize), b.Position, b.IsDestructible, b.IsSolid);
                    Collision(ref player, ref m);

                    if (!bomb.IsSolid)
                    {
                        Rectangle bombRect = new Rectangle((int)bomb.Position.X, (int)bomb.Position.Y, tileSize, tileSize);
                        int collisionCount = 0;

                        foreach (Player collisionPlayer in playerList)
                        {
                            if (collisionPlayer.CollisionRectangle.Intersects(bombRect))
                            {
                                collisionCount++;
                            }
                        }

                        foreach (Enemy collisionEnemy in enemyList)
                        {
                            if (collisionEnemy.CollisionRectangle.Intersects(bombRect))
                            {
                                collisionCount++;
                            }
                        }

                        if (collisionCount == 0)
                        {
                            bomb.IsSolid = true;
                        }
                    }
                }

                foreach (MapTile mapTile in mapTiles)
                {
                    MapTile m = mapTile;
                    Collision(ref player, ref m);
                }

                Rectangle tileTeleportRect = new Rectangle((int)teleport.Image.image.Position.X, (int)teleport.Image.image.Position.Y, tileSize, tileSize);

                if (enemyList.Count == 0 && player.CollisionRectangle.Intersects(tileTeleportRect) && (GetXAndYByVector(player.Position) == GetXAndYByVector(teleport.Image.image.Position)))
                {
                    ScreenManager.Instance.ChangeScreen(new MenuScreen());
                }

                foreach (AnimationNoLoopTilesList mapTileList in destroyWalls.AnimationNoLoopTilesList)
                {
                    foreach (MapTile mapTile in mapTileList.AnimationNoLoopTileList)
                    {
                        MapTile m = mapTile;
                        Collision(ref player, ref m);
                    }
                }

                for(int q = 0; q < bonusList.Count; q++)
                {
                    Rectangle tileRect = new Rectangle((int)bonusList[q].Image.Position.X, (int)bonusList[q].Image.Position.Y, tileSize, tileSize);

                    if (player.CollisionRectangle.Intersects(tileRect) && (GetXAndYByVector(player.Position) == GetXAndYByVector(bonusList[q].Image.Position)))
                    {
                        bonusList[q].GetBonusToPlayer(ref player);
                    }
                }

                foreach (AnimationNoLoopTilesList mapTileList in explosionList.AnimationNoLoopTilesList)
                {
                    foreach (MapTile mapTile in mapTileList.AnimationNoLoopTileList)
                    {
                        MapTile m = mapTile;

                        Collision(ref player, ref m);

                        Rectangle tileRect = new Rectangle((int)mapTile.Position.X, (int)mapTile.Position.Y, mapTile.SourceRectangle.Width, mapTile.SourceRectangle.Height);

                        if (player.CollisionRectangle.Intersects(tileRect) && (GetXAndYByVector(player.Position) == GetXAndYByVector(m.Position)))
                        {
                            player.Hit();
                        }
                    }
                }

                foreach (MapTile mapTile in animatedWalls.mapTilesList)
                {
                    if (mapTile != null)
                    {
                        mapTile.Update(gameTime);
                        MapTile m = mapTile;
                        Collision(ref player, ref m);
                    }
                }
            }


            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Update(gameTime);
                Enemy enemy = enemyList[i];

                foreach (MapTile mapTile in mapTiles)
                {
                    MapTile m = mapTile;
                    Collision(ref enemy, ref m);
                }

                foreach (Player p in playerList)
                {
                    if (enemy.CollisionRectangle.Intersects(p.CollisionRectangle) && (GetXAndYByVector(enemy.Position) == GetXAndYByVector(p.Position)))
                    {
                        p.Hit();
                    }

                    foreach (Bomb b in p.bombList)
                    {
                        MapTile m = new MapTile(new Rectangle(0, 0, tileSize, tileSize), b.Position, b.IsDestructible, b.IsSolid);
                        Collision(ref enemy, ref m);
                    }
                }

                foreach (AnimationNoLoopTilesList mapTileList in destroyWalls.AnimationNoLoopTilesList)
                {
                    foreach (MapTile mapTile in mapTileList.AnimationNoLoopTileList)
                    {
                        MapTile m = mapTile;
                        Collision(ref enemy, ref m);
                    }
                }

                foreach (AnimationNoLoopTilesList mapTileList in explosionList.AnimationNoLoopTilesList)
                {
                    foreach (MapTile mapTile in mapTileList.AnimationNoLoopTileList)
                    {
                        MapTile m = mapTile;

                        Collision(ref enemy, ref m);

                        Rectangle tileRect = new Rectangle((int)mapTile.Position.X, (int)mapTile.Position.Y, mapTile.SourceRectangle.Width, mapTile.SourceRectangle.Height);

                        if (enemy.CollisionRectangle.Intersects(tileRect) && (GetXAndYByVector(enemy.Position) == GetXAndYByVector(m.Position)))
                        {
                            enemy.Hit();
                        }
                    }
                }

                foreach (MapTile mapTile in animatedWalls.mapTilesList)
                {
                    if (mapTile != null)
                    {
                        mapTile.Update(gameTime);
                        MapTile m = mapTile;
                        Collision(ref enemy, ref m);
                    }
                }
            }

            teleport.Update(gameTime);

            explosionList.Update(gameTime);
            destroyWalls.Update(gameTime);
            animatedWalls.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (MapTile mapTile in mapTiles)
            {
                spriteBatch.Draw(sprite, mapTile.Position, mapTile.SourceRectangle, Color.White);
            }

            foreach (Bonus bonus in bonusList)
            {
                Point tempPoint = GetXAndYByVector(bonus.Image.Position);
                if (animatedWalls.mapTilesList[tempPoint.Y, tempPoint.X] == null)
                {
                    bonus.Draw(spriteBatch);
                }
            }

            Point tempTeleportPoint = GetXAndYByVector(teleport.Image.image.Position);
            if (animatedWalls.mapTilesList[tempTeleportPoint.Y, tempTeleportPoint.X] == null)
            {
                teleport.Draw(spriteBatch);
            }

            explosionList.Draw(spriteBatch);

            animatedWalls.Draw(spriteBatch);
            destroyWalls.Draw(spriteBatch);

            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Draw(spriteBatch);
            }

            foreach (Player player in playerList)
            {
                player.Draw(spriteBatch);
            }
        }
    }

    public class MapTile
    {
        public Rectangle SourceRectangle;
        public Vector2 Position { get; set; }
        public bool IsDestructible { get; set; }
        public bool IsSolid { get; set; }

        public DestroidTile Destroy;

        public MapTile(Rectangle sourceRectangle, Vector2 position, bool isDestructible, bool isSolid)
        {
            SourceRectangle = sourceRectangle;
            Position = position;
            IsDestructible = isDestructible;
            IsSolid = isSolid;
        }
        public MapTile(Rectangle sourceRectangles, Vector2 position)
        {
            SourceRectangle = sourceRectangles;
            Position = position;
            IsDestructible = false;
            IsSolid = false;
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
            ContentManager content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            Texture = content.Load<Texture2D>(texturePath);
            SequenceFrame = sequenceFrame;
            mapTilesList = new MapTile[height, weight];
        }

        public void Add(int x, int y, int tileSize, Vector2 position, DestroidTile destroy)
        {
            MapTile mapTileTemp = new MapTile(new Rectangle(0, 1 * tileSize, tileSize, tileSize), position, true, true);
            mapTilesList[y, x] = mapTileTemp;
            mapTilesList[y, x].Destroy = destroy;
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

    public class AnimationNoLoopTilesManager
    {
        Texture2D texture;
        public List<AnimationNoLoopTilesList> AnimationNoLoopTilesList;

        public AnimationNoLoopTilesManager(string texturePath)
        {
            ContentManager content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content"); ;
            texture = content.Load<Texture2D>(texturePath);
            AnimationNoLoopTilesList = new List<AnimationNoLoopTilesList>();
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < AnimationNoLoopTilesList.Count; i++)
            {
                AnimationNoLoopTilesList[i].Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < AnimationNoLoopTilesList.Count; i++)
            {
                AnimationNoLoopTilesList[i].Draw(spriteBatch, ref texture);
            }
        }
    }

    public class AnimationNoLoopTilesList
    {
        public List<MapTile> AnimationNoLoopTileList;
        bool IsAnimate = true;


        public int CurrentFrame = 0;
        public int SwitchTime = 50;
        int switchCounter = 0;
        public List<int> SequenceFrame = new List<int>();

        public event Action EndAnimate;

        public AnimationNoLoopTilesList()
        {
            AnimationNoLoopTileList = new List<MapTile>();
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

                    for (int i = 0; i < AnimationNoLoopTileList.Count; i++)
                    {
                        AnimationNoLoopTileList[i].SourceRectangle.X = SequenceFrame[CurrentFrame] * AnimationNoLoopTileList[i].SourceRectangle.Width;
                    }

                }

            }
        }

        public void Draw(SpriteBatch spriteBatch, ref Texture2D texture)
        {
            for (int i = 0; i < AnimationNoLoopTileList.Count; i++)
            {
                spriteBatch.Draw(texture, AnimationNoLoopTileList[i].Position, AnimationNoLoopTileList[i].SourceRectangle, Color.White);
            }
        }


    }

    public class AnimationNoLoopTile
    {

        public Rectangle SourceRectangle;
        public Vector2 Position { get; set; }



        public AnimationNoLoopTile(Rectangle sourceRectangles, Vector2 position)
        {
            SourceRectangle = sourceRectangles;
            Position = position;
        }

        public void Update(GameTime gameTime)
        {
        }
    }

    public delegate void DestroidTile(int x, int y);
}

