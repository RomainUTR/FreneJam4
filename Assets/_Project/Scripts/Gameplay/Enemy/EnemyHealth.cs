using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Settings")]
    public EnemySO enemySO;
    public RSO_EnemyScaling enemyScaling;

    //[Header("References")]

    [Header("Input")]
    public RSE_EnemyTakeDamage EnemyTakeDamage;
    public TMP_Text HPText;

    [Header("Output")]
    private float _currentHealth;
    public RSE_OnEnemyKilled OnEnemyKilled;

    void Awake()
    {
        _currentHealth = enemySO.BaseHealth * enemyScaling.healthMultiplier;
        HPText.text = _currentHealth.ToString("F1");
    }

    void OnEnable()
    {
        EnemyTakeDamage.OnEventRaised += TakeDamage;
    }

    void OnDisable()
    {
        EnemyTakeDamage.OnEventRaised -= TakeDamage;
    }

    void TakeDamage(GameObject GO, float amount)
    {
        if (GO != this.gameObject) return;

        _currentHealth -= amount;
        HPText.text = _currentHealth.ToString("F1");

        if (_currentHealth <= 0f)
        {
            OnEnemyKilled.RaiseEvent(GO);
        }
    }
}