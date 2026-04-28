using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using YG;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private List<CardData> allCards;
    private CardData secretCard;
    private HashSet<CardData> guessedCards = new HashSet<CardData>();

    private HashSet<string> unlockedCardNames = new HashSet<string>();

    public int maxAttempts = 5;
    private int currentAttempts = 0;

    [Header("UI Panels")]
    public GameObject gameCanvas;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject collectionPanel;

    [Header("Collection UI")]
    public Transform collectionContainer;
    public GameObject collectionItemPrefab;
    public GameObject emptyCollectionText;
    public TextMeshProUGUI collectionProgressText;

    [Header("Game UI")]
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
        LoadCollectionProgress();
        StartNewGame();
    }

    public void StartNewGame()
    {
        if (allCards.Count == 0) return;

        secretCard = allCards[Random.Range(0, allCards.Count)];
        currentAttempts = 0;
        guessedCards.Clear();

        gameCanvas.SetActive(true);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        collectionPanel.SetActive(false);

        UpdateAttemptsUI();
        foreach (Transform child in historyContainer) Destroy(child.gameObject);
        searchInput.text = "";
    }

    public void MakeGuess(CardData guessedCard)
    {
        if (currentAttempts >= maxAttempts || guessedCards.Contains(guessedCard)) return;

        currentAttempts++;
        guessedCards.Add(guessedCard);
        UpdateAttemptsUI();

        var historyObj = Instantiate(historyItemPrefab, historyContainer);
        historyObj.transform.SetAsFirstSibling();
        historyObj.GetComponent<HistoryItemUI>().Setup(guessedCard, secretCard);

        searchInput.text = "";
        OnSearchInputChanged("");

        CheckGameState(guessedCard);
    }

    private void CheckGameState(CardData guessedCard)
    {
        if (guessedCard == secretCard)
        {
            UnlockCard(guessedCard);
            StartCoroutine(ShowWinScreenDelayed());
        }
        else if (currentAttempts >= maxAttempts)
        {
            ShowEndScreen(losePanel);
            SoundManager.instance.Lose();
        }
    }

    private void UnlockCard(CardData card)
    {
        if (!unlockedCardNames.Contains(card.cardName))
        {
            unlockedCardNames.Add(card.cardName);
            SaveCollectionProgress();
        }
    }

    public void OpenCollectionPanel()
    {
        SoundManager.instance.Click();
        gameCanvas.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        collectionPanel.SetActive(true);

        foreach (Transform child in collectionContainer) Destroy(child.gameObject);

        bool isEmpty = unlockedCardNames.Count == 0;
        emptyCollectionText.SetActive(isEmpty);

        collectionContainer.parent.parent.GetComponent<UnityEngine.UI.ScrollRect>().enabled = !isEmpty;

        collectionProgressText.text = $"{unlockedCardNames.Count}/{allCards.Count}";

        if (!isEmpty)
        {
            var unlockedCards = allCards.Where(c => unlockedCardNames.Contains(c.cardName)).ToList();

            foreach (var card in unlockedCards)
            {
                var itemObj = Instantiate(collectionItemPrefab, collectionContainer);
                itemObj.GetComponent<CollectionItemUI>().Setup(card);
            }
        }
    }

    public void CloseCollectionPanel()
    {
        SoundManager.instance.Click();
        collectionPanel.SetActive(false);
        gameCanvas.SetActive(true);
    }
    private void SaveCollectionProgress()
    {
        string saveString = string.Join(",", unlockedCardNames);
        PlayerPrefs.SetString("UnlockedCards", saveString);
        PlayerPrefs.Save();
    }

    private void LoadCollectionProgress()
    {
        unlockedCardNames.Clear();
        if (PlayerPrefs.HasKey("UnlockedCards"))
        {
            string saveString = PlayerPrefs.GetString("UnlockedCards");
            if (!string.IsNullOrEmpty(saveString))
            {
                string[] names = saveString.Split(',');
                foreach (var n in names) unlockedCardNames.Add(n);
            }
        }
    }

    private IEnumerator ShowWinScreenDelayed()
    {
        YG2.saves.AddWin();
        YG2.SaveProgress();

        yield return new WaitForSeconds(1.5f);
        SoundManager.instance.Win();
        gameCanvas.SetActive(false);
        winPanel.SetActive(true);
        winPanel.GetComponent<WinPanelUI>().Setup(secretCard);
    }

    private void ShowEndScreen(GameObject panel)
    {
        gameCanvas.SetActive(false);
        panel.SetActive(true);
    }

    public void RestartButton()
    {
        SoundManager.instance.Click();
        StartNewGame();
    }
    public void MainMenuButton()
    {
        SoundManager.instance.Click();
        SceneManager.LoadScene("Menu");
    }

    public void AddExtraAttempt()
    {
        SoundManager.instance.Click();
        maxAttempts++;
        currentAttempts--;
        losePanel.SetActive(false);
        gameCanvas.SetActive(true);
        UpdateAttemptsUI();
    }

    void UpdateAttemptsUI()
    {
        if (attemptsText != null)
            attemptsText.text = $"Осталось попыток: {maxAttempts - currentAttempts}";
    }

    public void OnSearchInputChanged(string query)
    {
        foreach (Transform child in searchResultsContainer) Destroy(child.gameObject);
        if (string.IsNullOrWhiteSpace(query)) return;
        var results = allCards.Where(c => c.cardName.ToLower().Contains(query.ToLower()) && !guessedCards.Contains(c)).ToList();
        foreach (var card in results)
        {
            var btnObj = Instantiate(searchResultPrefab, searchResultsContainer);
            btnObj.GetComponent<SearchResultUI>().Setup(card);
        }
    }
}