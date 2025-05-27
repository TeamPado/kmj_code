[System.Serializable]
public class ItemData
{
    public string id;
    public string itemName;
    public string itemType;
    public int quantity;
    public string description;
    public string stats;
    public int cost;
    public int requiredLevel;
    public string iconName;
}


[System.Serializable]
public class ItemDataList
{
    public ItemData[] items;
}
