using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnMenu : MonoBehaviour
{

    public LevelManager LevelManager;
    private AudioController Audio;

    public void Start()
    {
        Audio = FindObjectOfType<AudioController>();
        LevelManager = FindObjectOfType<LevelManager>();
    }

    public void Respawn()
    {
        Audio.MenuButton.Play();
        LevelManager.RespawnPlayer();
    }

    public void QuitGame()
    {
        Audio.MenuButton.Play();
        Application.Quit();
    }

}
