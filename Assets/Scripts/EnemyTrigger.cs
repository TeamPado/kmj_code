using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;

            FadeManager fade = FindFirstObjectByType<FadeManager>();
            if (fade != null)
            {
                fade.StartBattleTransition();
            }
            else
            {
                Debug.LogError("FadeManager를 찾을 수 없습니다!");
            }
        }
    }
}
