using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{

    [SerializeField] GameObject optionsMenu;

    const string MIXER_MUSIC = "musicVolume";
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;

    private void Awake()
    {
        slider.onValueChanged.AddListener(SetMusicVolume);
    }

    void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
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
