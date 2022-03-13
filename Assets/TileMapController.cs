using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapController : MonoBehaviour {
    public Tilemap tileMap;

    public Tile highlightTile;
    public Tilemap highlightMap;

    public GameController _gameController;
    public GameObject gameController;
    public GameObject arrowButton;
    public bool _directionSelected = false;
    public GameGrid.Direction _direction;

    private Vector3Int previous;

    public GameObject gameGrid;

    private void Start()
    {
        _gameController = gameController.GetComponent<GameController>();  
    }

    public void ChooseDirection(int direction)
    {
        _directionSelected = true;
        Debug.Log("CHOOSE Direction");
        switch (direction)
        {
            case 0:
                _direction = GameGrid.Direction.up;
                break;
            case 1:
                _direction = GameGrid.Direction.left;
                break;
            case 2:
                _direction = GameGrid.Direction.down;
                break;
            case 3:
                _direction = GameGrid.Direction.right;
                break;


        }

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
                //gameGrid.GetComponent<GameGrid>().PlantLongSeed(gridPosition, GameGrid.Direction.left);
                //gameGrid.GetComponent<GameGrid>().PlantThiccSeed(gridPosition);

            }
            else
            {
                Debug.Log("CLOCK");
                if (_gameController.selectedSeed == 0 && _directionSelected && _gameController._longSeedNumber >= 1)
                {
                    gameGrid.GetComponent<GameGrid>().PlantLongSeed(gridPosition, _direction);
                    _gameController._longSeedNumber -= 1;
                    _directionSelected = false;
                }
                if (_gameController.selectedSeed == 1 && _gameController._thiccSeedNumber >= 1)
                {
                    _gameController._thiccSeedNumber -= 1;
                    gameGrid.GetComponent<GameGrid>().PlantThiccSeed(gridPosition);
                }
            }
        }
    }
}
