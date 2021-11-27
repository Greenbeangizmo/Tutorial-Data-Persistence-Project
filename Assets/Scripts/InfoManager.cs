using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InfoManager : MonoBehaviour
{
    public static InfoManager Instance;

    public static string playerName;
    public static int highScore;
    public static string currentPlayerName;

    public static List<SaveData> saveDataList;



    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);



        saveDataList = new List<SaveData>() {
            new SaveData{ highScore = 521, playerName = "GBG" },
            new SaveData{ highScore = 322, playerName = "RBG" },
            new SaveData{ highScore = 232, playerName = "ECC" },
            new SaveData{ highScore = 163, playerName = "EGG" },
            new SaveData{ highScore = 112, playerName = "NVM" },
            new SaveData{ highScore = 107, playerName = "BFG" },
            new SaveData{ highScore = 79, playerName = "EIH" },
            new SaveData{ highScore = 58, playerName = "AHH" },
            new SaveData{ highScore = 6, playerName = "GIZ" },
            new SaveData{ highScore = 19, playerName = "NOT" }

            };

        //
        highScore = 123;
        playerName = "Testing";

        currentPlayerName = "TestName";
        //

        LoadFromJson();

        

    }


    public static void SaveToJson()
    {
        //Create file path
        string path = Application.persistentDataPath + "/savefile.json";

        //Create save data
        SaveData d = new SaveData();

        d.currentPlayerName = currentPlayerName;
        d.saveDataList = saveDataList;

        string saveFile = JsonUtility.ToJson(d);


        // Is this needed?  File is created
        //if (!File.Exists(path))
        //{
        //    File.Create(path);
        //}

        StreamWriter writer = new StreamWriter(path);
        writer.Write(saveFile);
        writer.Close();

    }

    public static void LoadFromJson()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        Debug.Log(path);

        if (File.Exists(path))
        {
            StreamReader r = new StreamReader(path);
            string file = r.ReadToEnd();

            SaveData d = JsonUtility.FromJson<SaveData>(file);

            currentPlayerName = d.currentPlayerName;
            saveDataList = d.saveDataList;

            r.Close();
        }
        else
        {
            Debug.Log("No save data exists!");
        }
            
        
    }


    public static void AddHighscoreEntry()
    {

        SaveData addNewEntry = new SaveData { highScore = highScore, playerName = playerName };
        saveDataList.Add(addNewEntry);


        //Sort List
        for (int i = 0; i < saveDataList.Count; i++)
        {
            for (int j = i + 1; j < saveDataList.Count; j++)
            {
                if (saveDataList[j].highScore > saveDataList[i].highScore)
                {
                    SaveData tmp = saveDataList[i];
                    saveDataList[i] = saveDataList[j];
                    saveDataList[j] = tmp;
                }
            }
        }


        //Remove all ranks above 10
        var tempCount = saveDataList.Count;
        Debug.Log("saveDataList.Count = " + tempCount);
        for (int i = tempCount; i > 0; i--)
        {
            int j = i - 1;

            if (j >= 10)
            {
                //Debug.Log("j = " + j);
                Debug.Log(saveDataList[j].playerName + " " + saveDataList[j].highScore + " has been deleted");
                saveDataList.RemoveAt(j);
            }
        }

        //Displays list in console
        for (int i = 0; i < saveDataList.Count; i++)
        {
            var j = i + 1;
            Debug.Log("Rank:" + j + ": " + saveDataList[i].playerName + " " + saveDataList[i].highScore);
        }


        SaveToJson();


    }





}


[System.Serializable]
public class SaveData
{
    public string currentPlayerName;

    public int highScore;
    public string playerName;

    public List<SaveData> saveDataList;
}
