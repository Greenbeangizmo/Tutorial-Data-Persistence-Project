using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTextManager : MonoBehaviour
{
    public Text highScore;

    // Start is called before the first frame update
    void Start()
    {
        highScore.text = "Highscore: " + InfoManager.scoreList[0].playerName + " : " + InfoManager.scoreList[0].highScore;
    }

}
