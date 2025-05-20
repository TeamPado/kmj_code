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
            Debug.LogError("�÷��̾� ������Ʈ�� ã�� �� �����ϴ�. �±װ� �ùٸ��� �����Ǿ����� Ȯ���ϼ���.");
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
            Debug.Log("���� ���� �Ϸ�! JSON ���Ͽ� �����.");
        }
    }
}
