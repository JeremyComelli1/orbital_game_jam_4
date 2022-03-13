using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapController : MonoBehaviour
{   
    public Tilemap tileMap;
    public GameObject gameController; 
    public Tile highlightTile;
    public Tilemap highlightMap;
    public GameObject ArrowButton;
    public GameObject UI;

    private GameController _gameController;
    private Vector3Int previous;
    private GameObject arrowDown,arrowUp,arrowLeft,arrowRight;

    void Start()
    {
        _gameController = gameController.GetComponent<GameController>();

        
    }

    void Update()
    { 
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            Vector3Int gridPosition = tileMap.WorldToCell(mousePosition);



            
            Vector3 tileCenterPosition = tileMap.GetCellCenterWorld(gridPosition);




            TileBase clickedTile = tileMap.GetTile(gridPosition);

            // If it's a long seed
            if(_gameController.selectedSeed == 0)
                ShowDirectionnalArrows(tileCenterPosition);

            // if the position has changed
            if (gridPosition != previous)
            {
                // set the new tile
                highlightMap.SetTile(gridPosition, highlightTile);

                // erase previous
                highlightMap.SetTile(previous, null);

                // save the new position for next frame
                previous = gridPosition;
            }

        }
    }
    void ShowDirectionnalArrows(Vector3 mousePosition)
    {
        if (this.arrowRight)
        {
            Destroy(arrowRight);
            Destroy(arrowLeft);
            Destroy(arrowUp);
            Destroy(arrowDown);
        }




        
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, mousePosition);
        
        Vector2 anchoredPosition = transform.InverseTransformPoint(screenPoint);
        Vector2 offset;
        offset = new Vector2(-100,0);
        anchoredPosition = anchoredPosition + offset;
        arrowLeft = Instantiate(ArrowButton, anchoredPosition, Quaternion.identity);

        anchoredPosition = transform.InverseTransformPoint(screenPoint);
        offset = new Vector2(100, 0);
        anchoredPosition = anchoredPosition + offset;
        arrowRight = Instantiate(ArrowButton, anchoredPosition, Quaternion.identity);

        anchoredPosition = transform.InverseTransformPoint(screenPoint);
        offset = new Vector2(0, -100);
        anchoredPosition = anchoredPosition + offset;
        arrowDown = Instantiate(ArrowButton, anchoredPosition, Quaternion.identity);

        anchoredPosition = transform.InverseTransformPoint(screenPoint);
        offset = new Vector2(0, 100);
        anchoredPosition = anchoredPosition + offset;
        arrowUp = Instantiate(ArrowButton, anchoredPosition, Quaternion.identity);


        arrowLeft.GetComponent<RectTransform>().Rotate(0, 0, 90);
        arrowLeft.transform.parent = UI.transform;
        arrowRight.GetComponent<RectTransform>().Rotate(0, 0, 270);
        arrowRight.transform.parent = UI.transform;
        arrowDown.GetComponent<RectTransform>().Rotate(0, 0, 180);
        arrowDown.transform.parent = UI.transform;
        arrowUp.transform.parent = UI.transform;
    }
}
