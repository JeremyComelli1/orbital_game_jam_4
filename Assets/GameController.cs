using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int _longSeedNumber;
    private int _thiccSeedNumber;
    private TextMeshProUGUI _longSeed;
    private TextMeshProUGUI _thiccSeed;
    public GameObject longSeed;
    public GameObject thiccSeed;
    // Start is called before the first frame update
    void Start()
    {
        _longSeedNumber = 0;
        _thiccSeedNumber = 0;
        _longSeed = longSeed.GetComponent<TextMeshProUGUI>();
        _thiccSeed = thiccSeed.GetComponent<TextMeshProUGUI>();
        _longSeed.text = _longSeedNumber.ToString();
        _thiccSeed.text = _thiccSeedNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
