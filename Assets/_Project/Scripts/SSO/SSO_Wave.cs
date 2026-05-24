using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public struct EnemySpawnConfig
{
    [Required] public GameObject enemyPrefab;
    [MinValue(1)] public int count;
}

[CreateAssetMenu(fileName = "SSO_Wave", menuName = "Data/SSO/SSO_Wave")]
public class SSO_Wave : ScriptableObject
{
    [Title("Wave Pacing")]
    [SuffixLabel("sec", true)]
    public float timeBetweenSpawns = 0.5f;

    [Title("Enemies Roster")]
    [TableList(ShowIndexLabels = true)]
    public List<EnemySpawnConfig> enemiesToSpawn = new List<EnemySpawnConfig>();

    [Title("Wave Rewards & Scaling")]
    [InfoBox("De combien on augmente la difficulté des ennemis pour la vague suivante ? (0.1 = +10%)")]
    public float nextWaveHealthScaling = 0.1f;
    public float nextWaveDamageScaling = 0.1f;
}