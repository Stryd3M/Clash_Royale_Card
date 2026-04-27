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

    private void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);

        musicAudioSource.clip = music;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }

    private void Update()
    {
        if(SettingsMenu.isMusic == false)
        {
            musicAudioSource.volume = 0f;
        }
        else
        {
            musicAudioSource.volume = 1f;
        }

        if(SettingsMenu.isSfx == false)
        {
            sfxAudioSource.volume = 0f;
        }
        else
        {
            sfxAudioSource.volume = 1f;
        }
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
