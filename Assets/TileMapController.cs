using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapController : MonoBehaviour
{   
    public Tilemap tileMap;

    public Tile highlightTile;
    public Tilemap highlightMap;

    private Vector3Int previous;

    public GameObject gameGrid;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = tileMap.WorldToCell(mousePosition);

            TileBase clickedTile = tileMap.GetTile(gridPosition);

            Debug.Log(gridPosition);

            // if the position has changed
            if (gridPosition != previous)
            {
                // set the new tile
                highlightMap.SetTile(gridPosition, highlightTile);

                // erase previous
                highlightMap.SetTile(previous, null);

                // save the new position for next frame
                previous = gridPosition;

                //gameGrid.GetComponent<GameGrid>().PlantThiccSeed(gridPosition);
                gameGrid.GetComponent<GameGrid>().PlantLongSeed(gridPosition, GameGrid.Direction.left);

            }

        }
    }
}
