using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    Territory selected;

    void Awake()
    {
        Instance = this;
    }

    public void SelectTerritory(Territory t)
    {
        if (selected == null)
        {
            if (t.ownerId == GameManager.Instance.currentPlayer)
            {
                selected = t;
                selected.SetSelected(true);
                HighlightAttackOptions(true);
            }
        }
        else
        {
            selected.SetSelected(false);
            HighlightAttackOptions(false);

            if (selected.CanAttack(t))
            {
                CombatResolver.Resolve(selected, t);
            }

            selected = null;
        }
    }

    void HighlightAttackOptions(bool on)
    {
        if (selected == null) return;

        foreach (var neighbor in selected.neighbors)
        {
            if (neighbor.ownerId != selected.ownerId)
            {
                neighbor.GetComponent<SpriteRenderer>().color =
                    on ? Color.magenta : neighbor.GetBaseColor();
            }
        }
    }

}
