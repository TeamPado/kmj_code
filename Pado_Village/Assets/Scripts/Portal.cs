using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    public string nextSceneName;  // 이동할 씬 이름
    public GameObject loadingScreen;  // 로딩 UI 오브젝트 (Canvas)
    public Text loadingText;  // 로딩 상태 표시 텍스트
    public Slider loadingBar;  // 로딩 진행률 바 (선택 사항)
    public GameObject portalIcon;
    private bool isPlayerNearby = false;
    private bool canInteract = false;
    private bool isLoading = false; // 중복 로딩 방지

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
            Debug.Log("포탈타짐");
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
        isLoading = true; // 중복 실행 방지
        loadingScreen.SetActive(true); // 로딩 화면 표시
        StartCoroutine(AnimateLoadingText()); // "loading..." 애니메이션 시작

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; // 100% 로딩될 때까지 씬 전환 방지

        while (asyncLoad.progress < 0.9f) // 0~0.9까지 로딩 진행률 표시
        {
            if (loadingBar != null)
            {
                loadingBar.value = asyncLoad.progress;
            }
            yield return null;
        }

        // 거의 완료 상태에서 1초간 대기 후 씬 전환 (연출용)
        yield return new WaitForSeconds(1f);

        asyncLoad.allowSceneActivation = true; // 씬 전환 실행
    }

    // "loading..." -> "loading.." -> "loading." 반복 애니메이션
    private IEnumerator AnimateLoadingText()
    {
        string baseText = "loading";
        int dotCount = 0;
        while (loadingScreen.activeSelf)
        {
            loadingText.text = baseText + new string('.', dotCount);
            dotCount = (dotCount + 1) % 4; // 점 개수 0~3 순환
            yield return new WaitForSeconds(0.5f);
        }
    }
}
