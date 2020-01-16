using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text modeButtonText;
    [SerializeField] Text remaingNumberText;
    [SerializeField] Text resoucreText;
    [SerializeField] Text tileValueInfoText;
    [SerializeField] Text finalText;
    float resource = 0;
    float clickDelay = 0.1f;
    float curClickDelay = 0.0f;
    int scanClickNum = 6;
    int extractClickNum = 3;
    bool isGameEnd = false;

    enum Mode { ScanMode, ExtractMode};
    Mode curMode = Mode.ScanMode;

    private void Start()
    {
        remaingNumberText.text = "Remaing Scan Number: " + scanClickNum;
        finalText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isGameEnd) return;

        if (curClickDelay == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    if (curMode == Mode.ScanMode && scanClickNum > 0 && hit.transform.tag == "Cover")
                    {
                        curClickDelay += Time.deltaTime;
                        scanClickNum -= 1;
                        remaingNumberText.text = "Remaing Scan Number: " + scanClickNum;
                        DetectAllInRange(hit.transform);
                    }

                    if (curMode == Mode.ExtractMode && extractClickNum > 0 && (hit.transform.tag == "Tile" ))
                    {
                        curClickDelay += Time.deltaTime;
                        extractClickNum -= 1;
                        remaingNumberText.text = "Remaing Extract Number: " + extractClickNum;
                        ExtactTile(hit.transform);
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

        if(extractClickNum == 0)
        {
            finalText.gameObject.SetActive(true);
            finalText.text = "Final Resource : " + resource;
            isGameEnd = true;
        }

        GetTileInfo();
    }

    void GetTileInfo()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.transform.tag == "Tile")
            {
                tileValueInfoText.text = "Current Orge Value: " + (int)hit.transform.GetComponent<Tile>().value;
            }
            else
            {
                tileValueInfoText.text = "Hover on Orge";
            }
        }
    }

    void DetectAllInRange(Transform clickedTile)
    {
        Vector2Int point = new Vector2Int((int)clickedTile.GetComponent<CoverTile>().point.x, (int)clickedTile.GetComponent<CoverTile>().point.y);

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (point.x + j >= 0 && point.x + j < MapGenerator.MAP_SIZE && point.y + i >= 0 && point.y + i < MapGenerator.MAP_SIZE)
                {
                    MapGenerator.instance.coverTileMap[point.x + j, point.y + i].GetComponent<CoverTile>().Clicked();
                }
            }
        }
    }

    void ExtactTile(Transform clickedTile)
    {
        Vector2Int point = new Vector2Int((int)clickedTile.GetComponent<Tile>().point.x, (int)clickedTile.GetComponent<Tile>().point.y);

        for (int i = -2; i <= 2; i++)
        {
            for (int j = -2; j <= 2; j++)  
            {
                if (point.x + j >= 0 && point.x + j < MapGenerator.MAP_SIZE && point.y + i >= 0 && point.y + i < MapGenerator.MAP_SIZE)
                {
                    resource += MapGenerator.instance.tileMap[point.x + j, point.y + i].GetComponent<Tile>().CollectAround();
                    MapGenerator.instance.coverTileMap[point.x + j, point.y + i].GetComponent<CoverTile>().Clicked();
                }
            }
        }

        MapGenerator.instance.tileMap[point.x, point.y].GetComponent<Tile>().Collect();
        resoucreText.text = "Resource: " + resource;
    }

    public void ModeButtonClicked()
    {
        if(curMode == Mode.ScanMode)
        {
            curMode = Mode.ExtractMode;
            modeButtonText.text = "Extract Mode";
            remaingNumberText.text = "Remaing Extract Number: " + extractClickNum;
        }
        else
        {
            curMode = Mode.ScanMode;
            modeButtonText.text = "Scan Mode";
            remaingNumberText.text = "Remaing Scan Number: " + scanClickNum;
        }
    }

}
