using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(PlayerInput))]
public class PlayerCombat : MonoBehaviour
{
    [Title("Configuration")]
    [Required, InlineEditor] public SSO_WeaponProfile currentWeapon;

    [Title("Setup")]
    [Required] public Transform firePoint;

    public float currentHeat { get; private set; } = 0f;
    public bool isOverheated { get; private set; } = false;

    public event Action OnPlayerShoot;

    private PlayerInput playerInput;
    private float nextFireTime = 0f;

    private IObjectPool<Projectile> projectilePool;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        projectilePool = new ObjectPool<Projectile>(
            createFunc: CreateProjectile,
            actionOnGet: proj => proj.gameObject.SetActive(true),
            actionOnRelease: proj => proj.gameObject.SetActive(false),
            actionOnDestroy: proj => Destroy(proj.gameObject),
            collectionCheck: false,
            defaultCapacity: 50,
            maxSize: 200
        );
    }

    private void Update()
    {
        if (currentHeat > 0f)
        {
            currentHeat -= currentWeapon.coolingRate * Time.deltaTime;
            currentHeat = Mathf.Clamp01(currentHeat);
        }

        if (isOverheated && currentHeat <= 0f)
        {
            isOverheated = false;
        }

        if (playerInput.IsShooting && Time.time >= nextFireTime && !isOverheated)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        nextFireTime = Time.time + currentWeapon.fireRate;
        OnPlayerShoot?.Invoke();

        currentHeat += currentWeapon.heatPerShot;

        if (currentHeat >= 1f)
        {
            isOverheated = true;
            currentHeat = 1f;
        }

        Projectile newProj = projectilePool.Get();
        newProj.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);

        float randomSpread = UnityEngine.Random.Range(-currentWeapon.spreadAngle / 2f, currentWeapon.spreadAngle / 2f);
        Quaternion spreadRotation = Quaternion.Euler(0f, randomSpread, 0f);
        Vector3 finalDirection = spreadRotation * firePoint.forward;

        newProj.Initialize(finalDirection, projectilePool);
    }

    private Projectile CreateProjectile()
    {
        return Instantiate(currentWeapon.projectilePrefab);
    }
}