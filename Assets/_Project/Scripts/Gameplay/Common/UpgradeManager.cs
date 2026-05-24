using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Title("Data")]
    public List<SSO_Upgrade> UpgradeList;

    [Title("UI References")]
    public GameObject upgradeUIPanel;
    public UpgradeCardUI[] upgradeCards;

    [Title("Events")]
    [Required] public RSE_OnWaveCleared OnWaveCleared;
    [Required] public RSE_OnUpgradeFinished OnUpgradeFinished;

    private void OnEnable()
    {
        OnWaveCleared.OnEventRaised += ShowUpgrades;
    }

    private void OnDisable()
    {
        OnWaveCleared.OnEventRaised -= ShowUpgrades;
    }

    private void Start()
    {
        upgradeUIPanel.SetActive(false);
    }

    void ShowUpgrades()
    {
        Time.timeScale = 0f;
        upgradeUIPanel.SetActive(true);

        List<SSO_Upgrade> availablePool = new List<SSO_Upgrade>(UpgradeList);

        for (int i = 0; i < upgradeCards.Length; i++)
        {
            if (availablePool.Count == 0) break;

            int randomIndex = Random.Range(0, availablePool.Count);
            SSO_Upgrade chosenUpgrade = availablePool[randomIndex];

            upgradeCards[i].SetupCard(chosenUpgrade, this);
            availablePool.RemoveAt(randomIndex);
        }
    }

    public void CompleteUpgradeSelection()
    {
        upgradeUIPanel.SetActive(false);
        Time.timeScale = 1f;
        OnUpgradeFinished?.RaiseEvent();
    }
}