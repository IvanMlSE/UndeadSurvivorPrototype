using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]

public class PlayerProperties : EntityProperties
{
    [SerializeField] private PlayerWeapons _playerWeapons;
    [SerializeField] private PlayerPropertiesData[] _playersPropertiesData;

    private PlayerMovement _playerMovement;

    protected override void Awake()
    {
        base.Awake();

        _playerMovement = GetComponent<PlayerMovement>();
    }

    public void InitializePlayerType(PlayerType playerType, GameManager gameManager)
    {
        PlayerPropertiesData playerPropertiesData = null;

        foreach (PlayerPropertiesData propertiesData in _playersPropertiesData)
        {
            if (propertiesData.PlayerType == playerType)
            {
                playerPropertiesData = propertiesData;

                break;
            }
        }

        if (playerPropertiesData != null)
        {
            Entity.InitiateRevival(playerPropertiesData.MaxHealth);
            EntityMovement.SetMovementSpeed(playerPropertiesData.MovementSpeed);
            EntityRenderer.SetAnimatorController(playerPropertiesData.RuntimeAnimatorController);

            _playerMovement.SetGameManager(gameManager);
            _playerWeapons.Initialize(playerPropertiesData.WeaponVariant, gameManager);
        }
    }
}