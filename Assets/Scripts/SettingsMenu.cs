using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public static float musicVolume = 1f;
    public static float sfxVolume = 1f;

    [Header("Слайдеры")]
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        if (musicSlider != null) musicSlider.value = musicVolume;
        if (sfxSlider != null) sfxSlider.value = sfxVolume;
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
    }

    public void SetSfxVolume(float value)
    {
        sfxVolume = value;
    }

    public void CloseButton()
    {
        SoundManager.instance.Click();
        MainMenu.instance.CloseSettings();
    }
}