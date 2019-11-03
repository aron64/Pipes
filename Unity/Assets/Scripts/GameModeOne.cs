using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameModeOne : MonoBehaviour
{
    /// <summary>
    /// Csőfajta
    /// </summary>
    public GameObject  TPipe, XPipe, StraightPipe,CurvedPipe;

    /// <summary>
    /// Kék színű material
    /// </summary>
    public Material water;
    
    /// <summary>
    /// A csöobjektumokat tároló mátrix
    /// </summary>
    public GameObject[,] pipes;
    
    /// <summary>
    /// Az alsó fal i-edik eleme a kijárat
    /// </summary>
    public int exit;

    /// <summary>
    /// A jelenleg legenerált csövek típusa
    /// </summary>
    public int[,] MapMatrix;
        
    /// <summary>
    /// A játékot inicializáló funkció
    /// </summary>
    public void StartGame()
    {
        MapMatrix = MapGen.GenerateMap();

        //A jelenlegi kijárat megkeresése
        FindExit();

        //A csövek elhelyezése
        SpawnPipes();

    }
    /// <summary>
    /// Kijárat indexének mentése az exit változóba
    /// </summary>
    public void FindExit()
    {
        for (int i = 0; i < Map.MapSize + 2; i++)
        {
            if (MapMatrix[i, Map.MapSize + 1] == 9)
            {
                exit = i;
                FindObjectOfType<Map>().SetExit(i);
            }
        }
    }

    /// <summary>
    /// A csövek elhelyezése
    /// 
    /// A megfelelő viselkedéskomponenseket csatoljuk a csövekhez, majd a pipes mátrixba pakoljuk őket
    /// </summary>
    public void SpawnPipes()
    {
        GameObject[] Pipes = new GameObject[] { XPipe, TPipe, StraightPipe, CurvedPipe };
        pipes = new GameObject[Map.MapSize, Map.MapSize];
        for (int i = 1; i < Map.MapSize + 1; i++)
        {
            for (int j = 1; j < Map.MapSize + 1; j++)
            {
               
                GameObject pipeObj = Instantiate(Pipes[MapMatrix[i, j]], Map.positions[i - 1, j - 1], Quaternion.identity);
                pipeObj.AddComponent<BoxCollider>();
                switch (MapMatrix[i, j])
                {
                    case 2:
                        pipeObj.AddComponent<StraightPipeBehaviour>();
                        pipeObj.AddComponent<StraightAttachGameModeOne>();
                        pipeObj.GetComponent<StraightPipeBehaviour>().selfpos = new int[] { i - 1, j - 1 };
                        break;
                    case 3:
                        pipeObj.AddComponent<CurvedPipeBehaviour>();
                        pipeObj.AddComponent<CurvedAttachGameModeOne>();
                        pipeObj.GetComponent<CurvedPipeBehaviour>().selfpos = new int[] { i - 1, j - 1 };
                        //Helyzetbe forgatjuk a síkra
                        pipeObj.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                        break;
                    default:
                        break;
                }
                pipes[i - 1, j - 1] = pipeObj;
            }
        }
    }

    /// <summary>
    /// Az elfordítás utáni pálya vizsgálata
    /// </summary>
    /// <param name="pos">Elfordított elem x,y poziciója</param>
    /// <param name="neighbors">lehetséges csatlakozó szomszédok</param>
    public void PipeTurned(int[] pos, int[,] neighbors)
    {
        
    }

    /// <summary>
    /// A vízállás frissítése
    /// </summary>
    public void WaterUpgrade()
    {

    }

    /// <summary>
    /// Víz materialt ad az objektumhoz
    /// </summary>
    /// <param name="obj">Gameobject obj</param>
    public void AddWater(GameObject obj)
    {
        obj.GetComponent<MeshRenderer>().material = water;
    }

}

/// <summary>
/// Csatoljuk az egyenescső elforgatását a játékvezérlőhöz
/// A csövek függetlenségét elősegítő osztály
/// </summary>
public class StraightAttachGameModeOne : MonoBehaviour
{
    /// <summary>
    /// Elforgatjuk a csövet, és visszaadjuk a lehetséges víztovábbítás irányait a vezérlőnek
    /// </summary>
    void OnMouseDown()
    {
        FindObjectOfType<GameModeOne>().PipeTurned(GetComponent<StraightPipeBehaviour>().selfpos, GetComponent<StraightPipeBehaviour>().Turn());
    }
}

/// <summary>
/// Csatoljuk az görbecső elforgatását a játékvezérlőhöz
/// A csövek függetlenségét elősegítő osztály
/// </summary>
public class CurvedAttachGameModeOne : MonoBehaviour
{
    /// <summary>
    /// Elforgatjuk a csövet, és visszaadjuk a lehetséges víztovábbítás irányait a vezérlőnek
    /// </summary>
    void OnMouseDown()
    {
        FindObjectOfType<GameModeOne>().PipeTurned(GetComponent<CurvedPipeBehaviour>().selfpos, GetComponent<CurvedPipeBehaviour>().Turn());
    }
}

/// <summary>
/// MapGen Osztály
/// Pályát generál az algoritmus legalább egy útvonallal.
/// fv: public static int[,] GenerateMap()
/// </summary>
class MapGen
{
    static void Main(string[] args){}
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
        
        int mapSize = Map.MapSize+2; // 8x8 without walls
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
        map[1, 0] = 8; //Starting point, fix
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
                int index = Random.Range(0,directions.Count());
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