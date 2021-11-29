using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    public List<HighscoreEntry> highscoreEntryList;
    private List<Transform> transformList;

    private float templateHeight;

    private void Start()
    {
        entryContainer = transform.Find("Scores Container");
        entryTemplate = entryContainer.Find("Scores Template");

        entryTemplate.gameObject.SetActive(false);

        RectTransform container = (RectTransform)entryContainer.transform;
        float entryContainerHeight = container.rect.height;

        templateHeight = container.rect.height / 10f;
        if (templateHeight < 20f)
        {
            templateHeight = 20f;
        }

        highscoreEntryList = new List<HighscoreEntry>() {
            new HighscoreEntry{ highScore = 0, playerName = "A" },
            new HighscoreEntry{ highScore = 0, playerName = "A" },
            new HighscoreEntry{ highScore = 0, playerName = "A" },
            new HighscoreEntry{ highScore = 0, playerName = "A" },
            new HighscoreEntry{ highScore = 0, playerName = "A" },
            new HighscoreEntry{ highScore = 0, playerName = "A" },
            new HighscoreEntry{ highScore = 0, playerName = "A" },
            new HighscoreEntry{ highScore = 0, playerName = "A" },
            new HighscoreEntry{ highScore = 0, playerName = "A" },
            new HighscoreEntry{ highScore = 0, playerName = "A" }
            };


        //Copy list from InfoManager
        for (int i = 0; i < InfoManager.scoreList.Count; i++)
        {
            highscoreEntryList[i].playerName = InfoManager.scoreList[i].playerName;
            highscoreEntryList[i].highScore = InfoManager.scoreList[i].highScore;
        }

        transformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, transformList);
        }

    }


    public void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        entryContainer = transform.Find("Scores Container");
        entryTemplate = entryContainer.Find("Scores Template");

        entryTemplate.gameObject.SetActive(false);

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

        entryTransform.Find("RankText").GetComponent<Text>().text = rankString;



        string name = highscoreEntry.playerName;

        entryTransform.Find("NameText").GetComponent<Text>().text = name;



        int score = highscoreEntry.highScore;

        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();


        entryTransform.Find("ScoreBackground").gameObject.SetActive(rank % 2 == 1);

        transformList.Add(entryTransform);

    }

    public void NewPlayerName(string newName)
    {
        InfoManager.currentPlayerName = newName;
        Debug.Log(InfoManager.currentPlayerName);

        InfoManager.isNewHighScore = true;
    }

    public class HighscoreEntry
    {
        public int highScore;
        public string playerName;

        public List<HighscoreEntry> highscoreEntryList;
    }
}
