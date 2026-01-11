using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public Territory selected;
    Coroutine blinkCoroutine;
    List<Territory> highlightedTerritories = new List<Territory>();

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

        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }

        highlightedTerritories.Clear();

        foreach (var neighbor in selected.neighbors)
        {
            if (neighbor.ownerId != selected.ownerId)
            {
                if (!on)
                {
                    neighbor.GetComponent<SpriteRenderer>().color = neighbor.GetBaseColor();
                }
                else
                {
                    highlightedTerritories.Add(neighbor);
                }
            }
        }

        if (on && highlightedTerritories.Count > 0)
        {
            blinkCoroutine = StartCoroutine(BlinkTerritories());
        }
    }

    IEnumerator BlinkTerritories()
    {
        Color blinkColor = selected.ownerId == 0 ? Colors.RedBlink : Colors.BlueBlink;

        while (true)
        {
            foreach (var territory in highlightedTerritories)
            {
                if (territory != null)
                {
                    territory.GetComponent<SpriteRenderer>().color = blinkColor;
                }
            }

            yield return new WaitForSeconds(0.5f);

            foreach (var territory in highlightedTerritories)
            {
                if (territory != null)
                {
                    territory.GetComponent<SpriteRenderer>().color = territory.GetBaseColor();
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

}
