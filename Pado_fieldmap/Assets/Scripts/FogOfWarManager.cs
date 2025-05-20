using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FogOfWarManager : MonoBehaviour
{
    [Header("Ÿ�ϸ� ����")]
    public Tilemap fogTilemap;           // ��ο� �Ȱ� Ÿ�ϸ�
    public Tilemap exploredTilemap;      // ���� ���� Ÿ�ϸ�

    [Header("Ÿ�� ����")]
    public TileBase fogTile;             // ��ο� Ÿ�� (����)
    public TileBase exploredTile;        // ���� ������ Ÿ�� (ȸ��+����)

    [Header("������")]
    public int revealRadius = 2;

    private HashSet<Vector3Int> revealedTiles = new();

    void Start()
    {
        RectInt area = new RectInt(-10, 0, 20, 20); // ������ ���� ����
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
                    // �Ȱ� ����
                    fogTilemap.SetTile(pos, null);

                    // ���� ���� ���
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
