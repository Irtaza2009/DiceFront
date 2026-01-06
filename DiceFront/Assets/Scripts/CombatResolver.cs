using UnityEngine;

public static class CombatResolver
{
    public static bool Resolve(Territory attacker, Territory defender)
    {
        int aRoll = RollSum(attacker.diceCount);
        int dRoll = RollSum(defender.diceCount);
        Debug.Log(
            $"ATTACK: {attacker.name} ({attacker.diceCount}) → " +
            $"{defender.name} ({defender.diceCount})"
        );


        if (aRoll > dRoll)
        {
            defender.ownerId = attacker.ownerId;
            defender.diceCount = attacker.diceCount - 1;
            attacker.diceCount = 1;
            defender.UpdateVisuals();
            attacker.UpdateVisuals();
            return true;
        }
        else
        {
            attacker.diceCount = Mathf.Max(1, attacker.diceCount - 2);
            attacker.UpdateVisuals();
            return false;
        }
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
