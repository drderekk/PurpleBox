using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderHandler : MonoBehaviour
{
    public Slider VolumeSlider;

    private TextMeshProUGUI VolumeText;
    private AudioController Audio;

    // Start is called before the first frame update
    void Start()
    {
        Audio = FindObjectOfType<AudioController>();
        VolumeText = GameObject.Find("VolumeText").GetComponent<TextMeshProUGUI>();

        VolumeSlider.onValueChanged.AddListener(delegate { SetVolume(); });

        // Loads current volume percent from local storage (Set to 1 if none set) and sets the volume slider to that value
        VolumeSlider.value = PlayerPrefs.GetFloat(AudioController.VOLUME_PERCENT_KEY, 1);
        SetVolume();
    }

    private void SetVolume()
    {
        Audio.SetVolumePercent(VolumeSlider.value);

        int VolumePercentage = (int)(VolumeSlider.value * 100);

        VolumeText.SetText("Volume: " + VolumePercentage + "%");

    }
}
