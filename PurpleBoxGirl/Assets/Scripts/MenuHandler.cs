using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour {

    public CanvasGroup MainMenuGroup;
    public CanvasGroup SettingsGroup;

    private AudioController Audio;

    public void Start ()
    {
        Audio = FindObjectOfType<AudioController>();
        this.ShowMainMenu();
    }

    // Main Menu Buttons -------------------------------
    public void PlayGame ()
    {
        Audio.PlayMenuButtonSound();
        SceneManager.LoadScene("MainGame");
    }

    public void OpenSettings()
    {
        Audio.PlayMenuButtonSound();
        this.ShowSettings();
    }

    public void QuitGame()
    {
        Audio.PlayMenuButtonSound();
        Application.Quit();
    }
    //-------------------------------------------------

    // Settings page Buttons --------------------------
    public void ReturnToMenu()
    {
        Audio.PlayMenuButtonSound();
        this.ShowMainMenu();
    }
    //-------------------------------------------------

    public void ShowMainMenu()
    {
        this.SetCanvasGroupVisibility(MainMenuGroup, true);
        this.SetCanvasGroupVisibility(SettingsGroup, false);
    }

    public void ShowSettings()
    {
        this.SetCanvasGroupVisibility(MainMenuGroup, false);
        this.SetCanvasGroupVisibility(SettingsGroup, true);
    }


    private void SetCanvasGroupVisibility(CanvasGroup group, bool visible)
    {
        group.interactable = visible;
        group.blocksRaycasts = visible;

        if (visible)
        {
            group.alpha = 1;
        }
        else
        {
            group.alpha = 0;
        }
    }

}
