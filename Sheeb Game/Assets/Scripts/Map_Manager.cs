using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Manager : MonoBehaviour
{
    private bool map_Generated;

    [Header("Map Settings:")]
    public GameObject tile;
    public int map_Width;
    public int map_Height;

    [Header("Tile Settings:")]
    [Range(0.0f, 100.0f)] public float rainbow_Chance;

    // Update is called once per frame
    void Update()
    {
        if (!map_Generated)
        {
            Generate_Map();
            map_Generated = true;
        }
    }

    void Generate_Map()
    {
        float spacing_X = (-tile.transform.GetComponent<BoxCollider2D>().size.x * (map_Width / 2f)) + (tile.transform.GetComponent<BoxCollider2D>().size.x / 2f);
        float spacing_Y = (tile.transform.GetComponent<BoxCollider2D>().size.y * (map_Height / 2f)) - (tile.transform.GetComponent<BoxCollider2D>().size.y / 2f);

        for (int i = 0; i < map_Width; i++)
        {
            for (int j = 0; j < map_Height; j++)
            {
                GameObject new_Tile = Instantiate(tile, transform);

                int color = Random.Range(0, 3);

                if (Random.Range(0.0f, 100.0f) < rainbow_Chance)
                    color = 3;

                switch (color)
                {
                    case 0:
                        new_Tile.GetComponent<SpriteRenderer>().color = Color.red;
                        new_Tile.tag = "Red_Grass";
                        break;
                    case 1:
                        new_Tile.GetComponent<SpriteRenderer>().color = Color.blue;
                        new_Tile.tag = "Blue_Grass";
                        break;
                    case 2:
                        new_Tile.GetComponent<SpriteRenderer>().color = Color.green;
                        new_Tile.tag = "Green_Grass";
                        break;
                    case 3:
                        new_Tile.GetComponent<SpriteRenderer>().color = Color.magenta;
                        new_Tile.tag = "Rainbow_Grass";
                        break;
                }

                new_Tile.transform.position += new Vector3(spacing_X, spacing_Y, 0f);
                spacing_X += tile.transform.GetComponent<BoxCollider2D>().size.x;
            }

            spacing_Y -= tile.transform.GetComponent<BoxCollider2D>().size.y;
            spacing_X = (-tile.transform.GetComponent<BoxCollider2D>().size.x * (map_Width / 2f)) + (tile.transform.GetComponent<BoxCollider2D>().size.x / 2f);
        }
    }
}
