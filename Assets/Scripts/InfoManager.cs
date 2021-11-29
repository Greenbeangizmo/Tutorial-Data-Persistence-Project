using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class InfoManager : MonoBehaviour
{
    public static InfoManager Instance;

    public static string currentPlayerName;

    public static bool isNewHighScore;
    public static bool isReadyToSave;

    public static bool doesCurrentNameExist;

    public HighScore[] highScores = new HighScore[10];
    public static HighScore currentHighScore;

    public static List<ScoreList> scoreList;

    private string savePath; //Save path


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);



        //Default player names
        highScores[0].playerName = "GBG";
        highScores[1].playerName = "RBG";
        highScores[2].playerName = "ECC";
        highScores[3].playerName = "EGG";
        highScores[4].playerName = "NVM";
        highScores[5].playerName = "BFG";
        highScores[6].playerName = "EIH";
        highScores[7].playerName = "AHH";
        highScores[8].playerName = "NOT";
        highScores[9].playerName = "GIZ";

        //Default high scores
        highScores[0].highScore = 521;
        highScores[1].highScore = 322;
        highScores[2].highScore = 232;
        highScores[3].highScore = 163;
        highScores[4].highScore = 112;
        highScores[5].highScore = 107;
        highScores[6].highScore = 79;
        highScores[7].highScore = 58;
        highScores[8].highScore = 19;
        highScores[9].highScore = 6;



        //Default score list to be replaced
        scoreList = new List<ScoreList>() {
            new ScoreList{ highScore = 0, playerName = "A" },
            new ScoreList{ highScore = 0, playerName = "A" },
            new ScoreList{ highScore = 0, playerName = "A" },
            new ScoreList{ highScore = 0, playerName = "A" },
            new ScoreList{ highScore = 0, playerName = "A" },
            new ScoreList{ highScore = 0, playerName = "A" },
            new ScoreList{ highScore = 0, playerName = "A" },
            new ScoreList{ highScore = 0, playerName = "A" },
            new ScoreList{ highScore = 0, playerName = "A" },
            new ScoreList{ highScore = 0, playerName = "A" }
            };


        savePath = Application.persistentDataPath + "/savefile.json";
        Debug.Log(savePath);

        if (File.Exists(savePath))
        {
            Debug.Log("File Exists");
            LoadFromJson();
        }
        else
        {
            Debug.Log("File Does Not Exist");
            SaveToJson();
        }

    }


    private void Update()
    {
        //activates UpdateHighScores() from HighScore script
        if (isNewHighScore)
        {
            isNewHighScore = false;
            UpdateHighScores();
        }

        if (isReadyToSave)
        {
            isReadyToSave = false;
            SaveToJson();
        }
    }


    public void SaveToJson()
    {
        SaveData d = new SaveData();

        d.highScores = highScores;
        d.currentPlayerName = currentPlayerName;


        File.WriteAllText(savePath, JsonUtility.ToJson(d));
        Debug.Log("File Saved");

        LoadFromJson();
    }


    public void LoadFromJson()
    {

        SaveData d = JsonUtility.FromJson<SaveData>(File.ReadAllText(savePath));

        if (d != null)
        {
            highScores = d.highScores;
            currentPlayerName = d.currentPlayerName;
        }

        //Debug.Log("highScores.Length= " + highScores.Length);
        //Debug.Log("currentPlayerName= " + currentPlayerName);

        for (int i = 0; i < highScores.Length; i++)
        {
            scoreList[i].highScore = highScores[i].highScore;
            scoreList[i].playerName = highScores[i].playerName;
            //Debug.Log("scoreList[" + i + "] updated: Name: " + scoreList[i].playerName + " Score: " + scoreList[i].highScore);
        }

        if (currentPlayerName == "")
        {
            doesCurrentNameExist = false;
        }
        else
        {
            doesCurrentNameExist = true;
        }

    }

    public void UpdateHighScores()
    {
        //Create temporary list
        List<HighScore> tempList = new List<HighScore>();

        for (int i = 0; i < highScores.Length; i++)
        {
            tempList.Add(highScores[i]);
        }
        HighScore addNewEntry = new HighScore { playerName = currentPlayerName, highScore = currentHighScore.highScore };
        Debug.Log(addNewEntry);

        tempList.Add(addNewEntry);

        //Sort temporary list
        for (int i = 0; i < tempList.Count; i++)
        {
            for (int j = i + 1; j < tempList.Count; j++)
            {
                if (tempList[j].highScore > tempList[i].highScore)
                {
                    HighScore tmp = tempList[i];
                    tempList[i] = tempList[j];
                    tempList[j] = tmp;
                }
            }
        }

        //Remove all ranks above 10
        var tempCount = tempList.Count;
        Debug.Log("saveDataList.Count = " + tempCount);
        for (int i = tempCount; i > 0; i--)
        {
            int j = i - 1;

            if (j >= 10)
            {
                //Debug.Log("j = " + j);
                Debug.Log(tempList[j].playerName + " " + tempList[j].highScore + " has been deleted");
                tempList.RemoveAt(j);
                //tempList.RemoveAt(11);
            }
        }

        //Convert back from list to array
        for (int i = 0; i < highScores.Length; i++)
        {
            highScores[i].highScore = tempList[i].highScore;
            highScores[i].playerName = tempList[i].playerName;

            //Match scoreList to highScores
            scoreList[i].highScore = highScores[i].highScore;
            scoreList[i].playerName = highScores[i].playerName;
        }

        //Save changes
        SaveToJson();
    }



    [Serializable]
    public struct HighScore
    {
        public string playerName;
        public int highScore;
    }

    [Serializable]
    public class SaveData
    {
        public HighScore[] highScores;
        public string currentPlayerName;
    }

    [Serializable]
    public class ScoreList
    {
        public int highScore;
        public string playerName;
        public List<ScoreList> scoreList = new List<ScoreList>();
    }
}