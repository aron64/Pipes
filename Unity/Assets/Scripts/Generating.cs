using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generating : MonoBehaviour
{
    public int[,] map;
    public float map_size = 8f;
    void Start()
    {
        
    }

    public void SetSize(float size)
    {
        map_size = size;
    }
}
