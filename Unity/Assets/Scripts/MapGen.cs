using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// MapGen Osztály
/// Pályát generál az algoritmus legalább egy útvonallal.
/// fv: public static int[,] GenerateMap()
/// </summary>
class MapGen
{
    static void Main(string[] args) { }
    /// <summary>
    /// Egy csőtérképet generáló algoritmus, fallal a szélén
    /// bejárat, kijárat és random csövek, legalább 1 útvonallal
    /// 0 = ellenőrizetlen
    /// 1 = fal
    /// 2 = egyenes
    /// 3 = kanyar
    /// 4 = meghatározatlan
    /// 8 = bejárat (mindig x = 0, y = 2)
    /// 9 = kijárat
    /// In the final version there are only 1,2,3,8,9
    /// </summary>
    /// <returns> int[MapSize+2, MapSize+2] map - csovek típussal, ki- és bejárat</returns>
    public static int[,] GenerateMap()
    {

        int mapSize = Map.MapSize + 2; // 8x8 without walls
        int[,] map;

        #region Empty map generation
        map = new int[mapSize, mapSize];
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                if (x == 0 || x == mapSize - 1 || y == 0 || y == mapSize - 1)
                {
                    map[x, y] = 1; // 1 = wall
                }
                else
                {
                    map[x, y] = 0; // 0 = empty
                }
            }
        }
        map[2, 0] = 8; //Starting point, fix
        int[] exit = new int[] { Random.Range(1, mapSize - 2), mapSize - 1 };
        map[exit[0], exit[1]] = 9;
        #endregion
        #region Path generation
        int[] position = new int[] { 2, 1 };
        Stack<int[]> path = new Stack<int[]>();
        path.Push(new int[] { 2, 0 });
        bool done = false;
        while (!done)
        {
            List<string> directions = new List<string>() { "left", "down", "right", "up" };
            bool step = false;
            while (!step)
            {
                if (directions.Count == 0)
                {
                    map[position[0], position[1]] = 4;
                    position = path.First();
                    path.Pop();
                    break;
                }
                int index = Random.Range(0, directions.Count());
                if (directions[index] == "left") //go left
                {
                    if (map[position[0] - 1, position[1]] == 0)
                    {
                        path.Push(new int[] { position[0], position[1] });
                        map[position[0], position[1]] = 2;
                        position[0]--;
                        step = true;
                        break;
                    }
                    else
                    {
                        directions.Remove("left");
                    }
                }
                else if (directions[index] == "down") //go down
                {
                    if (map[position[0], position[1] + 1] == 0)
                    {
                        path.Push(new int[] { position[0], position[1] });
                        map[position[0], position[1]] = 2;
                        position[1]++;
                        step = true;
                        break;
                    }
                    else if (map[position[0], position[1] + 1] == 9)
                    {
                        path.Push(new int[] { position[0], position[1] });
                        map[position[0], position[1]] = 2;
                        done = true;
                        break;
                    }
                    else
                    {
                        directions.Remove("down");
                    }
                }
                else if (directions[index] == "right") //go right
                {
                    if (map[position[0] + 1, position[1]] == 0)
                    {
                        path.Push(new int[] { position[0], position[1] });
                        map[position[0], position[1]] = 2;
                        position[0]++;
                        step = true;
                        break;
                    }
                    else
                    {
                        directions.Remove("right");
                    }
                }
                else if (directions[index] == "up") //go up
                {
                    if (map[position[0], position[1] - 1] == 0)
                    {
                        path.Push(new int[] { position[0], position[1] });
                        map[position[0], position[1]] = 2;
                        position[1]--;
                        step = true;
                        break;
                    }
                    else
                    {
                        directions.Remove("up");
                    }
                }

            }
        }
        path.Push(new int[] { exit[0], exit[1] });
        List<int[]> pathlist = path.ToList();
        #endregion
        #region Adding turns
        for (int i = pathlist.Count - 2; i > 0; i--)
        {
            if (Mathf.Abs(pathlist[i - 1][0] - pathlist[i + 1][0]) == 2 || Mathf.Abs(pathlist[i - 1][1] - pathlist[i + 1][1]) == 2)
            {

            }
            else
            {
                map[pathlist[i][0], pathlist[i][1]] = 3;
            }
        }
        #endregion
        #region Replacing 0s and 4s
        //Randomizing fields not belonging to the path
        for (int i = 1; i < mapSize - 1; i++)
        {
            for (int j = 1; j < mapSize - 1; j++)
            {
                if (map[i, j] == 0 || map[i, j] == 4)
                {
                    map[i, j] = Random.Range(2, 4);
                }
            }
        }
        #endregion
        return map;
    }
}