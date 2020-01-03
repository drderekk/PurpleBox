using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public static string HISCORE_KEY = "hiscore";

    public TextMeshProUGUI Scoretext;
    public TextMeshProUGUI HiScoretext;
    private LevelManager LevelManager;
    private int HiScore = 0;
    private int Score;

    void Start()
    {
        // Loads current high score from local storage (Set to 1 if none set)
        HiScore = PlayerPrefs.GetInt(HISCORE_KEY, 1);

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

            // Updates High score in local storage
            PlayerPrefs.SetInt(HISCORE_KEY, HiScore);
        }

        HiScoretext.SetText("High Score: " + HiScore + "%");
        Scoretext.SetText("Progress: " + Score + "%");

    }
}
