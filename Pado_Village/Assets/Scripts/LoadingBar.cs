using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Image loadingBar; // UI �̹��� (Fill Type�� "Filled" ���� �ʿ�)
    private AsyncOperation asyncLoad;

    public void StartLoading(string sceneName)
    {
        gameObject.SetActive(true); // �ε� �� Ȱ��ȭ
        StartCoroutine(Loading(sceneName));
    }

    private IEnumerator Loading(string sceneName)
    {
        float progress = 0f;
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; // 100% �ε��� ������ �� ��ȯ ����

        while (!asyncLoad.isDone)
        {
            // ���� �� �ε� ������� �ݿ� (0.9���� �ö�)
            progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingBar.fillAmount = progress;

            yield return null;

            // 100% �ε� �Ϸ� �� 1�� ��� �� �� ��ȯ
            if (progress >= 1f)
            {
                yield return new WaitForSeconds(1f);
                asyncLoad.allowSceneActivation = true;
            }
        }
    }
}
