using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void OnPressPlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
