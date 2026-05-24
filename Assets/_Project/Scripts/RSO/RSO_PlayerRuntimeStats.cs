using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSO_PlayerRuntimeStats", menuName = "Data/RSO/RSO_PlayerRuntimeStats")]
public class RSO_PlayerRuntimeStats : ScriptableObject
{
    [Title("Static References (Read Only)")]
    [Required] public PlayerSettingsSO basePlayerSettings;
    [Required] public SSO_WeaponProfile baseWeaponProfile;
    [Required] public ProjectileStatsSO baseProjectileStats;

    [Title("Runtime Stats (Live Data)")]
    [ReadOnly] public float currentRunSpeed;
    [ReadOnly] public int currentMaxHealth;
    [ReadOnly] public float currentFireRate;
    [ReadOnly] public float currentDamage;
    [ReadOnly] public int currentMaxBounces;

    public void Initialize()
    {
        if (basePlayerSettings != null)
        {
            currentRunSpeed = basePlayerSettings.runSpeed;
            currentMaxHealth = basePlayerSettings.maxHealth;
        }

        if (baseWeaponProfile != null)
        {
            currentFireRate = baseWeaponProfile.fireRate;
        }

        if (baseProjectileStats != null)
        {
            currentDamage = baseProjectileStats.damage;
            currentMaxBounces = baseProjectileStats.MaxBounces;
        }
    }

    public void AddFlatDamage(float amount)
    {
        currentDamage += amount;
    }

    public void MultiplySpeed(float percentage)
    {
        currentRunSpeed += currentRunSpeed * percentage;
    }

    public void ImproveFireRate(float percentage)
    {
        float reduction = currentFireRate * percentage;
        currentFireRate -= reduction;

        currentFireRate = Mathf.Max(0.05f, currentFireRate); // Ne pas descendre en dessous de 0.05
    }

    public void AddFlatHealth(int amount)
    {
        currentMaxHealth += amount;
    }

    public void MultiplyMaxHealth(float percentage)
    {
        int bonusHealth = Mathf.RoundToInt(currentMaxHealth * percentage);
        currentMaxHealth += bonusHealth;
    }
}