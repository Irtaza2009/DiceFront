using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Territory : MonoBehaviour
{
    public int ownerId = -1;
    public int diceCount = 1;
    public const int MAX_DICE = 8;

    public List<Territory> neighbors = new List<Territory>();

    SpriteRenderer spriteRenderer;
    Color baseColor;
    public TextMeshProUGUI diceText;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseColor = 
            ownerId == 0 ? Color.blue :
            ownerId == 1 ? Color.red :
            Color.gray;
        UpdateVisuals();
    }

    void Update()
    {
        if (diceText != null)
        {
            diceText.text = diceCount.ToString();
        }
    }

    public void UpdateVisuals()
    {
        baseColor = GetBaseColor();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = baseColor;
        }
        if (diceText != null)
        {
            diceText.text = diceCount.ToString();
        }
    }

    public bool CanAttack(Territory target)
    {
        return neighbors.Contains(target) && diceCount > 1;
    }

    public void SetSelected(bool selected)
    {
        spriteRenderer.color = selected ? Color.yellow : baseColor;
    }

    public Color GetBaseColor()
    {
        return ownerId == 0 ? Color.blue :
            ownerId == 1 ? Color.red :
            Color.gray;
    }


}
