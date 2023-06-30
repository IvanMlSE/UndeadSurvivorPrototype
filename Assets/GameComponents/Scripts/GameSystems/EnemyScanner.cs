using UnityEngine;

public class EnemyScanner : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayerMask;
    [SerializeField] private CircleCollider2D _circleCollider2D;

    private RaycastHit2D[] _detectedEnemies;

    public Transform GetNearestEnemy()
    {
        _detectedEnemies = Physics2D.CircleCastAll(transform.position, _circleCollider2D.radius, Vector2.zero, 0, _enemyLayerMask);

        Transform nearestEnemy = null;

        if (_detectedEnemies.Length > 0)
        {
            float minEnemyDistance = _circleCollider2D.radius;
            float enemyDistance;

            foreach (RaycastHit2D detectedEnemy in _detectedEnemies)
            {
                if (detectedEnemy.collider.GetComponent<Enemy>().IsLife)
                {
                    enemyDistance = Vector2.Distance(transform.position, detectedEnemy.point);

                    if (enemyDistance < minEnemyDistance)
                    {
                        minEnemyDistance = enemyDistance;

                        nearestEnemy = detectedEnemy.transform;
                    }
                }
            }
        }

        _detectedEnemies = new RaycastHit2D[0];

        return nearestEnemy;
    }
}