using UnityEngine;

public class InnNPC : MonoBehaviour
{
    public GameObject overlayUI; // 팀 구성 및 스탯 오버레이
    public GameObject eIcon;     // E 아이콘

    private bool isPlayerNearby = false;
    private StatManager statManager;

    void Start()
    {
        eIcon.SetActive(false); // 기본적으로 숨김
        overlayUI.SetActive(false); // 처음엔 꺼두기

        // StatManager 연결
        statManager = overlayUI.GetComponent<StatManager>();
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ToggleOverlay();
        }

        if (overlayUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            overlayUI.SetActive(false);
            Debug.Log("장비창 닫힘 (ESC)");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            eIcon.SetActive(true); // E 아이콘 표시
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            eIcon.SetActive(false); // E 아이콘 숨김

            if (overlayUI.activeSelf)
            {
                overlayUI.SetActive(false); // UI 열려있을 때만 닫음
                Debug.Log("멀어져서 장비창 닫힘");
            }
        }
    }

    private void ToggleOverlay()
    {
        bool nextState = !overlayUI.activeSelf;
        overlayUI.SetActive(nextState);

        if (nextState && statManager != null)
        {
            statManager.UpdateUI(); // UI 열릴 때 최신 정보로 갱신
            Debug.Log("장비창 열림");
        }
    }
}
