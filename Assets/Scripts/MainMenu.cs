using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel;

    private Image[] s;

    public static MainMenu instance;
    private void Awake()
    {
        s = gameObject.GetComponentsInChildren<Image>();

        instance = this;
    }

    private void Start()
    {
        YG2.GetLanguage();
        YG2.GameReadyAPI();

        YG2.InterstitialAdvShow();
    }

    public void Play()
    {
        SoundManager.instance.Click();
        SceneManager.LoadScene("Game");
    }

    public void OpenSettings()
    {
        SoundManager.instance.Click();
        settingsPanel.SetActive(true);

        foreach (Image go in s)
        {
            if (go != null)
            {
                go.gameObject.SetActive(false);
            }
        }
    }

    public void CloseSettings()
    {
        SoundManager.instance.Click();
        settingsPanel.SetActive(false);

        foreach (Image go in s)
        {
            if (go != null)
            {
                go.gameObject.SetActive(true);
            }
        }
    }
}
