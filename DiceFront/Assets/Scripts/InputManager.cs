using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public Territory selected;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

            if (hit.collider != null)
            {
                Territory t = hit.collider.GetComponent<Territory>();
                if (t != null)
                {
                    Debug.Log("Clicked: " + t.name);
                    SelectTerritory(t);
                }
            }
        }
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

    public void HighlightAttackOptions(bool on)
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
