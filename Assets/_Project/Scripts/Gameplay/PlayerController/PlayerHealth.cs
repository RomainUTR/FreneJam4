using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerHealth : MonoBehaviour
{
    [Title("Data")]
    [Required, InlineEditor] public PlayerSettingsSO Settings;
    [Required] public RSE_OnPlayerDeath OnPlayerDeath;
    [Required] public RSE_OnDamakeTaken OnDamakeTaken;
    [Required] public RSE_OnHealthChanged OnHealthChanged;
    [Required] public RSE_OnHealPlayer OnHealPlayer;
    [Required, InlineEditor] public SoundData HealSound, ExplosionSound;
    public GameObject FX_OnPlayerHit;

    private int _currentHealth;

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

    void Awake()
    {
        _currentHealth = Settings.maxHealth;
    }

    void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        OnHealthChanged?.RaiseEvent(_currentHealth);

        if (_currentHealth <= 0)
        {   
            OnPlayerDeath?.RaiseEvent();
        }
    }

    void HealPlayer(int amount)
    {
        if (_currentHealth >= 0 && _currentHealth <= Settings.maxHealth)
        {
            //AudioManager.Instance.PlayClipAt(HealSound, transform.position);
            _currentHealth = Mathf.Min(_currentHealth + amount, Settings.maxHealth);
            OnHealthChanged?.RaiseEvent(_currentHealth);
        }
    }
}
