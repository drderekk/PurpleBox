using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioSource Death;
    public AudioSource Respawn;
    public AudioSource Jump;

    public AudioSource Music;

    private static bool AudioManagerExists;

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
}