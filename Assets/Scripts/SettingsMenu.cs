using TMPro;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public static bool isMusic = true;
    public static bool isSfx = true;

    public TMP_Text musicText;
    public TMP_Text sfxText;

    private void Start()
    {
        if(isMusic)
        {
            musicText.text = "ВКЛ";
            musicText.color = Color.green;
        }
        else
        {
            musicText.text = "ВЫКЛ";
            musicText.color = Color.red;
        }

        if (isSfx)
        {
            sfxText.text = "ВКЛ";
            sfxText.color = Color.green;
        }
        else
        {
            sfxText.text = "ВЫКЛ";
            sfxText.color = Color.red;
        }
    }

    public void ToggleMusic()
    {
        isMusic = !isMusic;

        if (isMusic)
        {
            musicText.text = "ВКЛ";
            musicText.color = Color.green;
        }
        else
        {
            musicText.text = "ВЫКЛ";
            musicText.color = Color.red;
        }

        SoundManager.instance.Click();
    }

    public void ToggleSfx()
    {
        isSfx = !isSfx;

        if (isSfx)
        {
            sfxText.text = "ВКЛ";
            sfxText.color = Color.green;
        }
        else
        {
            sfxText.text = "ВЫКЛ";
            sfxText.color = Color.red;
        }

        SoundManager.instance.Click();
    }

    public void CloseButton()
    {
        SoundManager.instance.Click();
        MainMenu.instance.CloseSettings();
    }
}
