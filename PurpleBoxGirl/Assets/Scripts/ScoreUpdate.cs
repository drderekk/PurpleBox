using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUpdate : MonoBehaviour {

    private TextMeshProUGUI Scoretext;
    private LevelManager LevelManager;
    private string Score;

    void Start()
    {
        Scoretext = GetComponent<TextMeshProUGUI>();
        LevelManager = FindObjectOfType<LevelManager>();
    }

	void Update () {
        Score = LevelManager.Score.ToString();
        Scoretext.SetText(Score + "%");
		
	}
}
