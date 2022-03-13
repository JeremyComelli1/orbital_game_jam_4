using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    // TODO: add seed state if necessary
    public enum State { error, water, dirt, grass, sheep, rock };
    public State currentState;

    // The GameObject that's displayed or something
    public GameObject TileObject;

    // Reference to gameGrid because lasagna > spaghett
    public GameGrid gameGrid;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetState(State state)
    {
         
        if(this.currentState != state && this.currentState != State.error)
        {
            SwapCurrentTile(state);
        }

        this.currentState = state;


        // Here swap the underlying visual prefab and set the currentState for calculations
    }

    // Instantiates a new gameobject to replace the current tile
    private void SwapCurrentTile(State newState)
    {
        int X = (int)TileObject.transform.position.x;
        int Y = (int)TileObject.transform.position.y;


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
