using NUnit.Framework.Interfaces;
using UnityEngine;

public class InventoryDataLoader : MonoBehaviour
{
    void Start()
    {
        LoadItemsFromJson();
    }

    public void LoadItemsFromJson()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("items"); // 확장자 .json 생략
        if (jsonFile == null)
        {
            Debug.LogError("Items.json not found in Resources folder!");
            return;
        }

        ItemDataList dataList = JsonUtility.FromJson<ItemDataList>(jsonFile.text);
        foreach (ItemData data in dataList.items)
        {
            Sprite icon = Resources.Load<Sprite>($"Icons/{data.iconName}"); // bowSprite 등
            Item item = new Item(
                data.itemName,
                icon,
                (ItemType)System.Enum.Parse(typeof(ItemType), data.itemType),
                data.quantity,
                data.description,
                data.stats,
                data.cost,
                data.requiredLevel
            );
            Inventory.Instance.AddItem(item);
        }
    }
}
