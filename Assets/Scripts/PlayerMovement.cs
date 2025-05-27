using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 200f;

    private RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(h, v, 0).normalized * moveSpeed * Time.deltaTime;
        transform.localPosition += move;
    }
}
