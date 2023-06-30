using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private Player _player;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private EnemiesSpawnerData[] _enemiesSpawnersData;

    private int _spawnLevel;
    private EnemiesSpawnerData _enemiesSpawnerData;
    private EnemiesSpawnerData.SpawnLevelData _spawnLevelData;

    public int TotalEnemiesNumber { get; private set; }

    public event UnityAction<int> IncreasedSpawnLevel;

    public void ChooseEnemiesSpawnerData(PlayerType playerType)
    {
        foreach (EnemiesSpawnerData enemiesSpawnerData in _enemiesSpawnersData)
        {
            if (enemiesSpawnerData.PlayerType == playerType)
            {
                _enemiesSpawnerData = enemiesSpawnerData;

                break;
            }
        }

        _enemiesSpawnerData.ResetCounters();
    }

    public void ResetSpawnLevel()
    {
        TotalEnemiesNumber = 0;
        _spawnLevel = 0;

        IncreasedSpawnLevel?.Invoke(_spawnLevel);
    }

    public void SpawnEnemy()
    {
        EnemiesSpawnerData.SpawnData spawnData;

        _spawnLevelData = _enemiesSpawnerData.GetSpawnLevelData(_spawnLevel);

        for (int spawnDataIndex = 0; spawnDataIndex < _spawnLevelData.SpawnDataLength && _player.IsLife && !_gameManager.IsGameOver; spawnDataIndex++)
        {
            spawnData = _spawnLevelData.GetSpawnData(spawnDataIndex);

            StartCoroutine(SpawnEnemyCoroutine(spawnData));
        }
    }

    private IEnumerator SpawnEnemyCoroutine(EnemiesSpawnerData.SpawnData spawnData)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(spawnData.SpawnPeriod);

        while (spawnData.EnemiesNumberOnSpawn < spawnData.MaxEnemiesNumberOnSpawn && _player.IsLife && !_gameManager.IsGameOver)
        {
            _enemyPool.ReleaseEnemy(spawnData.EnemyType);

            spawnData.EnemiesNumberOnSpawn++;

            _spawnLevelData.EnemiesNumberPerSpawnLevel++;

            TotalEnemiesNumber++;

            yield return waitForSeconds;

            IncreaseSpawnLevel();
        }
    }

    private void IncreaseSpawnLevel()
    {
        if (_spawnLevelData.EnemiesNumberPerSpawnLevel >= _spawnLevelData.GetTotalEnemiesNumberPerSpawnLevel())
        {
            _spawnLevelData.EnemiesNumberPerSpawnLevel = 0;

            _spawnLevel++;

            if (_spawnLevel < _enemiesSpawnerData.SpawnLevelsDataLength)
            {
                IncreasedSpawnLevel?.Invoke(_spawnLevel);
            }

            _spawnLevel = Mathf.Clamp(_spawnLevel, 0, _enemiesSpawnerData.SpawnLevelsDataLength - 1);

            SpawnEnemy();
        }
    }
}