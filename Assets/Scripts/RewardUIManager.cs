using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
/* 동적으로 구현하게 될 때 사용할 코드
public class RewardUIManager : MonoBehaviour
{
    public static RewardUIManager Instance;

    public GameObject rewardPanel;
    public Transform rewardSlotContainer;
    public GameObject rewardSlotPrefab;

    private void Awake()
    {
        Instance = this;
        rewardPanel.SetActive(false);
    }

    public void ShowRewards(List<Item> rewards)
    {
        // 기존 슬롯 제거
        foreach (Transform child in rewardSlotContainer)
            Destroy(child.gameObject);

        // 슬롯 생성
        foreach (Item item in rewards)
        {
            GameObject slot = Instantiate(rewardSlotPrefab, rewardSlotContainer);
            slot.GetComponent<Image>().sprite = item.itemIcon;
        }

        rewardPanel.SetActive(true);
    }

    public void CloseReward()
    {
        rewardPanel.SetActive(false);
    }
}*/
//현재 사용하는 코드는 슬롯을 정적으로 관리
public class RewardUIManager : MonoBehaviour
{
    public static RewardUIManager Instance;

    public GameObject rewardPanel; // ChestCanvas > Panel
    public List<Image> rewardSlots; // Slot1~Slot4의 Image 연결

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void Hide()
    {
        Debug.Log("리워드 창 닫기 실행됨");
        if (rewardPanel != null)
            rewardPanel.SetActive(false);
    }

    public void ShowRewards(List<Item> rewards)
    {
        rewardPanel.SetActive(true);

        // 슬롯 초기화
        foreach (var img in rewardSlots)
        {
            img.enabled = false;
        }

        // 보상 채우기
        for (int i = 0; i < Mathf.Min(rewards.Count, rewardSlots.Count); i++)
        {
            rewardSlots[i].sprite = rewards[i].itemIcon;
            rewardSlots[i].enabled = true;
        }
    }
}

