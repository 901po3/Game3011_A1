using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    const int MAP_SIZE = 16;
    [SerializeField] GameObject tile;
    [SerializeField] GameObject coverTile;
    public GameObject[,] tileMap = new GameObject[MAP_SIZE, MAP_SIZE];
    public GameObject[,] coverTileMap = new GameObject[MAP_SIZE, MAP_SIZE];

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
                    newTile.GetComponent<Tile>().SetUpType();
                    newTile.transform.position = new Vector3(j * tileSize, 0, i * tileSize);
                    tileMap[j, i] = newTile;

                    GameObject newCoverTile = Instantiate(coverTile, transform);
                    newCoverTile.transform.position = new Vector3(j * tileSize, tileSize / 2, i * tileSize);
                    coverTileMap[j, i] = newCoverTile;
                }
            }
        }

        PlaceResourceSet();
    }


    //Making one chunk of Resource Area using
    void MakeResourceSet(int x, int y, float value, Tile.Type type, int curNum)
    {
        if (curNum == 3)
        {
            for (int i = 0; i < 1 + (curNum * 2); i++)
            {
                for (int j = 0; j < 1 + (curNum * 2); j++)
                {
                    if (x + j >= 0 && x + j < MAP_SIZE && y + i >= 0 && y + i < MAP_SIZE)
                    {
                        tileMap[x + j, y + i].GetComponent<Tile>().UpdateTypeChange(type, value);
                    }
                }
            }
            return;
        }

        MakeResourceSet(x - 1, y - 1, value, type + 1, curNum + 1);

        for (int i = 0; i < 1 + (curNum * 2); i++)
        {
            for (int j = 0; j < 1 + (curNum * 2); j++)
            {
                if (x + j >= 0 && x + j < MAP_SIZE && y + i >= 0 && y + i < MAP_SIZE)
                {
                    tileMap[x + j, y + i].GetComponent<Tile>().UpdateTypeChange(type, value);
                }
            }
        }
    }

    void PlaceResourceSet()
    {
        Vector2Int[] tempVec2 = new Vector2Int[4];

        int x = Random.Range(0, 4);
        int y = Random.Range(0, 4);

        for (int i = 0; i < 4; i++)
        {
            tempVec2[i].x = x;
            tempVec2[i].y = y;

            int randVal = (int)Random.Range(2000, 5000) / 100 * 100;

            MakeResourceSet(x, y, randVal, Tile.Type.Maximum, 0);

            x += Random.Range(6, 8);
            y = Random.Range(0, 4);
        }

        for(int i = 0; i < 4; i++)
        {
            x = tempVec2[i].x;
            y = tempVec2[i].y;
            int originX = x;

            for (int j = 0; j < 3; j++)
            {
                if (j % 2 == 1)
                {
                    switch(Random.Range(0, 2))
                    {
                        case 0:
                            if (i < 2)
                                x += 1;
                            break;
                        case 1:
                            if (i >= 2)
                                x -= 1;
                            break;
                        case 2:
                            if (i == 0)
                                x += 1;
                            else if (i == 2)
                                x -= 1;
                            break;
                    }
                }
                else
                    x = originX;

                int randVal = (int)Random.Range(2000, 5000) / 100 * 100;

                MakeResourceSet(x, y, randVal, Tile.Type.Maximum, 0);
                y += Random.Range(6, 8);
            }
        }

    }
}
