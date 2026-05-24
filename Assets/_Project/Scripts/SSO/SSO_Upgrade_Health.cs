using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "SSO_Upgrade_Health", menuName = "Data/SSO/Upgrades/SSO_Upgrade_Health")]
public class SSO_Upgrade_Health : SSO_Upgrade
{
    [Header("Data")]
    [Required] public RSO_PlayerRuntimeStats playerStats;

    [InfoBox("Combien de PV Max on ajoute au joueur ?")]
    public int flatHealthToAdd = 10;

    public RSE_OnHealPlayer OnHealPlayer;

    public override void ApplyUpgrade()
    {
        playerStats.AddFlatHealth(flatHealthToAdd);

        if (OnHealPlayer != null)
        {
            OnHealPlayer.RaiseEvent(flatHealthToAdd);
        }
    }
}