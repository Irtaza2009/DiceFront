using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public static AIController Instance;

    [Header("AI Tuning")]
    public float thinkDelay = 0.5f;
    public float betweenAttacksDelay = 0.7f;
    [Range(0f, 1f)]
    public float riskySkipChance = 0.5f;

    const int AI_ID = 1;

    void Awake()
    {
        Instance = this;
    }

    public void StartAITurn()
    {
        StartCoroutine(AITurnRoutine());
    }

    IEnumerator AITurnRoutine()
    {
        yield return new WaitForSeconds(thinkDelay);

        while (true)
        {
            var attack = FindBestAttack();

            if (attack.from == null)
                break;

            // Risk check
            bool risky = attack.from.diceCount <= attack.to.diceCount + 1;
            if (risky && Random.value < riskySkipChance)
                break;

            // VISUAL SELECT
            InputManager.Instance.SelectTerritory(attack.from);
            yield return new WaitForSeconds(0.5f);

            // ATTACK
            CombatResolver.Resolve(attack.from, attack.to);
            yield return new WaitForSeconds(betweenAttacksDelay);

            // Clear selection properly
            if (InputManager.Instance.selected != null)
            {
                InputManager.Instance.selected.SetSelected(false);
                InputManager.Instance.HighlightAttackOptions(false);
            }
            InputManager.Instance.selected = null;
        }

        GameManager.Instance.EndTurn();
    }

    (Territory from, Territory to) FindBestAttack()
    {
        Territory bestFrom = null;
        Territory bestTo = null;
        int bestScore = int.MinValue;

        foreach (var t in GameManager.Instance.territories)
        {
            if (t.ownerId != AI_ID || t.diceCount <= 1)
                continue;

            foreach (var n in t.neighbors)
            {
                if (n.ownerId == AI_ID)
                    continue;

                if (t.diceCount <= n.diceCount)
                    continue; // MUST have more dice

                int score = (t.diceCount - n.diceCount);

                if (score > bestScore)
                {
                    bestScore = score;
                    bestFrom = t;
                    bestTo = n;
                }
            }
        }

        return (bestFrom, bestTo);
    }
}
