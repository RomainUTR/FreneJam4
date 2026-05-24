using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "SSO_Upgrade_Damage", menuName = "Data/SSO/Upgrades/SSO_Upgrade_Damage")]
public class SSO_Upgrade_Damage : SSO_Upgrade
{
    [Header("Data")]
    [Required] public RSO_PlayerRuntimeStats playerStats;

    public float amountOfDamage = 1f;

    public override void ApplyUpgrade()
    {
        playerStats.AddFlatDamage(amountOfDamage);
    }
}