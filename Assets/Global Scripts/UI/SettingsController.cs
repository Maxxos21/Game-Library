using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Text volumeText;


    private void Start()
    {
        AudioListener.volume = 0.5f;


        volumeSlider.value = AudioListener.volume;
        volumeText.text = Mathf.RoundToInt(volumeSlider.value * 100) + "%";
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        volumeText.text = Mathf.RoundToInt(volumeSlider.value * 100) + "%";
    }
}
