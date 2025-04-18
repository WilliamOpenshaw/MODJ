using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tableManager : MonoBehaviour
{
    public List<GameObject> goList;
    public GameObject table;
    public GameObject cell;
    public RectTransform test;
    public GameObject prefab;
    public int tableWidth;
    public int tableHeight;

    public GameObject content;

    public int cellNumber = 0;
    public string tableName = "";

    public float cellWidth = 200f;
    public float contentWidth;
    public float cellHeight;
    public float contentHeight;
    public float cellX = 0f;
    public float cellY = 0f;

    public float currentXPos = 0f;
    public float currentYPos = 0f;

    public float testXPos = 0f;
    public float testYPos = 0f;

    public float currentContentWidth = 0f;
    public float currentContentHeight = 0f;

    public float leftOffset = 0f;

    public RectTransform cellRect;
    public RectTransform currentCellRect;

    public RectTransform contentRect;

    public GameObject addRowButton;
    public GameObject addColumnButton;

    public GameObject deleteColumnButton;
    public GameObject deleteRowButton;

    public float originalCellWidth = 0f;
    public float originalAddColumnButtonYPos = 0f;

    public float originalAddRowButtonYPos = 0f;
    public float originalDeleteRowButtonYPos = 0f;

    public RectTransform currentRect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // set width to 0
        
        tableHeight = 1;
        tableWidth = 1;
        cellNumber = 1;
        goList = new List<GameObject>();
        goList.Add(cell);
        cellRect = cell.GetComponent<RectTransform>();
        contentRect = content.GetComponent<RectTransform>();
        //cellRect.sizeDelta = new Vector2(cellWidth, cellHeight);
        cellRect.anchoredPosition = new Vector2(cellX, 0);
        contentWidth = contentRect.rect.width * (0.85f);
        contentHeight = contentRect.rect.height;
        cellWidth = cellRect.rect.width;
        cellHeight = cellRect.rect.height;
        originalCellWidth = cellWidth;
        originalAddColumnButtonYPos = addColumnButton.GetComponent<RectTransform>().anchoredPosition.y;
        originalAddRowButtonYPos = addRowButton.GetComponent<RectTransform>().anchoredPosition.y;
        originalDeleteRowButtonYPos = deleteRowButton.GetComponent<RectTransform>().anchoredPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }

    public void AddTable()
    {
        //Instantiate(prefab, new Vector2(0, 0), Quaternion.identity);
    }

    public void RemoveTable()
    {

    }

    public void AddRightColumn()
    {
        if (tableWidth < 4)
        {
            //contentWidth = contentRect.rect.width * (0.75f);
            //contentHeight = contentRect.rect.height;
            tableWidth += 1;
            cellWidth = contentWidth / tableWidth;
            cellX = contentWidth / tableWidth;
            //+ (tableWidth * (-25));

            // EXISTING CELLS
            for (int i = 0; i < goList.Count; i++)
            {
                currentCellRect = goList[i].GetComponent<RectTransform>();
                if (currentCellRect.anchoredPosition.x <= 10)
                {
                    currentCellRect.sizeDelta = new Vector2(cellWidth, cellHeight);
                    currentCellRect.anchoredPosition = new Vector2(0, 0);

                }
                else if (currentCellRect.anchoredPosition.x <= (contentWidth / (tableWidth - 1)))
                {
                    currentCellRect.sizeDelta = new Vector2(cellWidth, cellHeight);
                    currentCellRect.anchoredPosition = new Vector2(cellX, 0);

                }
                else if (currentCellRect.anchoredPosition.x <= ((contentWidth / (tableWidth - 1)) * 2))
                {
                    currentCellRect.sizeDelta = new Vector2(cellWidth, cellHeight);
                    currentCellRect.anchoredPosition = new Vector2(cellX * 2, 0);

                }
                else if (currentCellRect.anchoredPosition.x <= ((contentWidth / (tableWidth - 1)) * 3))
                {
                    currentCellRect.sizeDelta = new Vector2(cellWidth, cellHeight);
                    currentCellRect.anchoredPosition = new Vector2(cellX * 3, 0);

                }
                else
                {
                    currentCellRect.sizeDelta = new Vector2(cellWidth, cellHeight);
                    currentCellRect.anchoredPosition = new Vector2(0, 0);
                    Debug.Log("///////////////////////////////////////////");
                    Debug.Log("ELSE");
                    Debug.Log("currentCellRect.anchoredPosition.x : " + currentCellRect.anchoredPosition.x);
                    Debug.Log("cellX : " + cellX);
                    Debug.Log("cellWidth : " + cellWidth);
                    Debug.Log("contentWidth : " + contentWidth);
                }
            }

            // NEW CELLS
            for (int i = 0; i < tableHeight; i++)
            {
                //cellNumber += 1;
                goList.Add(Instantiate(prefab, new Vector2(0, 0), Quaternion.identity));
                //goList[cellNumber] = Instantiate(prefab, new Vector2(0, 0), Quaternion.identity);
                goList[cellNumber].transform.SetParent(content.transform, false);
                goList[cellNumber].GetComponent<RectTransform>().sizeDelta = new Vector2(cellWidth, cellHeight);
                goList[cellNumber].GetComponent<RectTransform>().anchoredPosition = new Vector2(cellX * (tableWidth - 1), 0 + (i * cellHeight));
                cellNumber += 1;

            }

            // MOVE TABLE CELLS LEFT
            if (tableWidth > 2)
            {
                leftOffset = (contentWidth / 25) * (tableWidth - 1);
                for (int i = 0; i < goList.Count; i++)
                {
                    currentCellRect = goList[i].GetComponent<RectTransform>();
                    currentCellRect.anchoredPosition = new Vector2(currentCellRect.anchoredPosition.x - leftOffset, currentCellRect.anchoredPosition.y);
                }
            }
            if (addColumnButton.GetComponent<RectTransform>().anchoredPosition.y == originalAddColumnButtonYPos)
            {
                addColumnButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(addColumnButton.GetComponent<RectTransform>().anchoredPosition.x, originalAddColumnButtonYPos - (cellHeight / 4));
            }
            // move right button to new position
            addColumnButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(contentRect.rect.width * (0.95f), addColumnButton.GetComponent<RectTransform>().anchoredPosition.y);
            addColumnButton.GetComponent<RectTransform>().sizeDelta = new Vector2(contentRect.rect.width * (0.075f), contentRect.rect.width * (0.075f));
            // enable delete column button to new position
            deleteColumnButton.SetActive(true);


        }
    }

    public void DeleteRightColumn()
    {
        if (tableWidth > 1)
        {
            tableWidth -= 1;
            cellWidth = contentWidth / tableWidth;
            cellX = contentWidth / tableWidth;

            // EXISTING CELLS
            for (int i = 0; i < goList.Count; i++)
            {
                currentCellRect = goList[i].GetComponent<RectTransform>();
                if (currentCellRect.anchoredPosition.x <= 10)
                {
                    currentCellRect.sizeDelta = new Vector2(cellWidth, cellHeight);
                    currentCellRect.anchoredPosition = new Vector2(0, 0);

                }
                else if (currentCellRect.anchoredPosition.x <= (contentWidth / (tableWidth + 1)))
                {
                    currentCellRect.sizeDelta = new Vector2(cellWidth, cellHeight);
                    currentCellRect.anchoredPosition = new Vector2(cellX, 0);

                    if (tableWidth < 2)
                    {
                        // destroy gameobject
                        Destroy(goList[i]);
                        goList.RemoveAt(i);
                        cellNumber -= 1;
                        Debug.Log("cellNumber : " + cellNumber);
                    }

                }
                else if (currentCellRect.anchoredPosition.x <= ((contentWidth / (tableWidth + 1)) * 2))
                {
                    currentCellRect.sizeDelta = new Vector2(cellWidth, cellHeight);
                    currentCellRect.anchoredPosition = new Vector2(cellX * 2, 0);

                    if (tableWidth < 3)
                    {
                        // destroy gameobject
                        Destroy(goList[i]);
                        goList.RemoveAt(i);
                        cellNumber -= 1;
                        Debug.Log("cellNumber : " + cellNumber);
                    }

                }
                else if (currentCellRect.anchoredPosition.x <= ((contentWidth / (tableWidth + 1)) * 3))
                {
                    currentCellRect.sizeDelta = new Vector2(cellWidth, cellHeight);
                    currentCellRect.anchoredPosition = new Vector2(cellX * 3, 0);

                    if (tableWidth < 4)
                    {
                        // destroy gameobject
                        Destroy(goList[i]);
                        goList.RemoveAt(i);
                        cellNumber -= 1;
                        Debug.Log("cellNumber : " + cellNumber);
                    }

                }
                /*
                else
                {
                    currentCellRect.sizeDelta = new Vector2(cellWidth, cellHeight);
                    currentCellRect.anchoredPosition = new Vector2(0, 0);
                    Debug.Log("///////////////////////////////////////////");
                    Debug.Log("ELSE");
                    Debug.Log("currentCellRect.anchoredPosition.x : " + currentCellRect.anchoredPosition.x);
                    Debug.Log("cellX : " + cellX);
                    Debug.Log("cellWidth : " + cellWidth);
                    Debug.Log("contentWidth : " + contentWidth);
                } 
                */
            }

            // put right button back in original place
            if (tableWidth == 1)
            {
                // move right button back to original position
                addColumnButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(588, -115f);
                addColumnButton.GetComponent<RectTransform>().sizeDelta = new Vector2(contentRect.rect.width * (0.1f), contentRect.rect.width * (0.1f));
                // deleteColumnButton.SetActive(false);
                deleteColumnButton.SetActive(false);

                //set all cells in golist back to original cell width
                for (int i = 0; i < goList.Count; i++)
                {
                    currentCellRect = goList[i].GetComponent<RectTransform>();
                    currentCellRect.sizeDelta = new Vector2(originalCellWidth, cellHeight);
                    currentCellRect.anchoredPosition = new Vector2(0, 0);
                }
            }
        }


    }

    public void AddBottomRow()
    {

        tableHeight += 1;
        //cellHeight = contentHeight / tableHeight;
        //cellY = contentHeight / tableHeight;

        // NEW CELLS
        for (int i = 0; i < tableWidth; i++)
        {
            //cellNumber += 1;
            goList.Add(Instantiate(prefab, new Vector2(0, 0), Quaternion.identity));
            //goList[cellNumber] = Instantiate(prefab, new Vector2(0, 0), Quaternion.identity);
            goList[cellNumber].transform.SetParent(content.transform, false);
            goList[cellNumber].GetComponent<RectTransform>().sizeDelta = new Vector2(cellWidth, cellHeight);
            goList[cellNumber].GetComponent<RectTransform>().anchoredPosition = new Vector2(cellX * i, 0 - (cellHeight * (tableHeight - 1)));
            cellNumber += 1;
        }

        if (tableHeight > 1)
        {
            currentRect = addRowButton.GetComponent<RectTransform>();
            // move addRowButton back to original position
            currentRect.anchoredPosition = new Vector2(currentRect.anchoredPosition.x, currentRect.anchoredPosition.y - cellHeight);
            // deleteColumnButton.SetActive(true);
            deleteRowButton.SetActive(true);
            currentRect = deleteRowButton.GetComponent<RectTransform>();
            currentRect.anchoredPosition = new Vector2(currentRect.anchoredPosition.x, currentRect.anchoredPosition.y - cellHeight);
        }

    }

    public void DeleteBottomRow()
    {
        if (tableHeight > 1)
        {
            tableHeight -= 1;
            cellHeight = contentHeight / tableHeight;
            cellY = contentHeight / tableHeight;

            // EXISTING CELLS
            for (int i = 0; i < goList.Count; i++)
            {
                currentCellRect = goList[i].GetComponent<RectTransform>();
                currentCellRect.sizeDelta = new Vector2(cellWidth, cellHeight);
                currentCellRect.anchoredPosition = new Vector2(currentCellRect.anchoredPosition.x, cellY * (tableHeight - 1));
            }

            // DELETE CELLS
            for (int i = 0; i < tableWidth; i++)
            {
                if (tableHeight < 2)
                {
                    // destroy gameobject
                    Destroy(goList[i]);
                    goList.RemoveAt(i);
                    cellNumber -= 1;
                    Debug.Log("cellNumber : " + cellNumber);
                }

            }
            if (tableHeight == 1)
            {
                // move addRowButton back to original position
                addRowButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(588, -115f);
                addRowButton.GetComponent<RectTransform>().sizeDelta = new Vector2(contentRect.rect.width * (0.1f), contentRect.rect.width * (0.1f));
                // delete row button set inactive
                deleteRowButton.SetActive(false);

                //set all cells in golist back to original cell width
                for (int i = 0; i < goList.Count; i++)
                {
                    currentCellRect = goList[i].GetComponent<RectTransform>();
                    currentCellRect.sizeDelta = new Vector2(originalCellWidth, cellHeight);
                    currentCellRect.anchoredPosition = new Vector2(0, 0);
                }
            }
        }
    }
}
