using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Image iconImage;
    public TextMeshProUGUI quantityText;

    public Item currentItem;

    public void SetItem(Item item)
    {
        currentItem = item;

        if (item != null)
        {
            iconImage.sprite = item.itemIcon;
            iconImage.enabled = true;

            if (item.quantity > 1)
            {
                quantityText.text = item.quantity.ToString();
                quantityText.enabled = true;
            }
            else
            {
                quantityText.text = "";
                quantityText.enabled = false;
            }
        }
        else
        {
            ClearSlot();
        }
    }

    public void ClearSlot()
    {
        currentItem = null;
        iconImage.sprite = null;
        iconImage.enabled = false;
        quantityText.text = "";
        quantityText.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (currentItem != null)
            InventoryInfoPanel.Instance.ShowInfo(currentItem);
        else
            InventoryInfoPanel.Instance.ClearInfo();
    }
}
