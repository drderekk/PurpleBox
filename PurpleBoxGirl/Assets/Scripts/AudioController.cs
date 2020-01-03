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

    public float MaxVolume;

    [Range(0.0f, 1.0f)]
    public float VolumePercent;

    private static bool AudioManagerExists = false;

    private float currentVolume;

    void Start()
    {
        if (!AudioManagerExists)
        {
            AudioManagerExists = true;

            UpdateVolumeOfSounds();

            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Update()
    {
        if(CalculateCurrentVolume() != currentVolume)
        {
            UpdateVolumeOfSounds();
        }
    }

    public void SetVolumePercent(float newVolumePercent)
    {
        VolumePercent = newVolumePercent;
        UpdateVolumeOfSounds();
    }

    public void UpdateVolumeOfSounds()
    {
        currentVolume = CalculateCurrentVolume();

        Jump.volume = currentVolume;
        Death.volume = currentVolume;
        Respawn.volume = currentVolume;
        Bonus1.volume = currentVolume;
        Bonus2.volume = currentVolume;
        Bonus3.volume = currentVolume;
        MenuButton.volume = currentVolume;

        Music.volume = currentVolume;
}

    public float CalculateCurrentVolume()
    {
        if(VolumePercent >= 1)
        {
            VolumePercent = 1;
            return MaxVolume;
        }
        else if (VolumePercent <= 0)
        {
            VolumePercent = 0;
            return 0;
        }

        return VolumePercent * MaxVolume;
    }

	public void PlayJumpSound(){
        // Randomise pitch of jump sound before playing it (Between 0.7 and 1.2)
		Jump.pitch = 1 + UnityEngine.Random.Range(-3, 2)/10f;
		Jump.Play();
	}

	public void PlayDeathSound(){
        // Randomise pitch of death sound before playing it (Between 0.7 and 1.2)
        Death.pitch = 1 + UnityEngine.Random.Range(-3, 2)/10f;
        Death.Play();
	}

    public void PlayRespawnSound()
    {
        // Randomise pitch of respawn sound before playing it (Between 0.7 and 1.2)
        Respawn.pitch = 1 + UnityEngine.Random.Range(-3, 2) / 10f;
        Respawn.Play();
    }

    public void PlayBonusSound(int bonus)
    {
        switch (bonus)
        {
            case 1:
                Bonus1.Play();
                break;
            case 2:
                Bonus2.Play();
                break;
            default:
                Bonus3.Play();
                break;
        }
    }

    public void PlayMenuButtonSound()
    {
        MenuButton.Play();
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
        float StartVolume = currentVolume;
        while (Music.volume > 0)
        {
            Music.volume -= StartVolume * Time.deltaTime * 3f;

            yield return null;
        }

        Music.Stop();
        Music.volume = currentVolume;
    }
}