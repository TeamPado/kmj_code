using UnityEngine;

public class PlayerFogRevealer : MonoBehaviour
{
    private FogOfWarManager fog;

    void Start()
    {
        fog = Object.FindFirstObjectByType<FogOfWarManager>();
        if (fog != null)
        {
            fog.Reveal(transform.position); // 시작 위치 즉시 밝히기
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
