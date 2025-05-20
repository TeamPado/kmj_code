using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class PlayerEventTrigger : MonoBehaviour
{
    public Tilemap eventTilemap;
    public List<TileBase> eventTiles;

    private Vector3Int lastCheckedPos;

    void Update()
    {
        Vector3Int currentCell = eventTilemap.WorldToCell(transform.position);

        if (currentCell == lastCheckedPos) return;
        lastCheckedPos = currentCell;

        TileBase currentTile = eventTilemap.GetTile(currentCell);
        if (eventTiles.Contains(currentTile))
        {
            Debug.Log($"�̺�Ʈ �߻�! Ÿ�� �̸�: {currentTile.name}");
            TriggerEvent(currentTile);
        }
    }

    void TriggerEvent(TileBase tile)
    {
        // Ÿ�� ������ ���� �ٸ��� ó�� ����
        if (tile.name == "BattleTile")
        {
            Debug.Log("���� ����!");
        }
        else if (tile.name == "SpecialTile")
        {
            Debug.Log("���� �߰�!");
        }
        else
        {
            Debug.Log("��Ÿ �̺�Ʈ");
        }
    }
}
