using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerEventTrigger : MonoBehaviour
{
    public Tilemap eventTilemap;
    public TileBase SpecialTile;
    public TileBase BattleTile;

    public GameObject interactIcon; // F 표시 아이콘
    public GameObject chestUI;      // 상자 UI (Canvas Overlay)
    public string battleSceneName = "BattleScene"; // 전투 씬 이름

    public Sprite rewardIcon; // 아이템 아이콘 추가

    private Vector3Int lastCheckedPos;
    private bool chestOpened = false;
    private bool triggeredBattle = false;
    private bool isNearChest = false;

    void Start()
    {
        if (interactIcon != null) interactIcon.SetActive(false);
        if (chestUI != null) chestUI.SetActive(false);
    }

    void Update()
    {
        Vector3Int currentCell = eventTilemap.WorldToCell(transform.position);

        if (currentCell == lastCheckedPos) return;
        lastCheckedPos = currentCell;

        TileBase tile = eventTilemap.GetTile(currentCell);

        // 상자 타일 감지
        if (tile == SpecialTile && !chestOpened)
        {
            isNearChest = true;
            interactIcon?.SetActive(true);
        }
        else
        {
            isNearChest = false;
            interactIcon?.SetActive(false);
        }

        // 전투 타일 감지
        if (tile == BattleTile && !triggeredBattle)
        {
            triggeredBattle = true;
            Debug.Log("인카운터 발생");

            GameObject fadeObj = GameObject.Find("FadeManager");
            if (fadeObj != null)
            {
                fadeObj.SetActive(true);
                FadeManager fade = fadeObj.GetComponent<FadeManager>();
                fade?.StartBattleTransition();
            }
            else
            {
                Debug.LogWarning("FadeManager 오브젝트가 없습니다. 씬 직접 전환");
                SceneManager.LoadScene(battleSceneName);
            }
        }
    }

    void LateUpdate()
    {
        if (isNearChest && !chestOpened && Input.GetKeyDown(KeyCode.F))
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        chestOpened = true;
        interactIcon?.SetActive(false);
        chestUI?.SetActive(true);
        Debug.Log("상자 열림");

        
        Item reward = new Item("빨간 포션", rewardIcon, ItemType.Consumable, 1);
        Inventory.Instance.AddItem(reward);
    }

    public void CloseChestUI()
    {
        chestUI?.SetActive(false);
        Debug.Log("상자 UI 닫힘");
    }
}
