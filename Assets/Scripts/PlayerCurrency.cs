using UnityEngine;
using TMPro;

public class PlayerCurrency : MonoBehaviour
{
    public static PlayerCurrency Instance;

    public int gold = 0;
    public TextMeshProUGUI goldText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateGoldUI();
    }

    public void UpdateGoldUI()
    {
        if (goldText != null)
        {
            goldText.text = gold.ToString("N0");
        }
    }
}
