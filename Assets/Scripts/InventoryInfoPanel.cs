using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryInfoPanel : MonoBehaviour
{
    public static InventoryInfoPanel Instance;

    [Header("Text Fields")]
    public Image itemIconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI flavourText;
    public TextMeshProUGUI statsText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI levelText;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowInfo(Item item)
    {
        if (itemIconImage != null)
        {
            itemIconImage.sprite = item.itemIcon;
            itemIconImage.enabled = true;
        }
        nameText.text = item.itemName;
        flavourText.text = item.flavourText;
        statsText.text = item.stats;
        costText.text = $" {item.cost}";
        levelText.text = $"Required Level: {item.requiredLevel}";
    }

    public void ClearInfo()
    {
        if (itemIconImage != null)
        {
            itemIconImage.sprite = null;
            itemIconImage.enabled = false;
        }
        nameText.text = "";
        flavourText.text = "";
        statsText.text = "";
        costText.text = "";
        levelText.text = "";
    }
}
