using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HistoryItemUI : MonoBehaviour
{
    public Image cardIcon;
    public Color correctColor = new Color(0.2f, 0.8f, 0.2f);
    public Color wrongColor = new Color(0.8f, 0.2f, 0.2f);

    [System.Serializable]
    public struct CompareUI
    {
        public TextMeshProUGUI text;
        public Image bg;
        public GameObject upArrow;
        public GameObject downArrow;
    }

    public CompareUI elixirUI;
    public CompareUI rarityUI;
    public CompareUI typeUI;
    public CompareUI targetsUI;
    public CompareUI rangeUI;
    public CompareUI hitSpeedUI;
    public CompareUI moveSpeedUI;
    public CompareUI yearUI;

    public void Setup(CardData guess, CardData secret)
    {
        cardIcon.sprite = guess.cardIcon;

        CompareInt(guess.elixirCost, secret.elixirCost, elixirUI);
        CompareEnum((int)guess.rarity, (int)secret.rarity, guess.rarity.ToString(), rarityUI);
        CompareString(guess.type, secret.type, typeUI);
        CompareString(guess.targets, secret.targets, targetsUI);
        CompareFloat(guess.range, secret.range, rangeUI);
        CompareFloat(guess.hitSpeed, secret.hitSpeed, hitSpeedUI);
        CompareEnum((int)guess.movementSpeed, (int)secret.movementSpeed, guess.movementSpeed.ToString(), moveSpeedUI);
        CompareInt(guess.releaseYear, secret.releaseYear, yearUI);
    }

    private void CompareInt(int guessVal, int secretVal, CompareUI ui)
    {
        ui.text.text = guessVal.ToString();
        ui.bg.color = guessVal == secretVal ? correctColor : wrongColor;
        if (ui.upArrow != null) ui.upArrow.SetActive(secretVal > guessVal);
        if (ui.downArrow != null) ui.downArrow.SetActive(secretVal < guessVal);
    }

    private void CompareFloat(float guessVal, float secretVal, CompareUI ui)
    {
        ui.text.text = guessVal.ToString("F1");
        ui.bg.color = Mathf.Approximately(guessVal, secretVal) ? correctColor : wrongColor;
        if (ui.upArrow != null) ui.upArrow.SetActive(secretVal > guessVal);
        if (ui.downArrow != null) ui.downArrow.SetActive(secretVal < guessVal);
    }

    private void CompareString(string guessVal, string secretVal, CompareUI ui)
    {
        ui.text.text = guessVal;
        ui.bg.color = guessVal == secretVal ? correctColor : wrongColor;
        if (ui.upArrow != null) ui.upArrow.SetActive(false);
        if (ui.downArrow != null) ui.downArrow.SetActive(false);
    }

    private void CompareEnum(int guessVal, int secretVal, string displayText, CompareUI ui)
    {
        ui.text.text = displayText;
        ui.bg.color = guessVal == secretVal ? correctColor : wrongColor;
        if (ui.upArrow != null) ui.upArrow.SetActive(secretVal > guessVal);
        if (ui.downArrow != null) ui.downArrow.SetActive(secretVal < guessVal);
    }
}