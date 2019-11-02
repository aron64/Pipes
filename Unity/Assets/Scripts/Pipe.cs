using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Egy csövet leíró osztály
/// </summary>
public class Pipe
{
    /// <summary>
    /// Egy cső típusa (0:T, 1:X, 2:I, 3:L)
    /// </summary>
    public static string[] PipeTypes = new string[] { "T", "X", "I", "L" };

    /// <summary>
    /// Csőobjektum
    /// </summary>
    GameObject PipeObj;


    /// <summary>
    /// A cső típusának karaktere
    /// </summary>
    public string PipeType;

    /// <summary>
    /// Eléri a víz?
    /// </summary>
    public bool water = false;

    /// <summary>
    /// A cső elfordítása
    /// </summary>
    public int[,] Turn()
    {
        PipeObj.transform.Rotate(new Vector3(0, 90, 0));
        int[,] callNeighbor = new int[,] { { 0, 0 },{ 0, 0} };
        float y = PipeObj.transform.rotation.y;
        if (PipeType == "L")
        {
            switch ((y / 90) % 4)
            {
                case 0:
                    callNeighbor = new int[,] { { -1, 0 }, { 1, 0 } };
                    break;
                case 1:
                    callNeighbor = new int[,] { { 1, 0 }, { 1, 0 } };
                    break;
                case 2:
                    callNeighbor = new int[,] { { 1, 0 }, { -1, 0 } };
                    break;
                case 3:
                    callNeighbor = new int[,] { {- 1, 0 }, { -1, 0 } };
                    break;
                default:
                    Debug.Log("HUGE PROBLEM");
                    break;
            }
        }
        else if (PipeType == "I")
        {
            switch ((y / 90) % 2)
            {
                case 0:
                    callNeighbor = new int[,] { { 1, -1 }, { 0, 0 } };
                    break;
                case 1:
                    callNeighbor = new int[,] { { 0, 0 }, { 1, -1 } };
                    break;
                default:
                    Debug.Log("HUGE PROBLEM");
                    break;
            }
        }
        return callNeighbor;
    }

    public void WaterChanged()
    {
        water = !water;
        PipeObj.GetComponent<MeshRenderer>().material=GameModeOne.waterPub;
    }

    public Pipe(GameObject pipeObj, string type)
    {
        PipeObj = pipeObj;
        PipeType = type;
        if (PipeType == "L")
        {
            PipeObj.transform.Rotate(90, 0, 0);
        }
    }

}
