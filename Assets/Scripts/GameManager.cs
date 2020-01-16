using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    float clickDelay = 0.1f;
    float curClickDelay = 0.0f;
    int clickNum = 6;

    private void Update()
    {
        if(curClickDelay == 0 && clickNum > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickNum -= 1;
                curClickDelay += Time.deltaTime;
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    if (hit.transform.tag == "Cover")
                    {
                        DetectAllInRange(hit.transform);
                    }
                }
            }
        }
        else
        {
            curClickDelay += Time.deltaTime;
            if (curClickDelay > clickDelay)
                curClickDelay = 0.0f;
        }

    }

    void DetectAllInRange(Transform clickedTile)
    {
        Vector2 point = clickedTile.GetComponent<CoverTile>().point;

        for(int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (point.x + j >= 0 && point.x + j < MapGenerator.MAP_SIZE && point.y + i >= 0 && point.y + i < MapGenerator.MAP_SIZE)
                {
                    MapGenerator.instance.coverTileMap[(int)point.x + j, (int)point.y + i].SetActive(false);
                }
            }

        }
    }

}
