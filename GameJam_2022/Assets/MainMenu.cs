using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{

    [SerializeField] GameObject playLights;
    [SerializeField] GameObject quitLights;
    [SerializeField] GameObject optionsLights;
    [SerializeField] GameObject playButton;
    [SerializeField] GameObject quitButton;
    [SerializeField] GameObject optionsButton;
    [SerializeField] GameObject optionsMenu;

    [SerializeField] LevelLoader loader;
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;

    bool isPlaying = false;

    const string MIXER_MUSIC = "musicVolume";

    private void Awake()
    {
        slider.onValueChanged.AddListener(SetMusicVolume);
    }

    void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    }

    public void ShowPlayLights()
    {
        playLights.SetActive(true);
    }

    public void ShowQuitLights()
    {
        quitLights.SetActive(true);
    }

    public void ShowOptionsLights()
    {
        optionsLights.SetActive(true);
    }

    public void HideLights()
    {
        if (isPlaying) return;
        playLights.SetActive(false);
        optionsLights.SetActive(false);
        quitLights.SetActive(false);
    }

    public void PlayGame()
    {
        isPlaying = true;
        loader.LoadNextLevel();
    }

    public void QuitGame()
    {

    }

    public void ShowOptions()
    {
        HideLights();
        playButton.SetActive(false);
        quitButton.SetActive(false);
        optionsButton.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void ShowMainMenu()
    {
        playButton.SetActive(true);
        quitButton.SetActive(true);
        optionsButton.SetActive(true);
        optionsMenu.SetActive(false);
    }
}
