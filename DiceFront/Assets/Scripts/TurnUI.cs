using TMPro;
using UnityEngine;

public class TurnUI : MonoBehaviour
{
    public static TurnUI Instance;

    [Header("Turn")]
    public TextMeshProUGUI turnText;

    [Header("Scores")]
    public TextMeshProUGUI player0ScoreText;
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI neutralScoreText;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        UpdateScores();   
    }

    public void UpdateTurn(int playerId)
    {
        // Turn text
        turnText.text = $"Player {playerId + 1} Turn";
        turnText.color = playerId == 0 ? Color.blue : Color.red;
    }

    void UpdateScores()
    {
        if (GameManager.Instance == null) return;

        int p0Score = GameManager.Instance.GetScore(0);
        int p1Score = GameManager.Instance.GetScore(1);
        int neutralScore = GameManager.Instance.GetScore(-1);

        player0ScoreText.text = $"Player 1: {p0Score}";
        player1ScoreText.text = $"Player 2: {p1Score}";
        neutralScoreText.text = $"Neutral: {neutralScore}";

        player0ScoreText.color = Color.blue;
        player1ScoreText.color = Color.red;
        neutralScoreText.color = Color.gray;
    }
}
