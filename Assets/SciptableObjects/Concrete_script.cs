using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Concrete_script : MonoBehaviour
{
    public List<Sprite> TileSprites;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = TileSprites[Random.Range(0, TileSprites.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
