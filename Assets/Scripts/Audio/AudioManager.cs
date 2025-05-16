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
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        /*
        if (sceneCaller.gameSceneCalled)
        {
            if (sceneIndex == 0)
            {
                if (musicSource.clip != gameMusicClip)
                {
                    musicSource.clip = null;
                    musicSource.clip = gameMusicClip;
                    musicSource.Play();
                    sceneCaller.gameSceneCalled = false;
                    Debug.LogWarning("Game music playing");
                    Debug.LogWarning($"UpdateMusic called from:  { currentScene }");
                }   
            }
            else if(sceneIndex >= 0 && musicSource.clip != gameMusicClip)
            {
                musicSource.clip = null;
                musicSource.clip = gameMusicClip;
                musicSource.Play();
                sceneCaller.gameSceneCalled = false;
                Debug.LogWarning("Game music playing");
                Debug.LogWarning($"UpdateMusic called from:  {currentScene}");
            }
            return;
        }
        */
        if (startOfGame)
        {
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
        else if(!startOfGame && musicSource.clip == gameMusicClip) { return; }
        else if (!startOfGame)
        {
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
            storedSFXVolume = PlayerPrefs.GetFloat("sfxVolume");
        }
        else
        {
            storedSFXVolume = 1f; // Default volume
        }

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            storedMusicVolume = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            storedMusicVolume = 1f; // Default volume
        }

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
        }
        else
        {
            Destroy(gameObject);
        }

        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

    }

}
