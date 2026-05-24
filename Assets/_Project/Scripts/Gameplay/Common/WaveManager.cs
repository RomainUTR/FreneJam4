using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Title("Wave Configuration")]
    [Required] public List<SSO_Wave> waves;
    [InfoBox("Glisse ici tous les Transform vides qui servent de points d'apparition dans ta scène.")]
    public Transform[] spawnPoints;

    [Title("Data & Events")]
    [Required] public RSO_EnemyScaling enemyScaling;
    [Required] public RSE_OnEnemyKilled OnEnemyKilled;
    [Required] public RSE_OnWaveCleared OnWaveCleared;
    [Required] public RSE_OnUpgradeFinished OnUpgradeFinished;
    [Required] public RSE_OnVictory OnVictory;

    [Title("Debug")]
    public TMP_Text ActiveMobCount, CurrentWaveIndex;

    private int _currentWaveIndex = 0;
    private int _activeEnemiesCount = 0;
    private bool _isSpawning = false;


    private void OnEnable()
    {
        OnEnemyKilled.OnEventRaised += HandleEnemyDeath;
        OnUpgradeFinished.OnEventRaised += StartNextWave;
    }

    private void OnDisable()
    {
        OnEnemyKilled.OnEventRaised -= HandleEnemyDeath;
        OnUpgradeFinished.OnEventRaised -= StartNextWave;
    }

    private void Start()
    {
        StartNextWave();
    }

    [Button("Force Start Next Wave")]
    public void StartNextWave()
    {
        if (_currentWaveIndex >= waves.Count)
        {
            Debug.Log("Toutes les vagues sont terminées !");
            return;
        }

        StartCoroutine(SpawnWaveRoutine(waves[_currentWaveIndex]));
    }

    IEnumerator SpawnWaveRoutine(SSO_Wave wave)
    {
        _isSpawning = true;
        CurrentWaveIndex.text = wave.name;

        foreach(EnemySpawnConfig spawnConfig in wave.enemiesToSpawn)
        {
            for (int i = 0; i < spawnConfig.count; i++)
            {
                Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

                Instantiate(spawnConfig.enemyPrefab, randomSpawn.position, randomSpawn.rotation);

                _activeEnemiesCount++;
                ActiveMobCount.text = _activeEnemiesCount.ToString();

                yield return new WaitForSeconds(wave.timeBetweenSpawns);
            }
        }

        _isSpawning = false;

        CheckWaveCompletion();
    }

    private void HandleEnemyDeath(GameObject go)
    {
        _activeEnemiesCount--;
        CheckWaveCompletion();
    }

    void CheckWaveCompletion()
    {
        if (!_isSpawning && _activeEnemiesCount <= 0)
        {
            if (_currentWaveIndex >= waves.Count -1)
            {
                Debug.Log("Toutes les vagues sont terminées ! VICTOIRE !");
                OnVictory?.RaiseEvent();
                return;
            }

            SSO_Wave completedWave = waves[_currentWaveIndex];

            enemyScaling.AddHealthScaling(completedWave.nextWaveHealthScaling);
            enemyScaling.AddDamageScaling(completedWave.nextWaveDamageScaling);

            Debug.Log($"Vague {_currentWaveIndex + 1} terminée !");

            _currentWaveIndex++;

            OnWaveCleared?.RaiseEvent();
        }
    }
}