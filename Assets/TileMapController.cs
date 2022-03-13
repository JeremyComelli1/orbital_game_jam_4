using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TileMapController : MonoBehaviour {
    public Tilemap tileMap;
    public GameObject gameController; 
    public Tile highlightTile;
    public Tilemap highlightMap;
    public GameObject ArrowButton;
    public GameObject UI;
    public GameObject gameGrid;

    private bool _directionSelected;
    private GameGrid.Direction _direction;
    private GameController _gameController;
    private Vector3Int previous;
    private GameObject arrowDown,arrowUp,arrowLeft,arrowRight;

    void Start()
    {
        _gameController = gameController.GetComponent<GameController>();
        _directionSelected = false;
        
    }

    void Update()
    {
        if (_gameController.selectedSeed == 1)
            RemoveDirectionalArrows();
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            Vector3Int gridPosition = tileMap.WorldToCell(mousePosition);



            
            Vector3 tileCenterPosition = tileMap.GetCellCenterWorld(gridPosition);




            TileBase clickedTile = tileMap.GetTile(gridPosition);

            // If it's a long seed
            

            // if the position has changed
            if (gridPosition != previous)
            {
                // set the new tile
                highlightMap.SetTile(gridPosition, highlightTile);

                // erase previous
                highlightMap.SetTile(previous, null);

                // save the new position for next frame
                previous = gridPosition;
            } else
            {
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

    

    void ShowDirectionnalArrows(Vector3 mousePosition)
    {
        if (this.arrowRight)
        {
            Destroy(arrowRight);
            Destroy(arrowLeft);
            Destroy(arrowUp);
            Destroy(arrowDown);
        }
        


        /*

        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, mousePosition);
        
        Vector2 anchoredPosition = transform.InverseTransformPoint(screenPoint);
        Vector2 offset;
        offset = new Vector2(-100,0);
        anchoredPosition = anchoredPosition + offset;
        arrowLeft = Instantiate(ArrowButton, anchoredPosition, Quaternion.identity);
        arrowLeft.GetComponent<Button>().onClick.AddListener(delegate { ChooseDirection(GameGrid.Direction.left); });

        anchoredPosition = transform.InverseTransformPoint(screenPoint);
        offset = new Vector2(100, 0);
        anchoredPosition = anchoredPosition + offset;
        arrowRight = Instantiate(ArrowButton, anchoredPosition, Quaternion.identity);
        arrowRight.GetComponent<Button>().onClick.AddListener(delegate { ChooseDirection(GameGrid.Direction.right); });

        anchoredPosition = transform.InverseTransformPoint(screenPoint);
        offset = new Vector2(0, -100);
        anchoredPosition = anchoredPosition + offset;
        arrowDown = Instantiate(ArrowButton, anchoredPosition, Quaternion.identity);
        arrowDown.GetComponent<Button>().onClick.AddListener(delegate { ChooseDirection(GameGrid.Direction.down); });

        anchoredPosition = transform.InverseTransformPoint(screenPoint);
        offset = new Vector2(0, 100);
        anchoredPosition = anchoredPosition + offset;
        arrowUp = Instantiate(ArrowButton, anchoredPosition, Quaternion.identity);
        arrowUp.GetComponent<Button>().onClick.AddListener(delegate { ChooseDirection(GameGrid.Direction.up); });

        arrowLeft.GetComponent<RectTransform>().Rotate(0, 0, 90);
        arrowLeft.transform.parent = UI.transform;
        arrowRight.GetComponent<RectTransform>().Rotate(0, 0, 270);
        arrowRight.transform.parent = UI.transform;
        arrowDown.GetComponent<RectTransform>().Rotate(0, 0, 180);
        arrowDown.transform.parent = UI.transform;
        arrowUp.transform.parent = UI.transform;
        */
    }

    void RemoveDirectionalArrows()
    {
        if (this.arrowRight)
        {
            Destroy(arrowRight);
            Destroy(arrowLeft);
            Destroy(arrowUp);
            Destroy(arrowDown);
        }
    }
    public void ChooseDirection(int direction)
    {
        _directionSelected = true;
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
}
