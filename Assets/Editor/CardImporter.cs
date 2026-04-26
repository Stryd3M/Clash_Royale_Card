using UnityEngine;
using UnityEditor;
using System.IO;
using System.Globalization;

public class CardImporter : EditorWindow
{
    private string csvData = "";

    [MenuItem("Clash Royale/Импорт Карт из Текста")]
    public static void ShowWindow()
    {
        GetWindow<CardImporter>("Импорт Карт");
    }

    void OnGUI()
    {
        GUILayout.Label("Вставь CSV текст с картами:", EditorStyles.boldLabel);
        csvData = EditorGUILayout.TextArea(csvData, GUILayout.Height(200));

        if (GUILayout.Button("Сгенерировать файлы карт"))
        {
            ParseAndCreateCards();
        }
    }

    private void ParseAndCreateCards()
    {
        if (string.IsNullOrWhiteSpace(csvData)) return;

        if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            AssetDatabase.CreateFolder("Assets", "Resources");
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Cards"))
            AssetDatabase.CreateFolder("Assets/Resources", "Cards");

        string[] lines = csvData.Split('\n');
        int count = 0;

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] data = line.Trim().Split(';');
            if (data.Length < 9) continue;

            CardData newCard = ScriptableObject.CreateInstance<CardData>();
            newCard.cardName = data[0];
            newCard.elixirCost = int.Parse(data[1]);
            newCard.rarity = (CardRarity)System.Enum.Parse(typeof(CardRarity), data[2], true);
            newCard.type = data[3];
            newCard.targets = data[4];

            newCard.range = float.Parse(data[5].Replace(',', '.'), CultureInfo.InvariantCulture);
            newCard.hitSpeed = float.Parse(data[6].Replace(',', '.'), CultureInfo.InvariantCulture);

            newCard.movementSpeed = (MovementSpeed)System.Enum.Parse(typeof(MovementSpeed), data[7], true);
            newCard.releaseYear = int.Parse(data[8]);

            string assetPath = $"Assets/Resources/Cards/{newCard.cardName}.asset";
            AssetDatabase.CreateAsset(newCard, assetPath);
            count++;
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"Успешно сгенерировано карт: {count}");
    }
}