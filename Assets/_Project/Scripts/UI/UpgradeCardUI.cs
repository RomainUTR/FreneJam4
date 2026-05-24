using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

public class UpgradeCardUI : MonoBehaviour
{
    [Title("UI References")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public Image iconImage;
    public Button cardButton;

    public SSO_Upgrade _currentUpgrade;
    private UpgradeManager _manager;

    public void SetupCard(SSO_Upgrade upgradeData, UpgradeManager manager)
    {
        _currentUpgrade = upgradeData;
        _manager = manager;

        nameText.text = upgradeData.upgradeName;
        descriptionText.text = upgradeData.description;
        iconImage.sprite = upgradeData.icon;

        cardButton.onClick.RemoveAllListeners();
        cardButton.onClick.AddListener(OnCardClicked);
    }

    private void OnCardClicked()
    {
        _currentUpgrade.ApplyUpgrade();
        _manager.CompleteUpgradeSelection();
        Debug.Log("Upgrade: " + _currentUpgrade.name);
    }
}