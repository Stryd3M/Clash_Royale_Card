using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private List<CardData> allCards;
    private CardData secretCard;

    public int maxAttempts = 5;
    private int currentAttempts = 0;

    public TMP_InputField searchInput;
    public Transform searchResultsContainer;
    public GameObject searchResultPrefab;

    public Transform historyContainer;
    public GameObject historyItemPrefab;
    public TextMeshProUGUI attemptsText;

    void Awake()
    {
        Instance = this;
        allCards = Resources.LoadAll<CardData>("Cards").ToList();
        StartNewGame();
    }

    public void StartNewGame()
    {
        if (allCards.Count == 0) return;

        secretCard = allCards[Random.Range(0, allCards.Count)];
        currentAttempts = 0;
        UpdateAttemptsUI();

        foreach (Transform child in historyContainer) Destroy(child.gameObject);
        searchInput.text = "";
    }

    public void OnSearchInputChanged(string query)
    {
        foreach (Transform child in searchResultsContainer) Destroy(child.gameObject);

        if (string.IsNullOrWhiteSpace(query)) return;

        var results = allCards.Where(c => c.cardName.ToLower().Contains(query.ToLower())).ToList();

        foreach (var card in results)
        {
            var btnObj = Instantiate(searchResultPrefab, searchResultsContainer);
            btnObj.GetComponent<SearchResultUI>().Setup(card);
        }
    }

    public void MakeGuess(CardData guessedCard)
    {
        if (currentAttempts >= maxAttempts) return;

        currentAttempts++;
        UpdateAttemptsUI();

        var historyObj = Instantiate(historyItemPrefab, historyContainer);
        historyObj.GetComponent<HistoryItemUI>().Setup(guessedCard, secretCard);

        searchInput.text = "";
        OnSearchInputChanged("");
    }

    void UpdateAttemptsUI()
    {
        if (attemptsText != null)
            attemptsText.text = $"Осталось попыток: {maxAttempts - currentAttempts}";
    }
}