using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer myMixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    private void Awake()
    {
        LoadVolume();
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        if (PlayerPrefs.GetFloat("musicVolume") != volume) // Only update if changed
        {
            myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("musicVolume", volume);
            PlayerPrefs.Save();
            Debug.Log($"Updated music volume: {volume}");
        }

    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        if (PlayerPrefs.GetFloat("sfxVolume") != volume) // Only update if changed
        {
            myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("sfxVolume", volume);
            PlayerPrefs.Save();
            Debug.Log($"Updated SFX volume: {volume}");
        }

    }

    public void LoadVolume()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
            SetMusicVolume();
        }

        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
            SetSFXVolume();
        }

    }

}
