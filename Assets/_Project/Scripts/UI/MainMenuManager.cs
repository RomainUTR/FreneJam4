using RomainUTR.SLToolbox;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Required] public SceneReference GameScene;
    public TMP_Text ApplicationVersion;
    public GameObject ControlsPanel;


    void Update()
    {
        ApplicationVersion.text = $"Application version : {Application.version}";
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(GameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenControlsPanel()
    {
        ControlsPanel.SetActive(true);
    }

    public void CloseControlsPanel()
    {
        ControlsPanel.SetActive(false);
    }
}
