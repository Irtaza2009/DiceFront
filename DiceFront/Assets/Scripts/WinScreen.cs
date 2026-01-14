using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public static WinScreen Instance;

    public GameObject root;
    public TextMeshProUGUI winText;

    void Awake()
    {
        Instance = this;
        root.SetActive(false);
    }

    public void ShowWinner(int ownerId)
    {
        root.SetActive(true);
        Time.timeScale = 0f;

        if (ownerId == 0)
        {
            winText.text = "Player 1 Wins!";
            winText.color = Colors.Blue;
        }
        else if (ownerId == 1)
        {
            if (AIController.Instance != null)
            {
                winText.text = "AI Wins!";
            }
            else
            {
                winText.text = "Player 2 Wins!";
            }
            winText.color = Colors.Red;
        }
        else
        {
            winText.text = "Draw";
            winText.color = Color.white;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
