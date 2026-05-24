using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "SSO_Upgrade_FireRate", menuName = "Data/SSO/Upgrades/SSO_Upgrade_FireRate")]
public class SSO_Upgrade_FireRate : SSO_Upgrade
{
    [Header("Data")]
    [Required] public RSO_PlayerRuntimeStats playerStats;

    [InfoBox("Exemple: 0.15 = +15%")]
    public float GainFireRate = 0.15f;

    public override void ApplyUpgrade()
    {
        playerStats.ImproveFireRate(GainFireRate);
    }
}