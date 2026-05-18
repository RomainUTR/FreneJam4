using Sirenix.OdinInspector;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Title("Configuration")]
    [Required] public GameObject EnemyPrefab;
    [Required, InlineEditor] public EnemySO Settings;

    [Title("References")]
    public Transform Player, ArenaContainer;

    private float _nextSpawnTime;

    void Update()
    {
        if (Time.time >= _nextSpawnTime)
        {
            SpawnEnemy();
            _nextSpawnTime = Time.time + Settings.SpawnRate;
        }
    }

    void SpawnEnemy()
    {
        if (Player == null) return;

        Vector2 randomCircle = Random.insideUnitCircle.normalized;

        float currentRadius = Settings.SpawnRadius * ArenaContainer.localScale.x;
        Vector3 spawnPos = new Vector3(randomCircle.x, 0, randomCircle.y) * currentRadius;

        spawnPos += Player.position;
        Instantiate(EnemyPrefab, spawnPos, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        if (Settings == null || ArenaContainer == null || Player == null)
        {
            Gizmos.color = Color.red;
            float currentRadius = Settings.SpawnRadius * ArenaContainer.localScale.x;

            Gizmos.DrawWireSphere(Player.position, currentRadius);
        } 
    }
}
