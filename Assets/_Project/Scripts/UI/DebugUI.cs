using TMPro;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    [Header("References")]
    public PlayerCombat playerCombat;
    public TextMeshProUGUI cooldownText;

    void Update()
    {
        float remainingTime = playerCombat.GetRemainingCooldown();
        cooldownText.text = "Cooldown : " + remainingTime.ToString("F2") + "s";
    }
}
