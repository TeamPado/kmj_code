using UnityEngine;

public class Item
{
    public string itemName;
    public Sprite itemIcon;
    public ItemType itemType;
    public int quantity;

    // 추가 정보
    public string flavourText;
    public string stats;
    public int cost;
    public int requiredLevel;

    public Item(string name, Sprite icon, ItemType type, int qty = 1,
                string flavour = "", string stat = "", int price = 0, int level = 0)
    {
        itemName = name;
        itemIcon = icon;
        itemType = type;
        quantity = qty;
        flavourText = flavour;
        stats = stat;
        cost = price;
        requiredLevel = level;
    }
}
