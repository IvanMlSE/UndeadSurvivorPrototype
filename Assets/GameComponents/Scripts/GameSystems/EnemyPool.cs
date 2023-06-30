using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private EnemyProperties _enemyPrefab;
    [SerializeField] private Player _player;
    [SerializeField] private LayerMask _enemyAreaLayer;
    [SerializeField] private BoxCollider2D _enemyArea;
    [SerializeField] private GameManager _gameManager;

    private List<EnemyProperties>[] _enemies;

    private void Awake()
    {
        _enemies = new List<EnemyProperties>[EnemyProperties.TotalEnemyTypes];

        for (int i = 0; i < _enemies.Length; i++)
        {
            _enemies[i] = new List<EnemyProperties>();
        }
    }

    public void ReleaseEnemy(EnemyType enemyType)
    {
        int enemyTypeIndex = (int)enemyType;

        EnemyProperties returnedEnemy = null;

        if (_enemies[enemyTypeIndex].Count > 0)
        {
            foreach (EnemyProperties enemy in _enemies[enemyTypeIndex])
            {
                if (enemy.gameObject.activeSelf == false)
                {
                    returnedEnemy = RestoreEnemy(enemy, enemyType);

                    break;
                }
            }
        }

        if (returnedEnemy == null)
        {
            returnedEnemy = CreateEnemy(enemyType);

            _enemies[enemyTypeIndex].Add(returnedEnemy);
        }
    }

    private EnemyProperties CreateEnemy(EnemyType enemyType)
    {
        EnemyProperties enemy = Instantiate(_enemyPrefab, GetRandomPosition(), Quaternion.identity, transform);

        enemy.Initialize(_player, _gameManager, _enemyAreaLayer, _enemyArea, enemyType);

        return enemy;
    }

    private EnemyProperties RestoreEnemy(EnemyProperties enemy, EnemyType enemyType)
    {
        enemy.gameObject.SetActive(true);
        enemy.transform.position = GetRandomPosition();

        enemy.InitializeEnemyType(enemyType);

        return enemy;
    }

    private Vector2 GetRandomPosition()
    {
        const int areaDivider = 2;

        float randomPointByX = Random.Range(-1f * _enemyArea.size.x, _enemyArea.size.x) / areaDivider;
        float randomPointByY = Random.Range(-1f * _enemyArea.size.y, _enemyArea.size.y) / areaDivider;

        Vector2 spawnPosition = _player.transform.position;

        if (randomPointByX > randomPointByY)
        {
            if (randomPointByX > 0)
            {
                spawnPosition += new Vector2(_enemyArea.size.x / areaDivider, randomPointByY);
            }
            else
            {
                spawnPosition += new Vector2(-1f * _enemyArea.size.x / areaDivider, randomPointByY);
            }
        }
        else
        {
            if (randomPointByY > 0)
            {
                spawnPosition += new Vector2(randomPointByX, _enemyArea.size.y / areaDivider);
            }
            else
            {
                spawnPosition += new Vector2(randomPointByX, -1f * _enemyArea.size.y / areaDivider);
            }
        }

        return spawnPosition;
    }

    public void KillAllEnemies()
    {
        const float MaxDamage = 1000000f;

        foreach (List<EnemyProperties> enemies in _enemies)
        {
            foreach (EnemyProperties enemy in enemies)
            {
                enemy.GetComponent<Enemy>().GetDamage(MaxDamage);
            }
        }
    }
}