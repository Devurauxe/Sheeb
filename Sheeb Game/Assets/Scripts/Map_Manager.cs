using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Manager : MonoBehaviour
{
    [Header("Map Settings:")]
    public GameObject tile;
    public int map_Width;
    public int map_Height;

    [Header("Tile Settings:")]
    [Range(0.0f, 100.0f)] public float rainbow_Chance;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Generate_Map()
    {
        float spacing_X = 0f;
        float spacing_Y = 0f;

        for (int i = 0; i < map_Width; i++)
        {
            for (int j = 0; j < map_Height; j++)
            {
                GameObject new_Tile = Instantiate(tile, transform);
                new_Tile.transform.position += new Vector3(spacing_X, spacing_Y, 0f);
                spacing_X += tile.transform.localScale.x;
            }

            spacing_Y += tile.transform.localScale.y;
        }
    }
}
