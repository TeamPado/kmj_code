using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 플레이어 이동 속도
    private Rigidbody2D rb;

    // 플레이어가 이동할 수 있는 범위를 체크할 Tilemap
    public Tilemap walkableTilemap;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 방향 입력 받기
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 moveDirection = moveInput.normalized;

        // 이동 체크 및 제한
        Vector2 newPosition = rb.position + moveDirection * moveSpeed * Time.deltaTime;

        // 이동할 위치가 walkableTilemap에 포함되는지 확인
        if (IsTileWalkable(newPosition))
        {
            rb.MovePosition(newPosition);
        }
    }

    // 플레이어가 이동하려는 위치가 파란색 타일 안에 있는지 체크
    bool IsTileWalkable(Vector2 targetPosition)
    {
        // 타일맵의 위치를 world 좌표에서 타일 좌표로 변환
        Vector3Int tilePosition = walkableTilemap.WorldToCell(targetPosition);

        // 해당 타일이 파란색 타일인지 확인
        TileBase tile = walkableTilemap.GetTile(tilePosition);
        return tile != null; // 타일이 존재하면 이동 가능
    }
}
