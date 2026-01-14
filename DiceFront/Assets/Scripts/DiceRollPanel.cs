using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceRollPanel : MonoBehaviour
{
    public static DiceRollPanel Instance;

    [Header("Setup")]
    public Transform attackerArea;
    public Transform defenderArea;
    public Dice dicePrefab;

    [Header("UI")]
    public TextMeshProUGUI attackerSumText;
    public TextMeshProUGUI defenderSumText;

    void Awake()
    {
        Instance = this;
    }

    public IEnumerator RollDice(
        int attackerDice,
        int defenderDice,
        System.Action<int, int> onFinished
    )
    {
        ClearPanel();

        List<Dice> attackerDiceObjs = SpawnDice(attackerDice, attackerArea);
        List<Dice> defenderDiceObjs = SpawnDice(defenderDice, defenderArea);

        int aSum = 0;
        int dSum = 0;

        List<int> attackerValues = new();
        List<int> defenderValues = new();

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayDiceRollSFX();
        }


        // Generate random values
        for (int i = 0; i < attackerDice; i++)
        {
            int v = Random.Range(1, 7);
            attackerValues.Add(v);
            aSum += v;
        }

        for (int i = 0; i < defenderDice; i++)
        {
            int v = Random.Range(1, 7);
            defenderValues.Add(v);
            dSum += v;
        }

        List<IEnumerator> rolls = new();

        for (int i = 0; i < attackerDiceObjs.Count; i++)
        {
            rolls.Add(attackerDiceObjs[i].RollTo(attackerValues[i]));
        }

        for (int i = 0; i < defenderDiceObjs.Count; i++)
        {
            rolls.Add(defenderDiceObjs[i].RollTo(defenderValues[i]));
        }

        // Run all dice simultaneously
        foreach (var roll in rolls)
        {
            StartCoroutine(roll);
        }

        // Wait for longest roll
        yield return new WaitForSeconds(dicePrefab.rollDuration + 0.1f);


        attackerSumText.text = aSum.ToString();
        attackerSumText.color = GameManager.Instance.currentPlayer == 0 ? Colors.Blue : Colors.Red;
        defenderSumText.text = dSum.ToString();
        defenderSumText.color = GameManager.Instance.currentPlayer == 1 ? Colors.Blue : Colors.Red;

        yield return new WaitForSeconds(0.6f);

        yield return PlayResultAnimation(aSum > dSum);

        onFinished?.Invoke(aSum, dSum);
    }

    List<Dice> SpawnDice(int count, Transform parent)
    {
        List<Dice> list = new();

        for (int i = 0; i < count; i++)
        {
            Dice d = Instantiate(dicePrefab, parent);
            list.Add(d);
        }

        return list;
    }

    void ClearPanel()
    {
        attackerSumText.text = "";
        defenderSumText.text = "";

        foreach (Transform t in attackerArea)
            Destroy(t.gameObject);
        foreach (Transform t in defenderArea)
            Destroy(t.gameObject);
    }

    IEnumerator PlayResultAnimation(bool attackerWins)
    {
        // TODO: fade losing side, scale winning side

        yield return new WaitForSeconds(0.005f);
    }
}
