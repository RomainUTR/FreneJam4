using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "RSO_EnemyScaling", menuName = "Data/RSO/RSO_EnemyScaling")]
public class RSO_EnemyScaling : ScriptableObject
{
    [Title("Runtime Scaling (Live Data)")]
    [InfoBox("Multiplicateurs globaux appliqués à tous les ennemis. 1 = 100% (Stats de base).")]

    [ReadOnly] public float healthMultiplier = 1f;
    [ReadOnly] public float damageMultiplier = 1f;
    [ReadOnly] public float speedMultiplier = 1f;

    public void Initialize()
    {
        healthMultiplier = 1f;
        damageMultiplier = 1f;
        speedMultiplier = 1f;
    }

    public void AddHealthScaling(float percentageToAdd)
    {
        healthMultiplier += percentageToAdd;
    }

    public void AddDamageScaling(float percentageToAdd)
    {
        damageMultiplier += percentageToAdd;
    }
}