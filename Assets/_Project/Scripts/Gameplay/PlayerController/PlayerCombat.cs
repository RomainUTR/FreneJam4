using System;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerCombat : MonoBehaviour
{
    [Title("Configuration")]
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [Required]
    public PlayerSettingsSO settings;

    [Title("Setup")]
    [Required] public Projectile projectilePrefab;
    [Required] public Transform firePoint;
    [Required, InlineEditor] public SoundData ShootSound;

    public event Action OnPlayerShoot;

    private PlayerInput playerInput;
    private float nextFireTime = 0f;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (playerInput.IsShooting && Time.time >= nextFireTime)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        nextFireTime = Time.time + settings.fireRate;

        OnPlayerShoot?.Invoke();
        //AudioManager.Instance.PlayClipAt(ShootSound, firePoint.position);
        Projectile newProj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        newProj.Initialize(firePoint.forward);
    }

    public float GetRemainingCooldown()
    {
        if (Time.time >= nextFireTime)
        {
            return 0f;
        }

        return nextFireTime - Time.time;
    }

    public float GetCooldownRatio()
    {
        if (Time.time >= nextFireTime)
        {
            return 1f;
        }

        float remainingTime = nextFireTime - Time.time;
        return 1f - (remainingTime / settings.fireRate);
    }
}
