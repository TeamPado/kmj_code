using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIController : MonoBehaviour
{
    [Header("Main Buttons")]
    public Button newGameButton;
    public Button loadGameButton;
    public Button settingsButton;
    public Button exitButton;

    void Start()
    {
        newGameButton.onClick.AddListener(OnNewGameClicked);
        loadGameButton.onClick.AddListener(OnLoadGameClicked);
        settingsButton.onClick.AddListener(OnSettingsClicked);
        exitButton.onClick.AddListener(OnExitClicked);
    }

    void OnNewGameClicked()
    {
        Debug.Log("New Game");
        // SceneManager.LoadScene("GameScene");
    }

    void OnLoadGameClicked()
    {
        Debug.Log("Load Game");
        // LoadManager.LoadSavedGame();
    }

    void OnSettingsClicked()
    {
        Debug.Log("Settings");
        // Open settings UI
    }

    void OnExitClicked()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
