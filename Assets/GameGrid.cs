using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameGrid : MonoBehaviour
{

    public int width;
    public int height;

    public GameObject[,] tiles;
    public List <GameObject> GrassTilesPrefabs;
    public GameObject DirtTilePrefab;
    public GameObject WaterTilePrefab;
    public GameObject ConcreteTilePrefab;



    public Tilemap tileMap;
    public enum Direction { left, up, right, down };

    // Start is called before the first frame update
    void Start()
    {
        tiles = new GameObject[width, height];
        //Instantiate all game tiles to default value (ground) 
        for (int i = 0; i < width; i++)
        {
            for(int j = 0; j< height; j++)
            {
                // Instantiate the Tile GameObject with an offset to prevent clipping with the underlying tilemap 
                GameObject CurrentTile = Instantiate(DirtTilePrefab, tileMap.GetCellCenterWorld(ArrayIndexToGridPosition(new Vector2Int(i, j))) + new Vector3(0, 0, -1), Quaternion.identity);

                // Attach TileScript
                CurrentTile.AddComponent<TileScript>();

                // Set default tile (dirt)
                CurrentTile.GetComponent<TileScript>().SetState(TileScript.State.dirt);

                tiles[i, j] = CurrentTile;
            }
        }
        
        // hehe
        if (Random.Range(0, 100) > 99) { for (int i = 0; i < 100; i++) Debug.Log("<color=red>PENISPENISPENISPENIS</color>"); }

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
                if (0 <=currPos.x  && currPos.x < 12 && 0<= currPos.y && currPos.y < 12)
                {
                    tiles[currPos.x, currPos.y].GetComponent<TileScript>().SetState(TileScript.State.grass);
                }
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
        return new Vector2Int(gridPosition.x + 6, gridPosition.y + 6);
    }

    private Vector3Int ArrayIndexToGridPosition(Vector2Int arrayPos)
    {
        return new Vector3Int(arrayPos.x - 6, arrayPos.y - 6, 0);
    }

    // Prolly shouldn't be a function, remove if necessary
    private GameObject GetRandomGrassTile()
    {
        return GrassTilesPrefabs[Random.Range(0, GrassTilesPrefabs.Count)];
    }
}

