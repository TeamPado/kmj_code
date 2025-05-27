using UnityEngine;

public class InventoryUIController1 : MonoBehaviour
{
    public GameObject inventoryPanel; // 인벤토리 전체 패널
    private bool isOpen = false;

    void Start()
    {
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false); // 시작 시 비활성화
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);
    }

    public void CloseInventory()
    {
        isOpen = false;
        inventoryPanel.SetActive(false);
    }

    public bool IsInventoryOpen()
    {
        return isOpen;
    }
}
