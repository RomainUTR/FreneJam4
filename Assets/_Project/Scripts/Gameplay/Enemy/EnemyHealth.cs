using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Settings")]
    public ProjectileStatsSO projectileStatsSO;
    public EnemySO enemySO;

    //[Header("References")]

    [Header("Input")]
    public RSE_EnemyTakeDamage EnemyTakeDamage;
    public TMP_Text HPText;

    [Header("Output")]
    private float _currentHealth;

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

    void TakeDamage(float amount)
    {
        _currentHealth -= projectileStatsSO.damage;
        HPText.text = _currentHealth.ToString();

        if (_currentHealth <= 0f)
        {
            Debug.Log($"Enemy {this.gameObject.name} is dead");
        }
    }
}