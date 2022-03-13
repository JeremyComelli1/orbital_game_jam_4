using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameGrid : MonoBehaviour {

    public int width;
    public int height;

    public GameObject[,] tiles;
    public List<GameObject> GrassTilesPrefabs;
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
            for (int j = 0; j < height; j++)
            {

                // Create empty gameObject since we need to dynamically swap the displayed gameObject; the tile is therefore a child set thorugh tileScript.SetObject(Gameobject object)
                GameObject CurrentTile = new GameObject("Tile");
                CurrentTile.AddComponent<TileScript>();
                CurrentTile.GetComponent<TileScript>().gameGrid = this;

                // Instantiate the Tile GameObject with an offset to prevent clipping with the underlying tilemap 
                CurrentTile.GetComponent<TileScript>().SetObject(Instantiate(DirtTilePrefab, tileMap.GetCellCenterWorld(ArrayIndexToGridPosition(new Vector2Int(i, j))) + new Vector3(0, 0, -1), Quaternion.identity, CurrentTile.transform));

                // Set default tile (dirt)
                // DO NOT CALL BEFORE SETTING A TILE BECAUSE THEN MY SHITTY CODE BREAKS
                CurrentTile.GetComponent<TileScript>().SetState(TileScript.State.dirt);



                tiles[i, j] = CurrentTile;
            }
        }

        // hehe
        if (Random.Range(0, 100) > 99) { for (int i = 0; i < 100; i++) Debug.Log("<color=red>PENISPENISPENISPENIS</color>"); }

        // Here go setup values for the specific level

    }

    public void PlantThiccSeed(Vector3Int gridPosition)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                Vector2Int currPos = new Vector2Int(i, j) + GridPositionToArrayIndex(gridPosition);
                if (0 <= currPos.x && currPos.x < 12 && 0 <= currPos.y && currPos.y < 12)
                {
                    tiles[currPos.x, currPos.y].GetComponent<TileScript>().SetState(TileScript.State.grass);
                }
            }
        }
    }

    public void PlantLongSeed(Vector3Int gridPosition, Direction direction)
    {
        int delta;
        Vector2Int currPos = GridPositionToArrayIndex(gridPosition);
        tiles[currPos.x, currPos.y].GetComponent<TileScript>().SetState(TileScript.State.grass);

        if (direction == Direction.up || direction == Direction.right) delta = 1;
        else delta = -1;

        for (int i = 0; i < 4; i++)
        {
            // Delta is applied horizontally or vertically
            if (direction == Direction.up || direction == Direction.down) currPos += new Vector2Int(0, delta);
            else currPos += new Vector2Int(delta, 0);

            tiles[currPos.x, currPos.y].GetComponent<TileScript>().SetState(TileScript.State.grass);
        }
    }

    public void NextTurn()
    {

    }

    public void MoveSheep()
    {

    }

    public Vector2Int GridPositionToArrayIndex(Vector3Int gridPosition)
    {
        return new Vector2Int(gridPosition.x + 6, gridPosition.y + 6);
    }

    public Vector3Int ArrayIndexToGridPosition(Vector2Int arrayPos)
    {
        return new Vector3Int(arrayPos.x - 6, arrayPos.y - 6, 0);
    }
}

