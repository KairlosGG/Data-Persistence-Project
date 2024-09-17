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
    public Text BestScoreText; // A�adido para mostrar el mejor puntaje
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    private int m_BestScore; // A�adido para almacenar el mejor puntaje
    private string m_BestPlayer; // A�adido para almacenar el nombre del mejor jugador

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        LoadBestScore();
        UpdateBestScoreText();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";

        if (m_Points > m_BestScore)
        {
            m_BestScore = m_Points;
            m_BestPlayer = PlayerPrefs.GetString("PlayerName", "Player");
            SaveBestScore();
            UpdateBestScoreText();
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", m_BestScore);
        PlayerPrefs.SetString("BestPlayer", m_BestPlayer);
        PlayerPrefs.Save();
    }

    void LoadBestScore()
    {
        m_BestScore = PlayerPrefs.GetInt("BestScore", 0);
        m_BestPlayer = PlayerPrefs.GetString("BestPlayer", "None");
    }

    void UpdateBestScoreText()
    {
        BestScoreText.text = $"Best Score: {m_BestPlayer} : {m_BestScore}";
    }
}
