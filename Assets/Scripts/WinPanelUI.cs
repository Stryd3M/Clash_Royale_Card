using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinPanelUI : MonoBehaviour
{
    public Image correctCardIcon;
    public TextMeshProUGUI winText;

    public void Setup(CardData card)
    {
        correctCardIcon.sprite = card.cardIcon;
        winText.text = $"Победа! Правильная карта: {card.cardName}";
    }
}