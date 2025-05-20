using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    public string nextSceneName;  // �̵��� �� �̸�
    public GameObject loadingScreen;  // �ε� UI ������Ʈ (Canvas)
    public Text loadingText;  // �ε� ���� ǥ�� �ؽ�Ʈ
    public Slider loadingBar;  // �ε� ����� �� (���� ����)
    public GameObject portalIcon;
    private bool isPlayerNearby = false;
    private bool canInteract = false;
    private bool isLoading = false; // �ߺ� �ε� ����

    private void Start()
    {
        loadingScreen.SetActive(false);
        portalIcon.SetActive(false);
    }
    void Update()
    {
        if (canInteract && !isLoading && Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(LoadSceneAsync(nextSceneName));
            Debug.Log("��ŻŸ��");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
            isPlayerNearby = true;
            portalIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            isPlayerNearby = false;
            portalIcon.SetActive(false);
        }
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        isLoading = true; // �ߺ� ���� ����
        loadingScreen.SetActive(true); // �ε� ȭ�� ǥ��
        StartCoroutine(AnimateLoadingText()); // "loading..." �ִϸ��̼� ����

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; // 100% �ε��� ������ �� ��ȯ ����

        while (asyncLoad.progress < 0.9f) // 0~0.9���� �ε� ����� ǥ��
        {
            if (loadingBar != null)
            {
                loadingBar.value = asyncLoad.progress;
            }
            yield return null;
        }

        // ���� �Ϸ� ���¿��� 1�ʰ� ��� �� �� ��ȯ (�����)
        yield return new WaitForSeconds(1f);

        asyncLoad.allowSceneActivation = true; // �� ��ȯ ����
    }

    // "loading..." -> "loading.." -> "loading." �ݺ� �ִϸ��̼�
    private IEnumerator AnimateLoadingText()
    {
        string baseText = "loading";
        int dotCount = 0;
        while (loadingScreen.activeSelf)
        {
            loadingText.text = baseText + new string('.', dotCount);
            dotCount = (dotCount + 1) % 4; // �� ���� 0~3 ��ȯ
            yield return new WaitForSeconds(0.5f);
        }
    }
}
