using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "SSO_Upgrade_Speed", menuName = "Data/SSO/Upgrades/SSO_Upgrade_Speed")]
public class SSO_Upgrade_Speed : SSO_Upgrade
{
    [Header("Data")]
    [Required] public RSO_PlayerRuntimeStats playerStats;

    [InfoBox("Exemple: 0.15 = +15%")]
    public float speedPercentageToAdd = 0.15f;

    public override void ApplyUpgrade()
    {
        playerStats.MultiplySpeed(speedPercentageToAdd);
    }
}