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
    public GameObject SheepPrefab;
    public List<GameObject> sheeps;



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

        tiles[2, 2].GetComponent<TileScript>().SetState(TileScript.State.grass);

        GameObject sheep = Instantiate(SheepPrefab);
        sheep.GetComponent<SheepScript>().grid = this.gameObject;
        sheep.GetComponent<SheepScript>().SetInitialPos(8, 8);
        sheeps.Add(sheep);

    }

    public void PlantThiccSeed(Vector3Int gridPosition)
    {
        if (tiles[GridPositionToArrayIndex(gridPosition).x, GridPositionToArrayIndex(gridPosition).y].GetComponent<TileScript>().GetState() == TileScript.State.grass)
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
            NextTurn();
       }
    }

    public void PlantLongSeed(Vector3Int gridPosition, Direction direction)
    {
        Vector2Int currPos = GridPositionToArrayIndex(gridPosition);
        if (tiles[currPos.x, currPos.y].GetComponent<TileScript>().GetState() == TileScript.State.grass)
        {
            tiles[currPos.x, currPos.y].GetComponent<TileScript>().SetState(TileScript.State.grass);

            int delta;
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
    }

    public void NextTurn()
    {
        if (tiles[10,11].GetComponent<TileScript>().GetState() == TileScript.State.grass || tiles[11, 10].GetComponent<TileScript>().GetState() == TileScript.State.grass)
        {
            Debug.Log("You Win!");
        }

        MoveSheep();

        if (!ValidateYourself())
        {
            Debug.Log("You lose!");
        }
    }

    private void MoveSheep()
    {
        foreach (GameObject sheep in sheeps)
        {
            sheep.GetComponent<SheepScript>().Move();
        }
    }

    public void SetTileState(int x, int y, TileScript.State state)
    {
        tiles[x, y].GetComponent<TileScript>().SetState(state); 
    }

    public TileScript.State GetTileState(int x, int y)
    {
        return tiles[x, y].GetComponent<TileScript>().GetState();
    }

    public bool ValidateYourself()
    {
        return ValidateGrid(this.tiles);
    }

    // HARD CODED GRID VALUES
    public bool ValidateGrid(GameObject[,] tiles)
    {

        int[,] clumps = new int[tiles.GetLength(0), tiles.GetLength(1)];
        int currentclump = 1;

        for(int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j].GetComponent<TileScript>().GetState() == TileScript.State.grass)
                {
                    

                    if (clumps[i, j] == 0)
                    {
                        clumps[i, j] = currentclump;
                        RecurDFS(tiles, clumps, i, j);
                        currentclump = currentclump + 1;
                    }
                }
            }
        }

        foreach(int clump in clumps)
        {
            if (clump > 1)
            {
                return false;
            }
        }
        return true;
    }

    // HARD CODED GRID VALUES
    public void RecurDFS(GameObject[,] tiles, int[,] clumps, int i, int j)
    {
        
        if (0 <= i-1 && tiles[i-1, j].GetComponent<TileScript>().GetState() == TileScript.State.grass)
        {
            if (clumps[i - 1, j] == 0)
            {
                clumps[i - 1, j] = clumps[i, j];
                RecurDFS(tiles, clumps, i - 1, j);
            }
        }

        if (0 <= j - 1 && tiles[i, j-1].GetComponent<TileScript>().GetState() == TileScript.State.grass)
        {
            if (clumps[i, j - 1] == 0)
            {
                clumps[i, j - 1] = clumps[i, j];
                RecurDFS(tiles, clumps, i, j - 1);
            }
        }

        if (11 >= i + 1 && tiles[i + 1, j].GetComponent<TileScript>().GetState() == TileScript.State.grass)
        {
            if (clumps[i + 1, j] == 0)
            {
                clumps[i + 1, j] = clumps[i, j];
                RecurDFS(tiles, clumps, i + 1, j);
            }
        }

        if (11 >= j + 1 && tiles[i, j+1].GetComponent<TileScript>().GetState() == TileScript.State.grass)
        {
            if (clumps[i, j + 1] == 0)
            {
                clumps[i, j + 1] = clumps[i, j];
                RecurDFS(tiles, clumps, i, j + 1);
            }
        } 
    }

    public Vector2Int GridPositionToArrayIndex(Vector3Int gridPosition)
    {
        return new Vector2Int(gridPosition.x + 6, gridPosition.y + 6);
    }

    public Vector3Int ArrayIndexToGridPosition(Vector2Int arrayPos)
    {
        return new Vector3Int(arrayPos.x - 6, arrayPos.y - 6, 0);
    }

    public Vector3 GetRealWorldPos(Vector2Int pos)
    {
        return tileMap.CellToWorld(ArrayIndexToGridPosition(pos));
    }
}

