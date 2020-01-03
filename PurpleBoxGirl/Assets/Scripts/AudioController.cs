using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource Death;
    public AudioSource Respawn;
    public AudioSource Jump;
    public AudioSource Bonus1;
    public AudioSource Bonus2;
    public AudioSource Bonus3;
    public AudioSource MenuButton;

    public AudioSource Music;

    private static bool AudioManagerExists = false;

    void Start()
    {
        if (!AudioManagerExists)
        {
            AudioManagerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

	/*
	 * Randomises the pitch of the jump sound between 0.7 and 1.2
	 * before playing the sound.
	 */
	public void PlayJumpSound(){
		Jump.pitch = 1 + UnityEngine.Random.Range(-3, 2)/10f;
		Jump.Play();
	}

	/*
	 * Randomises the pitch of the death sound between 0.7 and 1.2
	 * before playing the sound.
	 */
	public void PlayDeathSound(){
		Death.pitch = 1 + UnityEngine.Random.Range(-3, 2)/10f;
		Death.Play();
	}

    public void PlayMusic()
    {
        Music.Play();
    }

    public void FadeOutMusic()
    {
        StartCoroutine("FadeOutMusicCoroutine");
    }


    public IEnumerator FadeOutMusicCoroutine()
    {
        float StartVolume = 0.2f;
        while (Music.volume > 0)
        {
            Music.volume -= StartVolume * Time.deltaTime * 3f;

            yield return null;
        }

        Music.Stop();
        Music.volume = 0.2f;
    }
}