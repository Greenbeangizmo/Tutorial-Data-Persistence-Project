using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;

    public GameObject spaceToStartText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    private bool areBlocksRespawned = false;

    private GameObject[] numberOfBricks;
    private int bricksInScene;


    // Start is called before the first frame update
    void Start()
    {
        SpawnBlocks();
    }

    private void SpawnBlocks()
    {
        areBlocksRespawned = false;

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.tag = "Brick";
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }


    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                spaceToStartText.gameObject.SetActive(false);
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                SceneManager.LoadScene("menu");
            }
        }



        if (!m_GameOver)
        {
            numberOfBricks = GameObject.FindGameObjectsWithTag("Brick");
            bricksInScene = numberOfBricks.Length;
            //Debug.Log("There are " + bricksInScene + " bricks in the scene");

            if (bricksInScene == 0 && areBlocksRespawned == false && m_Points != 0)
            {
                areBlocksRespawned = true;
                SpawnBlocks();
            }
        }
        
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        InfoManager.currentHighScore.highScore = m_Points;
        //InfoManager.currentScore = m_Points;
        InfoManager.isNewHighScore = true;
    }
}
