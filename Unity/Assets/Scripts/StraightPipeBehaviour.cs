using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightPipeBehaviour : MonoBehaviour
{
    /// <summary>
    /// Az objektum saját pozíciója
    /// </summary>
    public int[] selfpos { get; set; }

    /// <summary>
    /// Merre tud menni a víz?
    /// </summary>
    public int[,] flowDir;

    /// <summary>
    /// Eléri a víz?
    /// </summary>
    public bool water = false;
    
    /// <summary>
    /// Egy cső elforgatása
    /// </summary>
    public int[,] Turn()
    {
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 90f, 0f);

        flowDir = new int[,] { };
        int y =(int)Mathf.Round(transform.rotation.y);
        switch ((y / 90) % 2)
        {
            case 0:
                flowDir = new int[,] { { 1, -1 }, { 0, 0 } };
                break;
            case 1:
                flowDir = new int[,] { { 0, 0 }, { 1, -1 } };
                break;
            default:break;
        }
        return flowDir;
    }
}
