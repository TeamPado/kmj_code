using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Image loadingBar; // UI 이미지 (Fill Type이 "Filled" 설정 필요)
    private AsyncOperation asyncLoad;

    public void StartLoading(string sceneName)
    {
        gameObject.SetActive(true); // 로딩 바 활성화
        StartCoroutine(Loading(sceneName));
    }

    private IEnumerator Loading(string sceneName)
    {
        float progress = 0f;
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; // 100% 로딩될 때까지 씬 전환 방지

        while (!asyncLoad.isDone)
        {
            // 실제 씬 로딩 진행률을 반영 (0.9까지 올라감)
            progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingBar.fillAmount = progress;

            yield return null;

            // 100% 로딩 완료 후 1초 대기 후 씬 전환
            if (progress >= 1f)
            {
                yield return new WaitForSeconds(1f);
                asyncLoad.allowSceneActivation = true;
            }
        }
    }
}
