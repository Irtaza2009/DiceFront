using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void OnPress2PlayerButton()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySelectSFX();
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void OnPressAIButton()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySelectSFX();
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
