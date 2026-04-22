using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SearchResultUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI nameText;
    private CardData myCard;

    public void Setup(CardData card)
    {
        myCard = card;
        icon.sprite = card.cardIcon;
        nameText.text = card.cardName;
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        GameManager.Instance.MakeGuess(myCard);
    }
}