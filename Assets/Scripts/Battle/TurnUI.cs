using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnUI : MonoBehaviour
{
    public GameObject turnSlotPrefab;
    public Transform turnPanel;

    public void UpdateTurnOrderUI(List<Unit> turnOrder)
    {
        foreach (Transform child in turnPanel)
            Destroy(child.gameObject);

        foreach (Unit unit in turnOrder)
        {
            GameObject slot = Instantiate(turnSlotPrefab, turnPanel);
            slot.GetComponentInChildren<Text>().text = unit.unitName;
        }
    }
}
