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
        Audio.PlayMenuButtonSound();
        LevelManager.RespawnPlayer();
    }

    public void ReturnToMenu()
    {
        Audio.PlayMenuButtonSound();
        SceneManager.LoadScene("Menu");
    }

}
