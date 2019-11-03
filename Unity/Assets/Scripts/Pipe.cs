﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Minden csőnél előforduló elemek
/// </summary>
public class Pipe : MonoBehaviour
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

    public void SetWater(Material mat)
    {
        GetComponent<MeshRenderer>().material = mat;
    }
}
