using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnMenu : MonoBehaviour
{

    public LevelManager LevelManager;

    public void Start()
    {
        LevelManager = FindObjectOfType<LevelManager>();
    }

    public void Respawn()
    {
        LevelManager.RespawnPlayer();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
