using System.Collections;
using UnityEngine;

public class EnemyMovement : EntityMovement
{
    [SerializeField] private float _percentageSpeedSpread;

    private GameManager _gameManager;
    private float _dropForce;
    private Player _player;
    private Coroutine _coroutine;
    private WaitForSeconds _waitForSeconds;

    private const float DelayAfterDropping = 0.1f;

    protected override void Awake()
    {
        base.Awake();

        _waitForSeconds = new WaitForSeconds(DelayAfterDropping);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Entity.Hited += GetHit;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        Entity.Hited -= GetHit;
    }

    private void GetHit()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(GetHitCoroutine());
    }

    private IEnumerator GetHitCoroutine()
    {
        Rigidbody2D.AddForce(MovementVector * _dropForce, ForceMode2D.Impulse);

        yield return _waitForSeconds;

        Entity.ResetHit();
    }

    protected override void DetermineMovementVector()
    {
        MovementVector = (_player.transform.position - transform.position).normalized;
    }

    protected override void Run()
    {
        if (!Entity.IsHit)
        {
            if (_player.IsLife && !_gameManager.IsGameOver)
            {
                base.Run();
            }
            else
            {
                Rigidbody2D.velocity = Vector2.zero;
            }
        }

    }

    public void SetGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public override void SetMovementSpeed(float movementSpeed)
    {
        float minMovementSpeed = movementSpeed * (1 - _percentageSpeedSpread);
        float maxMovementSpeed = movementSpeed * (1 + _percentageSpeedSpread);

        movementSpeed = Random.Range(minMovementSpeed, maxMovementSpeed);

        base.SetMovementSpeed(movementSpeed);
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    public void SetDropForce(float dropForce)
    {
        _dropForce = -1f * dropForce;
    }
}