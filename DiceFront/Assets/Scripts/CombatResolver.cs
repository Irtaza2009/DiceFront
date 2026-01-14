using UnityEngine;
using System.Collections;

public static class CombatResolver
{
    public static void Resolve(Territory attacker, Territory defender)
    {
        GameManager.Instance.StartCoroutine(
            ResolveRoutine(attacker, defender)
        );
    }

    static IEnumerator ResolveRoutine(Territory attacker, Territory defender)
    {
        GameManager.Instance.isCombatActive = true;

        int aDice = attacker.diceCount;
        int dDice = defender.diceCount;

        yield return DiceRollPanel.Instance.RollDice(
            aDice,
            dDice,
            (aRoll, dRoll) =>
            {
                bool attackerWins = aRoll > dRoll;

                if (attackerWins)
                {
                    defender.ownerId = attacker.ownerId;
                    defender.diceCount = aDice - 1;
                    attacker.diceCount = 1;

                    // Play win sound
                    if (AudioManager.Instance != null)
                    {
                        AudioManager.Instance.PlayWinSFX();
                    }
                }
                else
                {
                    attacker.diceCount = 1;

                    // Play lose sound
                    if (AudioManager.Instance != null)
                    {
                        AudioManager.Instance.PlayLoseSFX();
                    }
                }

                attacker.UpdateVisuals();
                defender.UpdateVisuals();

                GameManager.Instance.CheckForWin();
            }
        );

        GameManager.Instance.isCombatActive = false;
    }
}
