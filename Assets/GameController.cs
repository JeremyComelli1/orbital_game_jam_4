using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{ 
    public int _longSeedNumber;
    public int _thiccSeedNumber;
    private TextMeshProUGUI _longSeed;
    private TextMeshProUGUI _thiccSeed;
    private Button _longSeedButton;
    private Button _thiccSeedButton;

    public GameObject LongSeedButton;
    public GameObject ThiccSeedButton;
    public GameObject longSeed;
    public GameObject thiccSeed;
    public int selectedSeed { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        _longSeedNumber = 5;
        _thiccSeedNumber = 20;
        _longSeedButton = LongSeedButton.GetComponent<Button>();
        _thiccSeedButton = ThiccSeedButton.GetComponent<Button>();
        _longSeed = longSeed.GetComponent<TextMeshProUGUI>();
        _thiccSeed = thiccSeed.GetComponent<TextMeshProUGUI>();
       
        _longSeedButton.onClick.AddListener(delegate { SelectSeed(0); });
        _thiccSeedButton.onClick.AddListener(delegate { SelectSeed(1); });

        selectedSeed = 1;

    }

    // Update is called once per frame
    void Update()
    {
        _longSeed.text = _longSeedNumber.ToString();
        _thiccSeed.text = _thiccSeedNumber.ToString();
    }
    public void SelectSeed(int seedType)
    {
        selectedSeed = seedType;
    }
}
