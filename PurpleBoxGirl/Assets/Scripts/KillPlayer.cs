using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour {

    public LevelManager LevelManager;
    private AudioController Audio;
    public LevelGenerator LevelGenerator;


    void Start () {
        LevelManager = FindObjectOfType<LevelManager>();
        LevelGenerator = FindObjectOfType<LevelGenerator>();
        Audio = FindObjectOfType<AudioController>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
		if (other.name == "Player") {
			StartCoroutine ("FadeOutMusic");
			LevelManager.PlayerDeath();
			Audio.PlayDeathSound();
		}

    }

    public IEnumerator FadeOutMusic()
    {
        float StartVolume = 0.2f;
        while (Audio.Music.volume > 0)
        {
            Audio.Music.volume -= StartVolume * Time.deltaTime * 4f;

            yield return null;
        }

        Audio.Music.Stop();
        Audio.Music.volume = 0.2f;

    }

}
