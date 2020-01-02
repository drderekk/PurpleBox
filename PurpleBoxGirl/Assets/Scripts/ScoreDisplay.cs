using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{

    public TextMeshProUGUI Scoretext;
    public TextMeshProUGUI HiScoretext;
    private LevelManager LevelManager;
    private int HiScore = 0;
    private int Score;

    void Start()
    {
        Scoretext = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        HiScoretext = GameObject.Find("HiScore").GetComponent<TextMeshProUGUI>();
        LevelManager = FindObjectOfType<LevelManager>();
    }

    void Update()
    {
        Score = LevelManager.Score;

        if(Score > HiScore)
        {
            HiScore = Score;
        }

        HiScoretext.SetText("High Score: " + HiScore + "%");
        Scoretext.SetText("Progress: " + Score + "%");

    }
}
