using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{

    public GameObject menuRoot;
    public GameObject menuOptions;

    public Slider audioVolumeSlider;

    void Start()
    {
        audioVolumeSlider.value = PersistentGameState.instance.audioVolume;
        audioVolumeSlider.onValueChanged.AddListener(delegate { AudioVolumeChangedCallback(); });
    }

    void Update()
    {

    }

    public void SwitchToOptions()
    {
        menuRoot.SetActive(false);
        menuOptions.SetActive(true);
    }

    public void SwitchToRoot()
    {
        menuRoot.SetActive(true);
        menuOptions.SetActive(false);
    }

    public void LaunchGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void AudioVolumeChangedCallback()
    {
        PersistentGameState.instance.audioVolume = audioVolumeSlider.value;
    }
}
