using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    public Button highScoreButton;
    public Button startButton;

    public Text helloPlayer;
    public Text playerConfirmation;

    public GameObject highScoreContainer;

    // Start is called before the first frame update
    void Start()
    {
        helloPlayer.text = "Hello " + InfoManager.currentPlayerName;
        if (InfoManager.doesCurrentNameExist)
        {
            playerConfirmation.text = "Not you?  Please enter your name.";
            startButton.gameObject.SetActive(true);
        }
        else
        {
            playerConfirmation.text = "Please enter your name.";
        }
    }

    public void OpenHighScoreScreen()
    {
        highScoreContainer.SetActive(true);
        highScoreButton.gameObject.SetActive(false);
    }

    public void CloseHighScoreScreen()
    {
        highScoreContainer.SetActive(false);
        highScoreButton.gameObject.SetActive(true);
    }

    public void NameEntered()
    {
        helloPlayer.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
        helloPlayer.text = "Hello " + InfoManager.currentPlayerName;
        playerConfirmation.text = "Not you?  Please enter your name.";
    }

    public void StartGame()
    {
        SceneManager.LoadScene("main");
    }

}
