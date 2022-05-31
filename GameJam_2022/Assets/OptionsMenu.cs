using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;

    float masterVolume;
    const string MIXER_MUSIC = "musicVolume";

    private void OnEnable()
    {
        mixer.GetFloat("musicVolume", out masterVolume);
        slider.value = masterVolume;
    }

    private void Awake()
    {
        slider.onValueChanged.AddListener(SetMusicVolume);
    }

    void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC, value);
    }

    public void ShowOptions()
    {
        optionsMenu.SetActive(true);
    }

    public void HideMenu()
    {
        optionsMenu.SetActive(false);
    }
}
