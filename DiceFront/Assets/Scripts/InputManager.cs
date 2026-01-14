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
        if (GameManager.Instance.isCombatActive)
            return;

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

                // Play select sound
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySelectSFX();
                }
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

        Color blinkColor = selected.ownerId == 0 ? Colors.RedBlink : Colors.BlueBlink;

        foreach (var neighbor in selected.neighbors)
        {
            if (neighbor.ownerId != selected.ownerId)
            {
                neighbor.GetComponent<SpriteRenderer>().color = on ? blinkColor : neighbor.GetBaseColor();
            }
        }
    }

}
