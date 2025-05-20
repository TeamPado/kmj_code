using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor;

public class NewGame : MonoBehaviour
{
    private SaveLoadManager saveLoadManager;
    public CanvasGroup fadeCanvas; 
    public float fadeDuration = 1.5f; // ���̵� �ð�
    private void Start()
    {
        fadeCanvas.alpha = 0f; 
        StartCoroutine(FadeIn());
        saveLoadManager = FindFirstObjectByType<SaveLoadManager>();
    }

    public void OnClickNewGame()
    {
        Debug.Log("�� ���� ����");
        StartCoroutine(FadeOutAndLoad("Customize")); //Ŀ���� �̵�
    }

    public void OnClickQuit()
    {
        Debug.Log("���� ����");
        Application.Quit();
    }

    public void OnClickLoad()
    {
        Debug.Log("���� �ҷ�����");
        string sceneName;
        Vector3 playerPosition;
        int health, exp;

        if (saveLoadManager.LoadGame(out sceneName, out playerPosition, out health, out exp))
        {
            StartCoroutine(LoadSceneAndSetPlayer(sceneName, playerPosition, health, exp));
        }
    }

    private IEnumerator FadeIn()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeCanvas.alpha = timer / fadeDuration; // ���� ��Ÿ��
            yield return null;
        }
    }

    private IEnumerator FadeOutAndLoad(string sceneName)
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeCanvas.alpha = 1 - (timer / fadeDuration); // ���� �����
            yield return null;
        }
        SceneManager.LoadScene(sceneName); // �� ��ȯ
    }
    private System.Collections.IEnumerator LoadSceneAndSetPlayer(string sceneName, Vector3 position, int health, int exp)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // ���� �ε�� ��, �÷��̾� ��ġ ����
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = position;
        }
    }
}