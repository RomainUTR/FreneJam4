using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool; // Nécessaire pour l'IObjectPool

public class Projectile : MonoBehaviour
{
    [InlineEditor, SerializeField] private ProjectileStatsSO Stats;

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

        if (!CanHurtPlayer && _age >= Stats.PlayerGracePeriod)
        {
            CanHurtPlayer = true;
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    private void FixedUpdate()
    {
        float moveDistance = Stats.Speed * Time.deltaTime;
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, Stats.CollisionRadius, _currentDirection, out hit, moveDistance, Stats.BounceMask))
        {
            _bounceCount++;

            if (_bounceCount > Stats.MaxBounces)
            {
                _pool.Release(this);
                return;
            }

            _currentDirection = Vector3.Reflect(_currentDirection, hit.normal);
            transform.position = hit.point + (hit.normal * Stats.CollisionRadius);
        }
        else
        {
            transform.Translate(_currentDirection * moveDistance, Space.World);
        }
    }
}