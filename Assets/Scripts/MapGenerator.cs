using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    const int MAP_SIZE = 16;
    [SerializeField] GameObject tile;
    GameObject[,] tileMap = new GameObject[MAP_SIZE, MAP_SIZE];

    private void Start()
    {
        if (tile != null)
        {
            float tileSize = tile.transform.localScale.x + tile.transform.localScale.x / 8;

            for(int i = 0; i < MAP_SIZE; i++)
            {
                for(int j = 0; j < MAP_SIZE; j++)
                {
                    GameObject newTile = Instantiate(tile, transform);
                    newTile.transform.position = new Vector3(j * tileSize, 0, i * tileSize);
                    tileMap[j, i] = newTile;
                }
            }
        }     
    }



    void MakeResourceSet(int x, int y, float value)
    {
        //tileMap[x, y];
    }
}
