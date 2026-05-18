using Sirenix.OdinInspector;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Settings")]
    public float MoveSpeed = 3f;
    [Required, InlineEditor] public EnemySO Settings;
    public RSE_OnDamakeTaken OnDamakeTaken;
    public RSE_OnEnemyKilled OnEnemyKilled;
    [Required, InlineEditor] public SoundData ExplosionSound;

    public GameObject FX_OnHit, FX_OnPlayerHit;
    public RSE_EnemyTakeDamage EnemyTakeDamage;
    public ProjectileStatsSO projectileStats;
    public PlayerSettingsSO playerSettings;

    public Transform PlayerTarget;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            PlayerTarget = playerObj.transform;
        }
    }

    void Update()
    {
        if (PlayerTarget == null) return;

        Vector3 lookPosition = new Vector3(PlayerTarget.position.x, transform.position.y, PlayerTarget.position.z);
        transform.LookAt(lookPosition);

        transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Projectile"))
        {
            //other.GetComponent<Projectile>().EnemyKilledCount++;
            //AudioManager.Instance.PlayClipAt(ExplosionSound, other.transform.position);
            //Destroy(gameObject);
            //Instantiate(FX_OnHit, transform.position, Quaternion.identity);
            //OnEnemyKilled?.RaiseEvent();

            EnemyTakeDamage.RaiseEvent(projectileStats.damage);
        } else if (other.CompareTag("Player"))
        {
            //Destroy(gameObject);
            //AudioManager.Instance.PlayClipAt(ExplosionSound, other.transform.position);
            //Instantiate(FX_OnPlayerHit, other.transform.position, Quaternion.identity);
            //OnDamakeTaken.RaiseEvent(Settings.BaseDamage);

            EnemyTakeDamage?.RaiseEvent(playerSettings.damage);
        }
    }
}
