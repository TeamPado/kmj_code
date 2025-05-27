using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<Item> items = new List<Item>();
    public InventorySlot[] slots;  // ÀÎ½ºÆåÅÍ¿¡¼­ ½½·Ô ¿¬°á

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        if (newItem.itemType == ItemType.Consumable)
        {
            Item existing = items.Find(i => i.itemName == newItem.itemName && i.itemType == ItemType.Consumable);
            if (existing != null)
            {
                existing.quantity += newItem.quantity;
                UpdateUI();
                return;
            }
        }

        items.Add(newItem);
        UpdateUI();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
            {
                Debug.Log($"[UpdateUI] Slot {i} ¡æ {items[i].itemName}");
                slots[i].SetItem(items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
