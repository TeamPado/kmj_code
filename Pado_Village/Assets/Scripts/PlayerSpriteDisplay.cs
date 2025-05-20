using UnityEngine;
using UnityEngine.UI;

public class PlayerSpriteDisplay : MonoBehaviour
{
    public GameObject player; // Knight_0
    public Image targetUIImage; // UI Image 오브젝트

    void Start()
    {
        if (player != null && targetUIImage != null)
        {
            SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                targetUIImage.sprite = sr.sprite;
                targetUIImage.rectTransform.sizeDelta = new Vector2(700, 100);
            }
        }
    }
}
