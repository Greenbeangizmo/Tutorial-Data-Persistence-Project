using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    //public List<HighscoreEntry> highscoreEntryList;
    private List<Transform> transformList;

    private float templateHeight;

    //public int highScore;
    //public string playerName;

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
       


        //----------------------------------------------------------------------------------------
        //Add a new entry

        //SaveData addNewEntry = new SaveData { highScore = 999, playerName = "Test" };
        //InfoManager.saveDataList.Add(addNewEntry);

        //----------------------------------------------------------------------------------------



        InfoManager.AddHighscoreEntry();


        //transformList = new List<Transform>();
        //foreach (HighscoreEntry highscoreEntry in highscoreEntryList)
        //{
        //    CreateHighscoreEntryTransform(highscoreEntry, entryContainer, transformList);
        //}

        transformList = new List<Transform>();
        foreach (SaveData highscoreEntry in InfoManager.saveDataList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, transformList);
        }


        //string json = JsonUtility.ToJson(highscoreEntryList);


    }

    //public void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    //{
    //    entryContainer = transform.Find("Scores Container");
    //    entryTemplate = entryContainer.Find("Scores Template");

    //    entryTemplate.gameObject.SetActive(false);

    //    Transform entryTransform = Instantiate(entryTemplate, container);
    //    RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
    //    entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
    //    entryTransform.gameObject.SetActive(true);

    //    int rank = transformList.Count + 1;
    //    string rankString;
    //    switch (rank)
    //    {
    //        default:
    //            rankString = rank + "TH"; break;

    //        case 1: rankString = "1ST"; break;
    //        case 2: rankString = "2ND"; break;
    //        case 3: rankString = "3RD"; break;
    //    }

    //    entryTransform.Find("RankText").GetComponent<Text>().text = rankString;



    //    string name = highscoreEntry.playerName;

    //    entryTransform.Find("NameText").GetComponent<Text>().text = name;



    //    int score = highscoreEntry.highScore;

    //    entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();

    //    transformList.Add(entryTransform);

    //}

    public void CreateHighscoreEntryTransform(SaveData highscoreEntry, Transform container, List<Transform> transformList)
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

        transformList.Add(entryTransform);

    }

    public void NewPlayerName(string newName)
    {
        InfoManager.currentPlayerName = newName;
        Debug.Log(InfoManager.currentPlayerName);
    }

    //public class HighscoreEntry
    //{
    //    public int highScore;
    //    public string playerName;
    //}
}
