using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header ("-----Audio Sources-----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("-----Audio Clips------")]
    public AudioClip gameMusicClip;
    public AudioClip titleMusicClip;
    float sfxVolume = 1f;
    float musicVolume = 1f;
    PauseAndEndGameUI sceneCaller;
    public bool startOfGame = true;
    public static AudioManager Instance;
    
    void Start()
    {
        if (startOfGame)
        {
            musicSource.clip = titleMusicClip;
            startOfGame = false;
            Debug.Log("game start");
            musicSource.Play();
        }

        sceneCaller = FindObjectOfType<PauseAndEndGameUI>();
        Instance = this;

        if (musicSource.isPlaying) { Debug.Log("Currently Playing " + musicSource.clip); }

        LoadVolumeSettings();
    }

    public void UpdateMusic()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (startOfGame)
        {
            //if in or loading the title scene, and the title music is not playing, change the music clip being played to the title music
            Debug.LogWarning("Going To Title");

            if (musicSource.clip != titleMusicClip)
            {
                musicSource.clip = null;
                musicSource.clip = titleMusicClip;
                musicSource.Play();
                startOfGame = false;
                Debug.LogWarning("Title music playing");
                Debug.LogWarning($"UpdateMusic called from:  {currentScene}");
            }
        }
        else if(!startOfGame && musicSource.clip == gameMusicClip) { return; } //if already in game and the game music is already playing, do nothing
        else if (!startOfGame)
        {
            //if in or loading a game level, and the level music is not playing, change the music clip being played to the game music 
            musicSource.clip = null;
            musicSource.clip = gameMusicClip;
            musicSource.Play();
            sceneCaller.gameSceneCalled = false;
            Debug.LogWarning("Game music playing");
            Debug.LogWarning($"UpdateMusic called from:  {currentScene}");
        }

        if (musicSource.isPlaying) { Debug.Log("Currently Playing " + musicSource.clip); }
    }

    void LoadVolumeSettings()
    {
        float storedSFXVolume;
        float storedMusicVolume;

        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            //if player prefs already has a sfxVolume key, assign the value stored to storedSFXVolume
            storedSFXVolume = PlayerPrefs.GetFloat("sfxVolume");
        }
        else
        {
            storedSFXVolume = 1f; // Default volume
        }

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            //if player prefs already has a musicVolume key, assign the value stored to storedMusicVolume
            storedMusicVolume = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            storedMusicVolume = 1f; // Default volume
        }

        //assign volume values based on the value assigned to the stored value from earlier
        sfxVolume = storedSFXVolume;
        sfxSource.volume = sfxVolume;

        musicVolume = storedMusicVolume;
        musicSource.volume = musicVolume;
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //keep this instance across all scenes

        }
        else if(Instance != this)
        {
            Destroy(gameObject); // erase any duplicates in scene before scene loads.
        }
    }

}
