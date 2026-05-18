using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerDeath : MonoBehaviour
{
    [Required] public RSE_OnPlayerDeath OnPlayerDeath;
    public GameObject GameOverPanel, FeelPanel;

    void OnEnable()
    {
        OnPlayerDeath.OnEventRaised += TriggerDeath;
    }

    void OnDisable()
    {
        OnPlayerDeath.OnEventRaised -= TriggerDeath;
    }

    void Start()
    {
        GameOverPanel.SetActive(false);
    }

    void TriggerDeath()
    {
        Debug.Log("KO");
        Time.timeScale = 0;
        FeelPanel.SetActive(false);
        GameOverPanel.SetActive(true);
    }
}
