using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapController : MonoBehaviour
{

    public Tile highlightTile;
    public Tilemap tileMap;
    public Tilemap highlightMap;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Vector3Int gridPosition 

        }
    }
}
