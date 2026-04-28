using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;

    [Space]
    [Header("Звуки")]
    public AudioClip click1;
    public AudioClip lose;
    public AudioClip win;

    [Space]
    public AudioClip music;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        musicAudioSource.clip = music;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }

    private void Update()
    {
        musicAudioSource.volume = SettingsMenu.musicVolume;
        sfxAudioSource.volume = SettingsMenu.sfxVolume;
    }

    public void Click()
    {
        sfxAudioSource.PlayOneShot(click1);
    }

    public void Win()
    {
        sfxAudioSource.PlayOneShot(win);
    }

    public void Lose()
    {
        sfxAudioSource.PlayOneShot(lose);
    }

    internal void click()
    {
        throw new NotImplementedException();
    }
}
