using UnityEngine;
using UnityEngine.UI;

public class TeamSlot : MonoBehaviour
{
    public Image characterImage;
    public bool isOccupied = false;

    public void SetCharacter(Sprite characterSprite)
    {
        characterImage.sprite = characterSprite;
        characterImage.enabled = true;
        isOccupied = true;
    }

    public void ClearSlot()
    {
        characterImage.sprite = null;
        characterImage.enabled = false;
        isOccupied = false;
    }
}
