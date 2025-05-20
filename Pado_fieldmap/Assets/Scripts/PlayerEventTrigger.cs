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
            Debug.Log($"이벤트 발생! 타일 이름: {currentTile.name}");
            TriggerEvent(currentTile);
        }
    }

    void TriggerEvent(TileBase tile)
    {
        // 타일 종류에 따라 다르게 처리 가능
        if (tile.name == "BattleTile")
        {
            Debug.Log("전투 시작!");
        }
        else if (tile.name == "SpecialTile")
        {
            Debug.Log("상자 발견!");
        }
        else
        {
            Debug.Log("기타 이벤트");
        }
    }
}
