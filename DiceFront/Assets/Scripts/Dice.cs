using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public Sprite[] faces; // size = 6
    Image image;

    [Header("Roll Animation")]
    public float rollDuration = 0.5f;
    public float rollSpeed = 0.05f;

    void Awake()
    {
        image = GetComponent<Image>();
        ShowFace(1);
    }

    public IEnumerator RollTo(int value)
    {
        float t = 0f;

        while (t < rollDuration)
        {
            int randomFace = Random.Range(1, 7);
            ShowFace(randomFace);

            t += rollSpeed;
            yield return new WaitForSeconds(rollSpeed);
        }

        ShowFace(value);
    }

    void ShowFace(int value)
    {
        if (image != null && faces.Length >= value)
        {
            image.sprite = faces[value - 1];
        }
    }
}
