using UnityEngine;

public class PlayerMovement : EntityMovement
{
    private GameManager _gameManager;
    private PlayerControls _playerControls;

    protected override void Awake()
    {
        base.Awake();

        _playerControls = new PlayerControls();
        _playerControls.Enable();
    }

    protected override void DetermineMovementVector()
    {
        if (!_gameManager.IsGameOver)
        {
            MovementVector = _playerControls.PlayerActionMap.MoveAction.ReadValue<Vector2>().normalized;
        }
        else
        {
            MovementVector = Vector2.zero;
        }
    }

    public void SetGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
}