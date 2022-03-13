using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    
    public enum State { water, dirt, grass, sheep, rock };
    public State currentState;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetState(State state)
    {
        this.currentState = state;
        // Here swap the underlying visual prefab and set the currentState for calculations
    }
}
