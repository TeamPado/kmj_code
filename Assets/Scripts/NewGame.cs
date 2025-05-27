using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor;

public class NewGame : MonoBehaviour
{
    private SaveLoadManager saveLoadManager;
    public CanvasGroup fadeCanvas; 
    public float fadeDuration = 1.5f; // 페이드 시간
    private void Start()
    {
        fadeCanvas.alpha = 0f; 
        StartCoroutine(FadeIn());
        saveLoadManager = FindFirstObjectByType<SaveLoadManager>();
    }

    public void OnClickNewGame()
    {
        Debug.Log("새 게임 시작");
        StartCoroutine(FadeOutAndLoad("Customize")); //커마로 이동
    }

    public void OnClickQuit()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }

    public void OnClickLoad()
    {
        Debug.Log("게임 불러오기");
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
            fadeCanvas.alpha = timer / fadeDuration; // 점점 나타남
            yield return null;
        }
    }

    private IEnumerator FadeOutAndLoad(string sceneName)
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeCanvas.alpha = 1 - (timer / fadeDuration); // 점점 사라짐
            yield return null;
        }
        SceneManager.LoadScene(sceneName); // 씬 전환
    }
    private System.Collections.IEnumerator LoadSceneAndSetPlayer(string sceneName, Vector3 position, int health, int exp)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // 씬이 로드된 후, 플레이어 위치 복구
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = position;
        }
    }
}