using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Kihívás játékmód
/// </summary>
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
    public Material pipeMat;
    public Material[,] waterstate;

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
    /// Az elforgatás hangja
    /// </summary>
    public AudioSource turnSound;

    /// <summary>
    /// A játékot inicializáló funkció
    /// </summary>
    public void StartGame()
    {
        //Térkép generálása
        MapMatrix = MapGen.GenerateMap();

        //Mód lefoglalása
        MenuManagerScript.activeMode = 1;

        //A jelenlegi kijárat megkeresése
        FindExit();

        //A csövek elhelyezése
        SpawnPipes();

        //Init
        PipeTurned();
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
                pipeObj.AddComponent<MeshCollider>();
                pipeObj.AddComponent<Pipe>();
                pipeObj.GetComponent<Pipe>().selfpos = new int[] { i - 1, j - 1 };
                switch (MapMatrix[i, j])
                {
                    case 2:
                        pipeObj.AddComponent<StraightPipeBehaviour>();
                        pipeObj.AddComponent<StraightAttachGameModeOne>();
                        pipeObj.GetComponent<Pipe>().flowDir = new int[,] { { -1, 1 }, { 0, 0 } };
                        break;
                    case 3:
                        pipeObj.AddComponent<CurvedPipeBehaviour>();
                        pipeObj.AddComponent<CurvedAttachGameModeOne>();
                        pipeObj.GetComponent<Pipe>().flowDir = new int[,] { { 1, 0 }, { -1, 0 } };
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
    /// Megkeresi egy pozición álló cső lehetséges folyásait
    /// </summary>
    /// <param name="pos">cső x,y poziciója</param>
    /// <param name="flowDir">lehetséges folyásirányok</param>
    /// <returns>Egy listát int[]-ben tárolt indexekkel</returns>
    public List<int[]> GetHoles(int[] pos, int[,] flowDir)
    {
        List<int[]> holes = new List<int[]>();
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                if (flowDir[i,j]!=0)
                {
                    int[] checkPos = pos.ToList().ToArray();
                    checkPos[i] = pos[i] + flowDir[i, j];
                    //Debug.Log(string.Format("{0} {1} {2} {3}", pos[0], pos[1], checkPos[0], checkPos[1]));
                    if (checkPos[0]<0 || checkPos[1]<0 || checkPos[0]>=Map.MapSize || checkPos[1] >= Map.MapSize)
                    {
                        
                        if (checkPos[0]==exit-1&& checkPos[1]==Map.MapSize)
                        {
                            holes.Add(new int[] { -6, -6 });
                        }
                        continue;
                    }
                    holes.Add(checkPos.ToList().ToArray());
                }
            }
        }
        return holes;
    }
    
    /// <summary>
    /// Ha vége a játéknak
    /// </summary>
    public void Won()
    {
        FindObjectOfType<MenuManagerScript>().exitMenuGm1.SetActive(true);
        MenuManagerScript.activeMode = 0;
    }

    public void CleanUp()
    {
        foreach (var item in pipes)
        {
            Destroy(item);
        }
        MenuManagerScript.activeMode = 0;
    }

    /// <summary>
    /// A vízállás frissítése rekurzívan
    /// </summary>
    /// <param name="nP">Következő cső</param>
    /// <param name="deny">Előző állásba nem térünk vissza</param>
    public void WaterUpgrade(int[] nP, int[] deny)
    {
        GameObject p = pipes[nP[0], nP[1]];

        waterstate[nP[0], nP[1]] = water;

        int[] pos = p.GetComponent<Pipe>().selfpos;
        int[,] fDir = p.GetComponent<Pipe>().flowDir;
        List<int[]> holes = GetHoles(pos, fDir);
        foreach (var item in holes)
        {
            if (item[0] == -6)
            {
                // játék vége
                Won();break;
            } 
            int[] nextPos = pipes[item[0], item[1]].GetComponent<Pipe>().selfpos;
            int[,] nfDir = pipes[item[0], item[1]].GetComponent<Pipe>().flowDir;

            List<int[]> nHoles = GetHoles(nextPos, nfDir);
            foreach (var item1 in nHoles)
            {
                if (item1[0] == pos[0] && item1[1] == pos[1] && (item[0]!=deny[0] || item[1]!= deny[1]))
                {
                    WaterUpgrade(nextPos, nP);
                }
            }
        }
    }

    /// <summary>
    /// Minden fordítás után meghatározzuk a vizes mezőket
    /// </summary>
    public void PipeTurned()
    {
        waterstate = new Material[Map.MapSize, Map.MapSize];

        // Alapból minden elem csőszínű
        for (int i = 0; i < Map.MapSize; i++)
        {
            for (int j = 0; j < Map.MapSize; j++)
            {
                waterstate[i, j] = pipeMat;
            }
        }
        int[,] a = pipes[1, 0].GetComponent<Pipe>().flowDir;
        // Eléri a víz a rendszert?
        if (pipes[1, 0].GetComponent<Pipe>().flowDir[1, 0] == -1)
        {
            WaterUpgrade(new int[] { 1, 0 }, new int[] { -5, -5 });
        }

        // A tényleges átszínezés
        for (int i = 0; i < Map.MapSize; i++)
        {
            for (int j = 0; j < Map.MapSize; j++)
            {
                pipes[i, j].GetComponent<Pipe>().SetWater(waterstate[i, j]);
            }
        }
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
        if (GetComponent<StraightPipeBehaviour>().wait)
        {
            return;
        }
        FindObjectOfType<GameModeOne>().turnSound.Play();
        GetComponent<StraightPipeBehaviour>().StartCoroutine(GetComponent<StraightPipeBehaviour>().TurnOverTime());
        StartCoroutine(WaitTurn());
       // FindObjectOfType<GameModeOne>().PipeTurned();

    }
    IEnumerator WaitTurn()
    {
        while (GetComponent<StraightPipeBehaviour>().wait)
        {
            yield return new WaitForSeconds(0.1f);
        }
        GetComponent<StraightPipeBehaviour>().Turn();
        FindObjectOfType<GameModeOne>().PipeTurned();
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
        if (GetComponent<CurvedPipeBehaviour>().wait)
        {
            return;
        }
        FindObjectOfType<GameModeOne>().turnSound.Play();
        GetComponent<CurvedPipeBehaviour>().StartCoroutine(GetComponent<CurvedPipeBehaviour>().TurnOverTime());
        StartCoroutine(WaitTurn());
        // FindObjectOfType<GameModeOne>().PipeTurned();
    }
    IEnumerator WaitTurn()
    {
        while (GetComponent<CurvedPipeBehaviour>().wait)
        {
            yield return new WaitForSeconds(0.1f);
        }
        GetComponent<CurvedPipeBehaviour>().Turn();
        FindObjectOfType<GameModeOne>().PipeTurned();
    }
}
