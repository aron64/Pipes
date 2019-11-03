using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightPipeBehaviour : MonoBehaviour
{
    /// <summary>
    /// Egy cső elforgatása
    /// </summary>
    public int[,] Turn()
    {
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 90f, 0f);

        GetComponent<Pipe>().flowDir = new int[,] { };
        int y = (int)Mathf.Round(transform.rotation.eulerAngles.y);
        switch ((y / 90)%2)
        {
            case 0:
                GetComponent<Pipe>().flowDir = new int[,] { { -1, 1 }, { 0, 0 } };
                break;
            case 1:
                GetComponent<Pipe>().flowDir = new int[,] { { 0, 0 }, { -1, 1 } };
                break;
            default:break;
        }
        return GetComponent<Pipe>().flowDir;
    }

}
