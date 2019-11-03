using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Map osztály
/// A játékteret definiáló objektumokat,
/// Illetve a játékteret előkészítő függvényeket,
///                      metódusokat tartalmazza.
/// 
/// NEM ez az osztály fogja kezelni a csöveket.
/// 
/// Publikus komponensek:
/// 
/// public static int[] MapSize - OptionsMenu script változtatja
/// 
/// public static Vector3[,] positions
/// 
/// Publikus fv-k, metódusok:
/// void CreateEmptyMap - elhelyezi az elkerítést, 
///                                   beleértve a csapot és a kijáratot
/// 
/// </summary>
public class Map : MonoBehaviour
{
    /// <summary>
    /// A falon belül található játéktér mérete
    /// </summary>
    /// <remarks>ADS</remarks>
    public static int MapSize { get; set; }

    /// <summary>
    /// A játéktér csövek számára fenntartott kordinátái
    /// </summary>
    public static Vector3[,] positions { get; set; }

    /// <summary>
    /// Kék szín
    /// </summary>
    public Material water;

    /// <summary>
    /// Prefabok az üres játéktérhez
    /// </summary>
    public GameObject PlanePrefab, CubeWall;
    
    /// <summary>
    /// A fal-objektumok tárolása
    /// </summary>
    List<GameObject> walls;

    /// <summary>
    /// A Plane-objektum tárolása
    /// </summary>
    GameObject MainPlane;


    // Start is called before the first frame update
    void Start()
    {
        

        if (MenuManagerScript.test) { TDD(); }
    }
    /// <summary>
    /// TDD, játéktér mutatása 1mp-ig
    /// </summary>
    void TDD()
    {
        CreateEmptyMap();
        DestroyMap(1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Üres játéktér generálása
    /// méretnek megfelelően
    /// 
    /// cubes : List walls
    /// 
    /// Plane : Gameobject MainPlane
    /// 
    /// </summary>
    public void CreateEmptyMap()
    {
        // Új lista
        walls = new List<GameObject>();

        // A csövek poziciókordinátáinak előkészítése
        positions = new Vector3[MapSize,MapSize];

        // Alkalmazkodás a pályamérethez - szorzó
        float scale = ((MapSize+2) / 10.0f);

        // A pálya elhelyezése
        MainPlane = Instantiate(PlanePrefab, new Vector3(0f, -8*scale, 3.8f*scale), Quaternion.identity);
        MainPlane.transform.localScale = new Vector3(scale, 1, scale);

        // Fal a pálya szélén, de azon belül
        for (int i = 0; i < MapSize+2; i++)
        {
            for (int j = 0; j < MapSize+2; j++)
            {
                if ((i != 0 && i != MapSize+1) && (j != 0 && j != MapSize+1))
                {
                    positions[j - 1, i - 1] = new Vector3(-((MapSize + 2) / 2) +0.5f + j,
                                                  (-8) * scale + 0.5f,
                                                  ((3.8f * scale + ((MapSize + 2) / 2) - 0.5f) - i));
                    
                    continue;
                }
                walls.Add(Instantiate(
                                        CubeWall,
                                        new Vector3(-((MapSize + 2) / 2) + 0.5f + j,
                                                  (-8) * scale + 0.5f,
                                                  ((3.8f * scale + ((MapSize + 2) / 2) - 0.5f) - i)),
                                        Quaternion.identity ));
            }
            walls[1].GetComponents<MeshRenderer>()[0].material = water;
        }
    }

    /// <summary>
    /// Kijelöljük grafikusan is a kijáratot
    /// </summary>
    /// <param name="i"></param>
    public void SetExit(int i)
    {
        walls[walls.Count-(MapSize+1-i)].GetComponents<MeshRenderer>()[0].material = water;
    }

    /// <summary>
    /// A játéktér objektumainak megsemmisétése after mp után
    /// </summary>
    /// <param name="after">lebegőpontos szám : Másodperc</param>
    public void DestroyMap(float after)
    {
        Destroy(MainPlane, after);
        for (int i = 0; i < walls.Count; i++)
        {
            Destroy(walls[i], after);
        }
    }

    /// <summary>
    /// public void ShowMap - setting the objects visible
    /// </summary>
    public void ShowMap()
    {
        MainPlane.SetActive(true);
    }

    /// <summary>
    /// public void HideMap - setting the objects invisible
    /// </summary>
    public void HideMap()
    {
        MainPlane.SetActive(true);
    }
}
