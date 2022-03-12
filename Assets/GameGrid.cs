using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameGrid : MonoBehaviour
{

    public int width;
    public int height;

    public GameObject[,] tiles;
    public GameObject tilePrefab;

    public Tilemap tileMap;
    public enum Direction { left, up, right, down };

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate all game tiles to default value (ground) 
        for (int i = 0; i < width; i++)
        {
            for(int j = 0; j< height; j++)
            {
                tiles[i,j] = Instantiate(tilePrefab, tileMap.GetCellCenterWorld(new Vector3Int(i, j, 0)), Quaternion.identity);
            }
        }

        // Here go setup values for the specific level
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlantThiccSeed(Vector3Int gridPosition)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                Vector2Int currPos = new Vector2Int(i, j) + GridPositionToArrayIndex(gridPosition);
                tiles[currPos.x, currPos.y].GetComponent<TileScript>().SetState(TileScript.State.grass);
            }
        }
    }

    public void PlantLongSeed(Vector3Int gridPosition, Direction direction)
    {

    }

    public void NextTurn()
    {

    }

    public void MoveSheep()
    {

    }

    private Vector2Int GridPositionToArrayIndex(Vector3Int gridPosition)
    {
        return new Vector2Int(gridPosition.x - 10, gridPosition.y - 10);
    }

}
