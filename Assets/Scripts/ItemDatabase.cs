using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;

    public TextAsset jsonFile;
    private Dictionary<string, ItemData> itemDict;

    [System.Serializable]
    private class ItemListWrapper
    {
        public List<ItemData> items;
    }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        ItemListWrapper wrapper = JsonUtility.FromJson<ItemListWrapper>(jsonFile.text);
        itemDict = new Dictionary<string, ItemData>();

        foreach (ItemData item in wrapper.items)
        {
            itemDict[item.id] = item;
        }
    }

    public ItemData GetItemById(string id)
    {
        if (itemDict.TryGetValue(id, out ItemData data))
        {
            return data;
        }
        Debug.LogWarning($"아이템 ID {id} 를 찾을 수 없습니다.");
        return null;
    }
}
