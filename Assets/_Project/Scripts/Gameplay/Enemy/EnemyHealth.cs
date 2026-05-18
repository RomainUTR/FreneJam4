using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Settings")]
    public EnemySO enemySO;

    //[Header("References")]

    [Header("Input")]
    public RSE_EnemyTakeDamage EnemyTakeDamage;
    public TMP_Text HPText;

    [Header("Output")]
    private float _currentHealth;
    public RSE_OnEnemyKilled OnEnemyKilled;

    void Awake()
    {
        _currentHealth = enemySO.BaseHealth;
        HPText.text = _currentHealth.ToString();
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
        HPText.text = _currentHealth.ToString();

        if (_currentHealth <= 0f)
        {
            OnEnemyKilled.RaiseEvent(GO);
        }
    }
}