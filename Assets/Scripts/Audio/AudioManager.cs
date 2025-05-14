using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header ("-----Audio Sources-----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("-----Audio Clips------")]
    public AudioClip musicClip;
    public AudioClip jumpClip;
    public AudioClip doubleJumpClip;
    public AudioClip deathClip;
    public AudioClip checkPointGot;
    public AudioClip gameWonClip;
    public AudioClip collectableGot; 
    
    void Start()
    {
        musicSource.clip = musicClip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

}
