using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private LayerMask _playerLayerMask;

    private float _damage;
    private Player _player;
    private GameManager _gameManager;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.otherCollider.IsTouchingLayers(_playerLayerMask) && !_gameManager.IsGameOver)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        _player.GetDamage(_damage * Time.deltaTime);
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public void SetGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    protected override void InitiateDeath()
    {
        base.InitiateDeath();

        _gameManager.IncrementKillsCounter();
    }
}