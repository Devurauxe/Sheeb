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
    private Vector3 centerMap;

    void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        //Generates a map based on the input factors
        for (int x = 0; x < tileGridX; x++) //Create X columns
        {
            Vector3 placePos = transform.position; //Get current position tracker to keep track of where tiles are being placed
            for (int y = 0; y < tileGridY; y++) //Create tiles of the appropriate number of rows in each column
            {
                placePos.x = (tileRowOffset * x) + (tileOffset.x * y); placePos.y = (tileOffset.y * y); //Prep position for instantiated tile to go
                GameObject newTile = Instantiate(grassTile, transform); //Instantiate new map tile
                newTile.transform.position = placePos; //Set newTile position

                //Get Center of Map:
                if (x == (tileGridX/2) - (tileGridX%2) && y == (tileGridY/2) + (tileGridY%2)) { centerMap = newTile.transform.position; }
            }
        }
        transform.position = -centerMap; //Set whole map position to center of camera
    }
}
