using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerHealth : MonoBehaviour
{
    [Title("Data")]
    [Required, InlineEditor] public RSO_PlayerRuntimeStats runtimeStats;

    [Required] public RSE_OnPlayerDeath OnPlayerDeath;
    [Required] public RSE_OnDamakeTaken OnDamakeTaken;
    [Required] public RSE_OnHealthChanged OnHealthChanged;
    [Required] public RSE_OnHealPlayer OnHealPlayer;
    [Required, InlineEditor] public SoundData HealSound, ExplosionSound;
    public GameObject FX_OnPlayerHit;

    private float _currentHealth;

    void OnEnable()
    {
        OnDamakeTaken.OnEventRaised += TakeDamage;
        OnHealPlayer.OnEventRaised += HealPlayer;
    }

    void OnDisable()
    {
        OnDamakeTaken.OnEventRaised -= TakeDamage;
        OnHealPlayer.OnEventRaised -= HealPlayer;
    }

    void Start()
    {
        _currentHealth = runtimeStats.currentMaxHealth;

        OnHealthChanged?.RaiseEvent(_currentHealth);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Projectile ball = other.GetComponent<Projectile>();

            if (ball != null && ball.CanHurtPlayer)
            {
                // AudioManager.Instance.PlayClipAt(ExplosionSound, other.transform.position);
                Instantiate(FX_OnPlayerHit, other.transform.position, Quaternion.identity);
                OnDamakeTaken?.RaiseEvent(1.5f);

                ball.ReleaseToPool();
            }
        }
    }

    void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        OnHealthChanged?.RaiseEvent(_currentHealth);
        Debug.Log($"Player took {amount} damage. Current health: {_currentHealth}");

        if (_currentHealth <= 0)
        {
            OnPlayerDeath?.RaiseEvent();
        }
    }

    void HealPlayer(int amount)
    {
        if (_currentHealth >= 0 && _currentHealth <= runtimeStats.currentMaxHealth)
        {
            //AudioManager.Instance.PlayClipAt(HealSound, transform.position);
            _currentHealth = Mathf.Min(_currentHealth + amount, runtimeStats.currentMaxHealth);
            OnHealthChanged?.RaiseEvent(_currentHealth);
        }
    }
}