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
                Instantiate(tile[Random.Range(0, 16)], new Vector3(i, j), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
