using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepScript : MonoBehaviour
{
    public Vector2Int currPos;
    public Vector2Int nextMove;
    public GameObject grid;
    private int[] visionRange = new int[] { -2, -1, 0, 1, 2 };
    private move lastMove = move.left;
    private enum move { left, top, right, down };

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInitialPos(int x, int y)
    {
        this.currPos = new Vector2Int(x, y);
        this.transform.position = grid.GetComponent<GameGrid>().GetRealWorldPos(currPos);
        ComputetNextMove();
    }

    public Vector2Int Move()
    {
        Debug.Log("CURR POS : " + currPos);
        Debug.Log("NEXT MOV : " + nextMove);
        // Internal
        this.transform.position = grid.GetComponent<GameGrid>().GetRealWorldPos(nextMove);
        currPos = nextMove;

        if (this.grid.GetComponent<GameGrid>().GetTileState(currPos.x, currPos.y) == TileScript.State.grass)
        {
            this.grid.GetComponent<GameGrid>().SetTileState(currPos.x, currPos.y, TileScript.State.dirt);
        }

        return ComputetNextMove();

    }

    public Vector2Int ComputetNextMove()
    {
        var tiles = CreateState();
        // Does sheep see grass?
        List<Vector2Int> seenGrass = new List<Vector2Int>();
        foreach (int i in visionRange)
        {
            foreach(int j in visionRange)
            {
                if(tiles[currPos.x + i, currPos.y + j] == TileScript.State.grass)
                {
                    seenGrass.Add(new Vector2Int(currPos.x + i, currPos.y + j));
                }
            }
        }
        if (seenGrass.Count != 0)
        {
            // Is in eating range?
            List<float> seenGrassDistance = new List<float>();
            foreach (Vector2 pos in seenGrass)
            {
                seenGrassDistance.Add(Vector2.Distance(currPos, pos));
            }
            var closestDistance = 10000.0f;
            foreach (float a in seenGrassDistance)
            {
                if (a < closestDistance)
                {
                    closestDistance = a;
                }
            }
            var closestDistanceIndex = seenGrassDistance.IndexOf(seenGrassDistance.Find(a => a == closestDistance));
            Debug.Log(closestDistance);
            if (closestDistance <= 1)
            {
                //Yes eat logic 
                // Remove tile check for split

                // LEFT FIRST
                var modifiedGridState = CopyState(tiles);
                var moveCountLeft = 20;
                bool isLeftGood = false;
                for (int i = currPos.x - 1; i >= 0; i--)
                {
                    if (modifiedGridState[i, currPos.y] != TileScript.State.grass)
                    {
                        break;
                    }
                    moveCountLeft = moveCountLeft + 1;
                    modifiedGridState[i, currPos.y] = TileScript.State.dirt;
                    if (!ValidateGrid(modifiedGridState))
                    {
                        // We have a split, this is a good move
                        isLeftGood = true;
                        break;
                    }
                }

                // TOP
                modifiedGridState = CopyState(tiles);
                var moveCountTop = 20;
                bool isTopGood = false;
                for (int i = currPos.y + 1; i < 12; i++)
                {
                    if (modifiedGridState[currPos.x, i] != TileScript.State.grass)
                    {
                        break;
                    }
                    moveCountTop = moveCountTop + 1;
                    modifiedGridState[currPos.x, i] = TileScript.State.dirt;
                    if (!ValidateGrid(modifiedGridState))
                    {
                        // We have a split, this is a good move
                        isTopGood = true;
                        break;
                    }
                }

                // RIGHT
                modifiedGridState = CopyState(tiles);
                var moveCountRight = 20;
                bool isRightGood = false;
                for (int i = currPos.x + 1; i < 12; i++)
                {
                    if (modifiedGridState[i, currPos.y] != TileScript.State.grass)
                    {
                        break;
                    }
                    moveCountRight = moveCountRight + 1;
                    modifiedGridState[i, currPos.y] = TileScript.State.dirt;
                    if (!ValidateGrid(modifiedGridState))
                    {
                        // We have a split, this is a good move
                        isRightGood = true;
                        break;
                    }
                }

                // DOWN
                modifiedGridState = CopyState(tiles);
                var moveCountDown = 20;
                bool isDownGood = false;
                for (int i = currPos.y - 1; i >= 0; i--)
                {
                    if (modifiedGridState[currPos.x, i] != TileScript.State.grass)
                    {
                        break;
                    }
                    moveCountDown = moveCountDown + 1;
                    modifiedGridState[currPos.x, i] = TileScript.State.dirt;
                    if (!ValidateGrid(modifiedGridState))
                    {
                        // We have a split, this is a good move
                        isDownGood = true;
                        break;
                    }
                }
                var topMove = move.left;
                var shortestMove = 100;
                if (isLeftGood)
                {
                    if (moveCountLeft < shortestMove)
                    {
                        topMove = move.left;
                        shortestMove = moveCountLeft;
                    }
                }
                if (isRightGood)
                {
                    if (moveCountRight < shortestMove)
                    {
                        topMove = move.right;
                        shortestMove = moveCountRight;
                    }
                }
                if (isTopGood)
                {
                    if (moveCountTop < shortestMove)
                    {
                        topMove = move.top;
                        shortestMove = moveCountTop;
                    }
                }
                if (isDownGood)
                {
                    if (moveCountDown < shortestMove)
                    {
                        topMove = move.down;
                        shortestMove = moveCountDown;
                    }
                }

                // NOW WE FUCKING MOVE
                switch (topMove)
                {
                    case move.left:
                        if (shortestMove > 19)
                        {
                            //No good move, pick last move
                            //this is retarded
                            switch (lastMove)
                            {
                                case move.left:
                                    this.nextMove = new Vector2Int(currPos.x - 1, currPos.y);
                                    break;
                                case move.top:
                                    this.nextMove = new Vector2Int(currPos.x, currPos.y + 1);
                                    break;
                                case move.right:
                                    this.nextMove = new Vector2Int(currPos.x + 1, currPos.y);
                                    break;
                                case move.down:
                                    this.nextMove = new Vector2Int(currPos.x, currPos.y - 1);
                                    break;
                            }
                            break;
                        }
                        else
                        {
                            this.nextMove = new Vector2Int(currPos.x - 1, currPos.y);
                            break;
                        }
                    case move.top:
                        this.nextMove = new Vector2Int(currPos.x, currPos.y + 1);
                        break;
                    case move.right:
                        this.nextMove = new Vector2Int(currPos.x + 1, currPos.y);
                        break;
                    case move.down:
                        this.nextMove = new Vector2Int(currPos.x, currPos.y - 1);
                        break;
                }

            }
            else {
                //No walk to closest
                Vector2Int closest = seenGrass[closestDistanceIndex];
                this.nextMove = new Vector2Int(currPos.x + Mathf.Clamp(-closest.x, -1, 1), currPos.y + Mathf.Clamp(-closest.y, -1, 1));
            }
        }
        else
        {
            this.nextMove = currPos;
        }
        Debug.Log("COMPUTEDNEXTMOVE: " + nextMove);
        
        return nextMove;
    }


    private TileScript.State[,] CreateState()
    {
        TileScript.State[,] gridState = new TileScript.State[12, 12];
        var tile = grid.GetComponent<GameGrid>().tiles;
        for(int i = 0; i< 12; i++)
        {
            for (int j= 0; j < 12; j++)
            {
                gridState[i, j] = tile[i,j].GetComponent<TileScript>().GetState();
            }
        }
        return gridState;
    }
    private TileScript.State[,] CopyState(TileScript.State[,] oldstate)
    {
        TileScript.State[,] newState = new TileScript.State[12, 12];
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                newState[i, j] = oldstate[i, j];
            }
        }
        return newState;
    }

    public bool ValidateGrid(TileScript.State[,] tiles)
    {

        int[,] clumps = new int[tiles.GetLength(0), tiles.GetLength(1)];
        int currentclump = 1;

        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j] == TileScript.State.grass)
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

        foreach (int clump in clumps)
        {
            if (clump > 1)
            {
                return false;
            }
        }
        return true;
    }

    // HARD CODED GRID VALUES
    public void RecurDFS(TileScript.State[,] tiles, int[,] clumps, int i, int j)
    {

        if (0 <= i - 1 && tiles[i - 1, j] == TileScript.State.grass)
        {
            if (clumps[i - 1, j] == 0)
            {
                clumps[i - 1, j] = clumps[i, j];
                RecurDFS(tiles, clumps, i - 1, j);
            }
        }

        if (0 <= j - 1 && tiles[i, j - 1] == TileScript.State.grass)
        {
            if (clumps[i, j - 1] == 0)
            {
                clumps[i, j - 1] = clumps[i, j];
                RecurDFS(tiles, clumps, i, j - 1);
            }
        }

        if (11 >= i + 1 && tiles[i + 1, j] == TileScript.State.grass)
        {
            if (clumps[i + 1, j] == 0)
            {
                clumps[i + 1, j] = clumps[i, j];
                RecurDFS(tiles, clumps, i + 1, j);
            }
        }

        if (11 >= j + 1 && tiles[i, j + 1] == TileScript.State.grass)
        {
            if (clumps[i, j + 1] == 0)
            {
                clumps[i, j + 1] = clumps[i, j];
                RecurDFS(tiles, clumps, i, j + 1);
            }
        }
    }
}
