using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerTileEventTrigger : MonoBehaviour
{
    public Tilemap eventTilemap;
    public TileBase SpecialTile;
    public TileBase BattleTile;

    public GameObject interactIcon; // F 표시 아이콘
    public GameObject chestUI;      // 상자 UI (Canvas Overlay에 연결)
    public string battleSceneName = "BattleScene"; // 전투 씬 이름을 Inspector에서 입력

    private Vector3Int lastCheckedPos;
    private bool chestOpened = false;
    private bool triggeredBattle = false;
    private bool isNearChest = false;

    void Start()
    {
        interactIcon?.SetActive(false);
        chestUI?.SetActive(false);
    }

    void Update()
    {
        Vector3Int currentCell = eventTilemap.WorldToCell(transform.position);

        if (currentCell == lastCheckedPos) return;
        lastCheckedPos = currentCell;

        TileBase tile = eventTilemap.GetTile(currentCell);

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

        if (tile == BattleTile && !triggeredBattle)
        {
            triggeredBattle = true;
            Debug.Log("인카운터 발생");
            GameObject fadeObj = GameObject.Find("FadeManager");
            if (fadeObj != null)
            {
                fadeObj.SetActive(true); // 비활성 상태였다면 이걸로 활성화!
                fadeObj.GetComponent<FadeManager>().StartBattleTransition();
            }
            else
            {
                Debug.LogWarning("FadeManager 오브젝트를 찾을 수 없습니다.");
            }
            // 여기에서 인스펙터에서 설정한 씬으로 이동
            SceneManager.LoadScene(battleSceneName);
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
    }

    public void CloseChestUI()
    {
        chestUI?.SetActive(false);
        Debug.Log("상자 UI 닫힘");
    }
}
