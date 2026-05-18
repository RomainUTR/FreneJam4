using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [InlineEditor, SerializeField] private ProjectileStatsSO Stats;
    [Required] public RSE_OnComboFinished OnComboFinished;
    [InlineEditor, Required] public SoundData BounceSound;

    private Vector3 _currentDirection;
    private int _bounceCount = 0;
    private float _age = 0f;
    public bool CanHurtPlayer { get; private set; } = false;

    public int EnemyKilledCount = 0;

    public void Initialize(Vector3 startDirection)
    {
        _currentDirection = startDirection.normalized;
        StartCoroutine(FirstSecondsKillsCount());
    }

    void Update()
    {
        _age += Time.deltaTime;

        if (!CanHurtPlayer && _age >= Stats.PlayerGracePeriod)
        {
            CanHurtPlayer = true;
            Material objectMat = GetComponent<MeshRenderer>().material;
            objectMat.color = Color.red;
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
                Destroy(gameObject);
                return;
            }

            _currentDirection = Vector3.Reflect(_currentDirection, hit.normal);
            //AudioManager.Instance.PlayClipAt(BounceSound, transform.position);
            transform.position = hit.point + (hit.normal * Stats.CollisionRadius);
        } else
        {
            transform.Translate(_currentDirection * moveDistance, Space.World);
        }
    }

    private IEnumerator FirstSecondsKillsCount()
    {
        yield return new WaitForSeconds(1.5f);

        OnComboFinished.RaiseEvent(EnemyKilledCount);
    }
}
