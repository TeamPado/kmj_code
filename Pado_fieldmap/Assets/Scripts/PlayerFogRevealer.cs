using UnityEngine;

public class PlayerFogRevealer : MonoBehaviour
{
    private FogOfWarManager fog;

    void Start()
    {
        fog = Object.FindFirstObjectByType<FogOfWarManager>();
        if (fog != null)
        {
            fog.Reveal(transform.position); // ���� ��ġ ��� ������
        }
    }

    void Update()
    {
        if (fog != null)
        {
            fog.Reveal(transform.position);
        }
    }
}
