using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class create_grid : MonoBehaviour
{
    // Start is called before the first frame update

    public int grid_x;
    public int grid_y;

    public List<GameObject> tile;

    void Start()
    {
        for(int i = 0; i < grid_x; i++)
        {
            for(int j = 0; j < grid_y; j++)
            {
                GameObject game_object = Instantiate(tile[Random.Range(0, 16)], new Vector3(i, j), Quaternion.identity);

                // Iterates over every tile looking for an Animator, then disables it because fuck him
                foreach(Transform child in game_object.transform)
                {
                    if (child.GetComponent<Animator>() != null)
                    {
                        //child.GetComponent<Animator>().enabled = false;
                        //StartCoroutine("DelayedStart", (child, j));
                    }
                }
            }
        }


    }


    /// TODO: make a delayed start with a motherfucking coroutine, but rn IDGAF
    /*IEnumerator DelayedStart(object lol)
    {
        float delay_in_seconds = Mathf.Cos(x_coord);
        Debug.Log(delay_in_seconds);
        for (int i = 0; i < delay_in_seconds / 10; i++)
        {
            yield return new WaitForSeconds(.1f);
        }

        gameobject.GetComponent<Animator>().enabled = true;
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
