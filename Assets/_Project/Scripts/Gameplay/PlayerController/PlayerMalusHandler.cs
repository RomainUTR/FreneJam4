using UnityEngine;

public class PlayerMalusHandler : MonoBehaviour
{
    [Header("Settings")]
    public float SearchRadius = 20f;
    public LayerMask EnemyLayer;

    [Header("References")]
    private CharacterController _playerController;

    [Header("Input")]
    public RSF_ForceTeleportToClosestEnemy ForceTeleportToClosestEnemy;

    //[Header("Output")]

    private void Awake()
    {
        _playerController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        ForceTeleportToClosestEnemy.OnInvoke = ExecuteSwap;
    }

    private void OnDisable()
    {
        ForceTeleportToClosestEnemy.OnInvoke = null;
    }

    private bool ExecuteSwap()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, SearchRadius, EnemyLayer);

        if (hits.Length == 0) return false;

        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider hit in hits)
        {
            float distance = Vector3.Distance(transform.position, hit.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = hit.transform;
            }
        }

        if (closestEnemy != null)
        {
            Vector3 playerPos = transform.position;
            Vector3 enemyPos = closestEnemy.position;

            if (_playerController != null) _playerController.enabled = false;

            transform.position = enemyPos;

            if (_playerController != null) _playerController.enabled = true;

            closestEnemy.position = playerPos;

            return true;
        }

        return false;   
    }
}