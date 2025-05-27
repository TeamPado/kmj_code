using UnityEngine;
using System.Collections.Generic;

public class Chest : MonoBehaviour
{
    [Header("드랍 아이템 ID 목록 (JSON 기반)")]
    public List<string> dropItemIDs; // JSON의 item.id 값들
    public int dropCount = 1;

    private bool isPlayerNearby = false;
    private bool isOpened = false;

    void Start()
    {
        if (RewardUIManager.Instance != null)
            RewardUIManager.Instance.Hide(); // 처음에는 비활성화
    }

    void Update()
    {
        if (isPlayerNearby && !isOpened && Input.GetKeyDown(KeyCode.F))
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        if (isOpened) return;

        isOpened = true;

        List<Item> rewards = new List<Item>();

        // 드랍 ID 셔플 후 일부 선택
        List<string> shuffled = new List<string>(dropItemIDs);
        for (int i = 0; i < shuffled.Count; i++)
        {
            int rand = Random.Range(i, shuffled.Count);
            (shuffled[i], shuffled[rand]) = (shuffled[rand], shuffled[i]);
        }

        for (int i = 0; i < Mathf.Min(dropCount, shuffled.Count); i++)
        {
            ItemData data = ItemDatabase.Instance.GetItemById(shuffled[i]);
            if (data == null) continue;

            ItemType type = (ItemType)System.Enum.Parse(typeof(ItemType), data.itemType);

            if (type == ItemType.Currency)
            {
                // 플레이어 자원에 직접 추가
                PlayerCurrency.Instance.AddGold(data.quantity);
                Debug.Log($"[GOLD] {data.quantity} 골드 획득!");
                continue; // 인벤토리에 넣지 않음
            }

            // 일반 아이템 생성
            Sprite icon = Resources.Load<Sprite>($"Icons/{data.iconName}");
            Item reward = new Item(
                data.itemName,
                icon,
                type,
                data.quantity,
                data.description,
                data.stats,
                data.cost,
                data.requiredLevel
            );

            Inventory.Instance.AddItem(reward);
            rewards.Add(reward); // 리워드 UI에 표시할 목록
            RewardUIManager.Instance.ShowRewards(rewards);
        }

        // 리워드 UI 표시
        RewardUIManager.Instance.ShowRewards(rewards);
        Debug.Log("상자 열림 및 보상 지급 완료");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerNearby = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerNearby = false;
    }
}
