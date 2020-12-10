using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighscoreTableHard : MonoBehaviour
{
    private static HighscoreTableHard instance;

    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        instance = this;

        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        string jsonString = PlayerPrefs.GetString("highscoreTableEndlessRunnerHard");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        entryTemplate.gameObject.SetActive(false);

        gameObject.SetActive(false);
    }

    private void Refresh()
    {
        string jsonString = PlayerPrefs.GetString("highscoreTableEndlessRunnerHard");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        // Sort entry list by Score
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    // Swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        if (highscoreEntryTransformList != null)
        {
            foreach (Transform highscoreEntryTransform in highscoreEntryTransformList)
            {
                Destroy(highscoreEntryTransform.gameObject);
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 40f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().text = rankString;

        int score = highscoreEntry.score;

        entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().text = name;

        // Set background visible odds and evens, easier to read
        entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);

        // Highlight First
        if (rank == 1)
        {
            entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().color = Color.green;
            entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().color = Color.green;
            entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().color = Color.green;
        }

        if (rank == 2)
        {
            entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().color = Color.yellow;
            entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().color = Color.yellow;
            entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().color = Color.yellow;
        }

        if (rank == 3)
        {
            entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().color = Color.red;
            entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().color = Color.red;
            entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().color = Color.red;
        }

        transformList.Add(entryTransform);
    }

    public static void AddHighscoreEntry_Static(int score, string name)
    {
        instance.AddHighscoreEntry(score, name);
    }

    private void AddHighscoreEntry(int score, string name)
    {
        // Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        // Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTableEndlessRunnerHard");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            // There's no stored table, initialize
            highscores = new Highscores()
            {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }

        // Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        // Remove highscores outside Top 10
        if (highscores.highscoreEntryList.Count > 8)
        {
            highscores.highscoreEntryList.RemoveAt(7);
        }

        // Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTableEndlessRunnerHard", json);
        PlayerPrefs.Save();
    }

    public static void Show()
    {
        instance.Refresh();
        instance.gameObject.SetActive(true);
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    /*
     * Represents a single High score entry
     * */
    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
}
