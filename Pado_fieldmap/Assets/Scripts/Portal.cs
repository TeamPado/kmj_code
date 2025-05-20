using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string targetSceneName;
    public GameObject portalIcon;

    private bool isPlayerNear = false;

    void Start()
    {
        if (portalIcon != null)
            portalIcon.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Æ÷Å» È°¼ºÈ­!");
            SceneManager.LoadScene(targetSceneName);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (portalIcon != null)
                portalIcon.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (portalIcon != null)
                portalIcon.SetActive(false);
        }
    }
}
