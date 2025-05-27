using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;
    public string battleSceneName = "BattleScene";

    private void Update()
    {
        //if (fadeImage != null)
          //  Debug.Log($" FadeImage Alpha: {fadeImage.color.a}");
    }
    public void StartBattleTransition()
    {
        //Debug.Log("StartBattleTransition 실행됨");
        if (fadeImage != null)
            fadeImage.color = new Color(0, 0, 0, 0); // 초기화 (투명)

        StartCoroutine(FadeAndLoadBattle());
    }

    IEnumerator FadeAndLoadBattle()
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            fadeImage.color = new Color(0, 0, 0, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 1f); // 완전 검정
        SceneManager.LoadScene(battleSceneName);
    }
}
