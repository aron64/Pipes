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
/// static int[] MapSize - OptionsMenu script változtatja
/// static int[] mapShit - a balfelső sarok kordinátái egy Cube 
///                                         méreteit beszámítva
/// Publikus fv-k, metódusok:
/// void CreateEmptyMap - elhelyezi az elkerítést, 
///                                   beleértve a csapot és a kijáratot
/// 
/// </summary>
public class Map : MonoBehaviour
{


    /// <summary>
    /// Refers to the place maintained for the pipes, excluding the wall
    /// </summary>
    public static int MapSize { get; set; }

    /// <summary>
    /// Blue Block Material
    /// </summary>
    public Material water;

    /// <summary>
    /// Prefabs of an empty playfield
    /// </summary>
    public GameObject PlanePrefab, CubeWall;
    
    /// <summary>
    /// Storing the actual objects of the wall
    /// </summary>
    List<GameObject> walls;

    /// <summary>
    /// Storing the actual object of the Plane
    /// </summary>
    GameObject MainPlane;


    // Start is called before the first frame update
    void Start()
    {
        MapSize = 10;//3.8+4.5=8.3
        

        if (MenuManagerScript.test) { TDD(); }
    }
    /// <summary>
    /// Test Driven Dev, showing the map for 1s
    /// </summary>
    void TDD()
    {
            CreateEmptyMap();
            Destroy(MainPlane, 1f);
            for (int i = 0; i<walls.Count; i++)
            {
                Destroy(walls[i], 1f);
            }
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Generating an empty map
    /// 
    /// cubes: List walls;
    /// Plane : Gameobject MainPlane
    /// (locals)
    /// </summary>
    public void CreateEmptyMap()
    {
        // Resetting the list
        walls = new List<GameObject>();

        // Scaling vector
        float scale = ((MapSize+2) / 10.0f);

        // Main Plane placement
        MainPlane = Instantiate(PlanePrefab, new Vector3(0f, -8*scale, 3.8f*scale), Quaternion.identity);
        MainPlane.transform.localScale = new Vector3(scale, 1, scale);

        // Create Wall around it's edge. (inside the square)
        for (int i = 0; i < MapSize+2; i++)
        {
            for (int j = 0; j < MapSize+2; j++)
            {
                if ((i != 0 && i != MapSize+1) && (j != 0 && j != MapSize+1))
                {
                    continue;
                }
                walls.Add(Instantiate(CubeWall, new Vector3(-((MapSize+2)/2)+0.5f + j,
                                                             (-8)*scale+0.5f,
                                                             ((3.8f * scale + ((MapSize + 2) / 2) - 0.5f) - i)), Quaternion.identity));
            }
            walls[1].GetComponents<MeshRenderer>()[0].material = water;
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
