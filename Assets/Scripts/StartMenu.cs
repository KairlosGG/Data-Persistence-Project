using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public InputField playerNameInput;

    public void StartGame()
    {
        PlayerPrefs.SetString("PlayerName", playerNameInput.text);
        SceneManager.LoadScene("Main");
    }
}

