using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public float BaseDamage = 1;
    public float BaseHealth = 5;

    public float SpawnRate = 1f;
    public float SpawnRadius = 1f;
}
