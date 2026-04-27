using UnityEngine;

public class MiniMenu : MonoBehaviour
{
    public GameObject miniMenu;

    private bool isActive;

    public void ToggleActive()
    {
        isActive = !isActive;

        miniMenu.SetActive(isActive);

        SoundManager.instance.Click();
    } 
}
