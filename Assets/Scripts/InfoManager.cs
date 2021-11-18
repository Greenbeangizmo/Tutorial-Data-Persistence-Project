using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InfoManager : MonoBehaviour
{
    public static InfoManager Instance;

    public string playerName;
    public string highScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public string highScore;
    }


    //Find a way to save both the name and high score to one json save?



    //public void SaveName()
    //{
    //    SaveData data = new SaveData();
    //    data.playerName = playerName;

    //    string json = JsonUtility.ToJson(data);

    //    File.WriteAllText(Application.persistentDataPath + "/savename.json", json);
    //}

    //public void LoadName()
    //{
    //    string path = Application.persistentDataPath + "/savename.json";
    //    if (File.Exists(path))
    //    {
    //        string json = File.ReadAllText(path);
    //        SaveData data = JsonUtility.FromJson<SaveData>(json);

    //        playerName = data.playerName;
    //    }
    //}

    //public void SaveHighScore()
    //{
    //    SaveData data = new SaveData();
    //    data.highScore = highScore;

    //    string json = JsonUtility.ToJson(data);

    //    File.WriteAllText(Application.persistentDataPath + "")
    //}
}
