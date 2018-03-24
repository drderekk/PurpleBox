﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private AudioController Audio;

    public void Start ()
    {
        Audio = FindObjectOfType<AudioController>();
    }

    public void PlayGame ()
    {
        Audio.MenuButton.Play();
        SceneManager.LoadScene("Scene");
    }

    public void QuitGame ()
    {
        Audio.MenuButton.Play();
        Application.Quit();
    }

}
