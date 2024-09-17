using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text playerNameText;

    void Start()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "Player");
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        playerNameText.text = $"Best Score: {playerName} : {bestScore}";
    }
}


