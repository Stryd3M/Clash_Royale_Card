using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectionItemUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI nameText;

    public void Setup(CardData card)
    {
        icon.sprite = card.cardIcon;
        nameText.text = card.cardName;
    }
}