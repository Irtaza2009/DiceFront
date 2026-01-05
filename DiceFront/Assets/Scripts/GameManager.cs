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
        TurnUI.Instance?.UpdateTurn(currentPlayer);
    }

    
    public void EndTurn()
    {
        GrantDice(currentPlayer);
        currentPlayer = (currentPlayer + 1) % playerCount;
        TurnUI.Instance.UpdateTurn(currentPlayer);
    }

    void GrantDice(int playerId)
    {
        int owned = 0;
        foreach (var territory in territories)
        {
            if (territory.ownerId == playerId)
            {
                owned++;
            }
        }
        int diceToGive = Mathf.Max(1, owned / 2);

        // random placement for now
        List<Territory> ownedTerritories = territories.FindAll(t => t.ownerId == playerId);
        for (int i = 0; i < diceToGive; i++)
        {
            var t = ownedTerritories[Random.Range(0, ownedTerritories.Count)];
            if (t.diceCount < Territory.MAX_DICE)
            {
                t.diceCount++;
            }
        }
    }
}
