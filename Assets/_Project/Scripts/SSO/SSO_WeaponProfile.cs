using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "SSO_WeaponProfile", menuName = "Data/SSO/SSO_WeaponProfile")]
public class SSO_WeaponProfile : ScriptableObject
{
    [Title("Weapon Stats")]
    [SuffixLabel("sec", true)] public float fireRate = 0.1f;
    public float spreadAngle = 10f;

    [Title("Heat")]
    public float heatPerShot = 0.15f;
    public float coolingRate = 0.3f;

    public Projectile projectilePrefab;
}