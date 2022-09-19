using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHighscore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _highScore, _totalGems;
    // Start is called before the first frame update
    void Start()
    {
        _highScore.SetText(PlayerPrefs.GetInt("HighScore").ToString());
        _totalGems.SetText(PlayerPrefs.GetInt("TotalGems").ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
