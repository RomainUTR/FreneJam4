using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryUIManager : MonoBehaviour
{
    [Title("UI References")]
    [Required] public GameObject victoryPanel;

    [Title("Events")]
    [Required] public RSE_OnVictory OnVictoryEvent;

    private void OnEnable()
    {
        OnVictoryEvent.OnEventRaised += ShowVictoryScreen;
    }

    private void OnDisable()
    {
        OnVictoryEvent.OnEventRaised -= ShowVictoryScreen;
    }

    private void Start()
    {
        victoryPanel.SetActive(false);
    }

    void ShowVictoryScreen()
    {
        Time.timeScale = 0f;
        victoryPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}