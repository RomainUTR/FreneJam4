using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public int Damage = 1;

    public float SpawnRate = 1f;
    public float SpawnRadius = 1f;
}
