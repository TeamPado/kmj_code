using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaveManager : MonoBehaviour
{
    private SaveLoadManager saveLoadManager;
    private GameObject player;
    private PlayerStats playerHealth;

    private void Start()
    {
        saveLoadManager = FindFirstObjectByType<SaveLoadManager>();
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerStats>();
        }
        else
        {
            Debug.LogError("플레이어 오브젝트를 찾을 수 없습니다. 태그가 올바르게 설정되었는지 확인하세요.");
        }
    }

    public void OnClickSave()
    {
        if (player != null && saveLoadManager != null)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            Vector3 position = player.transform.position;
            int health = playerHealth != null ? playerHealth.currentHealth : 100;
            int exp = 50;

            saveLoadManager.SaveGame(currentScene, position, health, exp);
            Debug.Log("게임 저장 완료! JSON 파일에 저장됨.");
        }
    }
}
