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
        baseColor = GetBaseColor();
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
        if (!selected)
        {
            spriteRenderer.color = baseColor;
            return;
        }
        spriteRenderer.color = ownerId == 0
            ? Colors.BlueSelected
            : ownerId == 1
                ? Colors.RedSelected
                : Colors.GreySelected;
    }

    public Color GetBaseColor()
    {
        return ownerId switch
        {
            0 => Colors.Blue,
            1 => Colors.Red,
            _ => Colors.Grey
        };
    }



}
