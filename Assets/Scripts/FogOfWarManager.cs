using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FogOfWarManager : MonoBehaviour
{
    [Header("타일맵 연결")]
    public Tilemap fogTilemap;           // 어두운 안개 타일맵
    public Tilemap exploredTilemap;      // 밝힌 흔적 타일맵

    [Header("타일 설정")]
    public TileBase fogTile;             // 어두운 타일 (검정)
    public TileBase exploredTile;        // 밝힌 흔적용 타일 (회색+알파)

    [Header("설정값")]
    public int revealRadius = 2;

    private HashSet<Vector3Int> revealedTiles = new();

    void Start()
    {
        RectInt area = new RectInt(-10, 0, 20, 20); // 적절한 범위 설정
        FillFog(area);
    }

    public void Reveal(Vector3 worldPos)
    {
        Vector3Int center = fogTilemap.WorldToCell(worldPos);

        for (int dx = -revealRadius; dx <= revealRadius; dx++)
        {
            for (int dy = -revealRadius; dy <= revealRadius; dy++)
            {
                Vector3Int pos = new Vector3Int(center.x + dx, center.y + dy, 0);
                if (Vector3Int.Distance(center, pos) <= revealRadius)
                {
                    // 안개 제거
                    fogTilemap.SetTile(pos, null);

                    // 밝힌 흔적 기록
                    if (!revealedTiles.Contains(pos))
                    {
                        exploredTilemap.SetTile(pos, exploredTile);
                        revealedTiles.Add(pos);
                    }
                }
            }
        }
    }

    public void FillFog(RectInt bounds)
    {
        for (int x = bounds.xMin; x <= bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y <= bounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                fogTilemap.SetTile(pos, fogTile);
            }
        }
    }
}
