using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    [Title("Data")]
    [InlineEditor, SerializeField]
    private RSO_PlayerRuntimeStats runtimeStats;

    private Vector3 _currentDirection;
    private int _bounceCount;
    private float _age;
    public bool CanHurtPlayer { get; private set; }

    private IObjectPool<Projectile> _pool;
    private TrailRenderer _trailRenderer;

    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    public void Initialize(Vector3 startDirection, IObjectPool<Projectile> pool)
    {
        _pool = pool;
        _currentDirection = startDirection.normalized;

        _bounceCount = 0;
        _age = 0f;
        CanHurtPlayer = false;

        GetComponent<MeshRenderer>().material.color = Color.white;

        if (_trailRenderer != null)
        {
            _trailRenderer.Clear();
        }
    }

    void Update()
    {
        _age += Time.deltaTime;

        if (!CanHurtPlayer && _age >= runtimeStats.baseProjectileStats.PlayerGracePeriod)
        {
            CanHurtPlayer = true;
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    private void FixedUpdate()
    {
        float moveDistance = runtimeStats.baseProjectileStats.Speed * Time.deltaTime;
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, runtimeStats.baseProjectileStats.CollisionRadius, _currentDirection, out hit, moveDistance, runtimeStats.baseProjectileStats.BounceMask))
        {
            _bounceCount++;

            if (_bounceCount > runtimeStats.currentMaxBounces)
            {
                _pool.Release(this);
                return;
            }

            _currentDirection = Vector3.Reflect(_currentDirection, hit.normal);
            transform.position = hit.point + (hit.normal * runtimeStats.baseProjectileStats.CollisionRadius);
        }
        else
        {
            transform.Translate(_currentDirection * moveDistance, Space.World);
        }
    }

    public void ReleaseToPool()
    {
        if (_pool != null) _pool.Release(this);
    }
}