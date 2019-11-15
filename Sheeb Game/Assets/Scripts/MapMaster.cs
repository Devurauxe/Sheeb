using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaster : MonoBehaviour
{
    //Governs random map generation

    //Objects and Components:
    public GameObject grassTile;
    public Vector2 tileOffset;
    public float tileRowOffset; //How far away (along the x axis) tiles are from each other
    [Space()]
    public int tileGridX;
    public int tileGridY;

    void Start()
    {
        
    }

}
