using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentPlayer = 0;
    public int playerCount = 2;

    public List<Territory> territories = new List<Territory>();
    
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        territories = new List<Territory>(FindObjectsOfType<Territory>());
        Debug.Log($"Registered {territories.Count} territories.");
        
        if (TurnUI.Instance != null)
        {
            TurnUI.Instance.UpdateTurn(currentPlayer);
        }
        if (InputManager.Instance != null)
        {
            InputManager.Instance.SelectTerritory(null);
        }
    }


    
    public void EndTurn()
    {
        int endingPlayer = currentPlayer;
        GrantDice(endingPlayer);
        currentPlayer = (currentPlayer + 1) % playerCount;
        TurnUI.Instance.UpdateTurn(currentPlayer);
        if (InputManager.Instance.selected != null)
        {
            InputManager.Instance.selected.SetSelected(false);
        }
        InputManager.Instance.HighlightAttackOptions(false);
        InputManager.Instance.selected = null;

        if (currentPlayer == 1 && AIController.Instance != null)
        {
            AIController.Instance.StartAITurn();
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySelectSFX();
        }
    }

    void GrantDice(int playerId)
    {
        List<Territory> ownedTerritories = territories.FindAll(t => t.ownerId == playerId);

        if (ownedTerritories.Count == 0)
        {
            Debug.Log($"Player {playerId} owns no territories, skipping dice grant.");
            return;
        }
        int diceToGive = Mathf.Max(1, ownedTerritories.Count / 2);

        for (int i = 0; i < diceToGive; i++)
        {
            var candidates = ownedTerritories.FindAll(t => t.diceCount < Territory.MAX_DICE);
            if (candidates.Count == 0) break;

            var t = candidates[Random.Range(0, candidates.Count)];
            t.diceCount++;
            t.UpdateVisuals();
        }
    }

    public int GetScore(int playerId)
    {
        int score = 0;
        foreach (var t in territories)
        {
            if (t.ownerId == playerId)
            {
                score += Mathf.RoundToInt(Mathf.Pow(t.diceCount, 0.9f));
            }
        }
        return score;
    }

}
