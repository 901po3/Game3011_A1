using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    const int MAP_SIZE = 16;
    [SerializeField] GameObject tile;

    private void Start()
    {
        if(tile != null)
        {
            float tileSize = tile.transform.localScale.x;

            for(int i = 0; i < MAP_SIZE; i++)
            {
                for(int j = 0; j < MAP_SIZE; j++)
                {

                }
            }
        }     
    }
}
