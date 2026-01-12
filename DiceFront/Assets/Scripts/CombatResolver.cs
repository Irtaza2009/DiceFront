using UnityEngine;

public static class CombatResolver
{
    public static bool Resolve(Territory attacker, Territory defender)
    {
        int aDice = attacker.diceCount;
        int dDice = defender.diceCount;

        int aRoll = RollSum(aDice);
        int dRoll = RollSum(dDice);

        Debug.Log(
            $"ATTACK {attacker.name} ({aDice}) [{aRoll}] → " +
            $"{defender.name} ({dDice}) [{dRoll}]"
        );

        // Play dice roll sound
        if (AudioManager.Instance != null)
        {
           // removing dice roll sfx for now, will add it back later when I add dice roll panel
           // AudioManager.Instance.PlayDiceRollSFX();
        }

        // Defender wins ties
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

        return attackerWins;
    }

    static int RollSum(int dice)
    {
        int sum = 0;
        for (int i = 0; i < dice; i++)
        {
            sum += Random.Range(1, 7);
        }
        return sum;
    }
}
