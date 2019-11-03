using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedPipeBehaviour : MonoBehaviour
{
    /// <summary>
    /// Egy cső elforgatása
    /// </summary>
    public int[,] Turn()
    {
        transform.rotation = Quaternion.Euler(90f, transform.rotation.eulerAngles.y+90f, 0f);
     
        GetComponent<Pipe>().flowDir = new int[,] { };
        int y = (int)Mathf.Round(transform.rotation.eulerAngles.y);
        switch ((y / 90))
        {
            case 0:
                GetComponent<Pipe>().flowDir = new int[,] { { 1, 0 }, { -1, 0 } };
                break;
            case 1:
                GetComponent<Pipe>().flowDir = new int[,] { { 1, 0 }, { 1, 0 } };
                break;
            case 2:
                GetComponent<Pipe>().flowDir = new int[,] { { -1, 0 }, { 1, 0 } };
                break;
            case 3:
                GetComponent<Pipe>().flowDir = new int[,] { { -1, 0 }, { -1, 0 } };
                break;
            default:
                break;
        }
        return GetComponent<Pipe>().flowDir;
    }
}

