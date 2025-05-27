using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public GameObject inventoryPanel;

    private bool isOpen = false;

    void Start()
    {
        if (inventoryPanel != null)
            inventoryPanel.SetActive(false); // 시작 시 비활성화
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);
    }
}
