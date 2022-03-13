using UnityEngine;

public class TileScript : MonoBehaviour {
    // TODO: add seed state if necessary
    public enum State { error, water, victory_water, dirt, grass, sheep, rock , seed};
    public State currentState;

    // The GameObject that's displayed or something
    public GameObject TileObject;

    // Reference to gameGrid because lasagna > spaghett
    public GameGrid gameGrid;

    public void SetState(State NewState)
    {
        bool ChangeDenied = false;
        // Is this what they call a state machine?
        if (this.currentState != NewState && this.currentState != State.error)
        {
            // Cases where a state change is possible
            if (this.currentState == State.dirt && NewState == State.grass)
            {
                SwapCurrentTile(NewState);
            }
            else if (this.currentState == State.grass && NewState == State.dirt)
            {
                // Mouton goes here I guess
                this.SwapCurrentTile(State.dirt);
            }
            else if(this.currentState == State.seed)
            {
                // Seed pickup goes here
            }
            else if (this.currentState == State.victory_water)
            {
                // Trigger victory here
                ChangeDenied = true;
            }
            else if(this.currentState == State.water)
            {
                // Prevent water from being overwritten
                ChangeDenied = true;
            }
            else ChangeDenied = true;
        }

        if (!ChangeDenied) this.currentState = NewState;


        // Here swap the underlying visual prefab and set the currentState for calculations
    }

    public State GetState()
    {
        return this.currentState;
    }

    // Instantiates a new gameobject to replace the current tile
    private void SwapCurrentTile(State newState)
    {
        switch (newState)
        {
            case State.dirt:
                this.SetObject(Instantiate(gameGrid.DirtTilePrefab, TileObject.transform.position, Quaternion.identity, transform));
                break;

            case State.grass:
                this.SetObject(Instantiate(gameGrid.GrassTilesPrefabs[Random.Range(0, gameGrid.GrassTilesPrefabs.Count)], TileObject.transform.position, Quaternion.identity, transform));
                break;

            default:
                Debug.Log("Error, tile not implemented yet");
                break;

                // TODO: ADD ALL TILES HERE
        }
    }
    public void SetObject(GameObject NewObject)
    {
        Destroy(this.TileObject);
        this.TileObject = NewObject;
    }

    // Reference to the GameGrid
    public void SetGameGrid(GameGrid gameGrid)
    {
        this.gameGrid = gameGrid;
    }
}
