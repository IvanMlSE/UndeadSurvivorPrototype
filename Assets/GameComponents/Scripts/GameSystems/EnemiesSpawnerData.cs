using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesSpawnerData", menuName = "ScriptableObjects/GameSystems/EnemiesSpawnerData", order = 51)]

public class EnemiesSpawnerData : ScriptableObject
{
    [SerializeField] private PlayerType _playerType;
    [SerializeField] private SpawnLevelData[] _spawnLevelsData = new SpawnLevelData[1];

    public PlayerType PlayerType => _playerType;
    public int SpawnLevelsDataLength => _spawnLevelsData.Length;

    public SpawnLevelData GetSpawnLevelData(int spawnLevelDataIndex)
    {
        return _spawnLevelsData[spawnLevelDataIndex];
    }

    public void ResetCounters()
    {
        SpawnLevelData spawnLevelData;

        for (int SpawnLevelsDataIndex = 0; SpawnLevelsDataIndex < SpawnLevelsDataLength; SpawnLevelsDataIndex++)
        {
            spawnLevelData = GetSpawnLevelData(SpawnLevelsDataIndex);

            spawnLevelData.EnemiesNumberPerSpawnLevel = 0;

            for (int SpawnDataIndex = 0; SpawnDataIndex < spawnLevelData.SpawnDataLength; SpawnDataIndex++)
            {
                GetSpawnLevelData(SpawnLevelsDataIndex).GetSpawnData(SpawnDataIndex).EnemiesNumberOnSpawn = 0;
            }
        }
    }

    [System.Serializable]
    public class SpawnLevelData
    {
        [SerializeField] private SpawnData[] _spawnData = new SpawnData[1];

        public int EnemiesNumberPerSpawnLevel { get; set; }

        public int SpawnDataLength => _spawnData.Length;

        public SpawnData GetSpawnData(int spawnDataIndex)
        {
            return _spawnData[spawnDataIndex];
        }

        public int GetTotalEnemiesNumberPerSpawnLevel()
        {
            int totalEnemiesNumber = 0;

            foreach (SpawnData spawnData in _spawnData)
            {
                totalEnemiesNumber += spawnData.MaxEnemiesNumberOnSpawn;
            }

            return totalEnemiesNumber;
        }
    }

    [System.Serializable]
    public class SpawnData
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private int _maxEnemiesNumberOnSpawn;
        [SerializeField] private float _spawnPeriod;

        public int EnemiesNumberOnSpawn { get; set; }

        public EnemyType EnemyType => _enemyType;
        public int MaxEnemiesNumberOnSpawn => _maxEnemiesNumberOnSpawn;
        public float SpawnPeriod => _spawnPeriod;
    }
}