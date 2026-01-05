using TMPro;
using UnityEngine;

public class TurnUI : MonoBehaviour
{
    public static TurnUI Instance;
    public TextMeshProUGUI turnText;

    void Awake()
    {
        Instance = this;
    }

    public void UpdateTurn(int playerId)
    {
        turnText.text = $"Player {playerId + 1} Turn";
    }
}
