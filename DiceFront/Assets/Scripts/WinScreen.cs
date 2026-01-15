using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public static WinScreen Instance;

    public GameObject root;
    public TextMeshProUGUI winText;

    public GameObject pauseMenu;

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
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySelectSFX();
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySelectSFX();
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySelectSFX();
        }
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySelectSFX();
        }
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
}
