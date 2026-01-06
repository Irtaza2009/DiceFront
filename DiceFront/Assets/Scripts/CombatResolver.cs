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

        // Defender wins ties
        if (aRoll > dRoll)
        {
            defender.ownerId = attacker.ownerId;

            defender.diceCount = aDice - 1;
            attacker.diceCount = 1;
        }
        else
        {
            attacker.diceCount = 1;
        }

        attacker.UpdateVisuals();
        defender.UpdateVisuals();

        return aRoll > dRoll;
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
