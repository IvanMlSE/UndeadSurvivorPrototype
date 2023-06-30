using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class EnemyReposition : Reposition
{
    private Enemy _enemy;
    private BoxCollider2D _enemyArea;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    protected override void Reposit()
    {
        base.Reposit();

        const int areaDivider = 2;
        Vector2 randomPosition = new Vector2(Random.Range(-1f * _enemyArea.size.x, _enemyArea.size.x), Random.Range(-1f * _enemyArea.size.y, _enemyArea.size.y)) / areaDivider;

        if (Player.IsLife && _enemy.IsLife)
        {
            if (PlayerAbsoluteDirection.x > PlayerAbsoluteDirection.y)
            {
                transform.Translate(PlayerDirection + new Vector2(PositionRelativePlayer.x * _enemyArea.size.x / areaDivider, randomPosition.y));
            }
            else if (PlayerAbsoluteDirection.x < PlayerAbsoluteDirection.y)
            {
                transform.Translate(PlayerDirection + new Vector2(randomPosition.x, PositionRelativePlayer.y * _enemyArea.size.y / areaDivider));
            }
            else
            {
                transform.Translate(PlayerDirection + new Vector2(new Vector2(PositionRelativePlayer.x * _enemyArea.size.x / areaDivider, randomPosition.y).x,
                    new Vector2(randomPosition.x, PositionRelativePlayer.y * _enemyArea.size.y / areaDivider).y));
            }
        }
    }

    public void Initialize(Player player, LayerMask enemyAreaLayer, BoxCollider2D enemyArea)
    {
        Initialize(player, enemyAreaLayer);

        _enemyArea = enemyArea;
    }
}