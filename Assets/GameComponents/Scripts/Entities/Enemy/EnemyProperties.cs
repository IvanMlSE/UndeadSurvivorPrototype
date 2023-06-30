using UnityEngine;

[RequireComponent(typeof(EnemyReposition))]
[RequireComponent(typeof(EnemySound))]

public class EnemyProperties : EntityProperties
{
    [SerializeField] private EnemyPropertiesData[] _enemiesPropertiesDatas;

    private EnemyReposition _enemyReposition;
    private EnemySound _enemySound;

    public static int TotalEnemyTypes = System.Enum.GetValues(typeof(EnemyType)).Length;

    protected override void Awake()
    {
        base.Awake();

        _enemyReposition = GetComponent<EnemyReposition>();
        _enemySound = GetComponent<EnemySound>();
    }

    public void Initialize(Player player, GameManager gameManager, LayerMask enemyAreaLayer, BoxCollider2D enemyArea, EnemyType enemyType)
    {
        _enemyReposition.Initialize(player, enemyAreaLayer, enemyArea);

        (Entity as Enemy).SetPlayer(player);
        (Entity as Enemy).SetGameManager(gameManager);
        (EntityMovement as EnemyMovement).SetPlayer(player);
        (EntityMovement as EnemyMovement).SetGameManager(gameManager);
        _enemySound.SetGameManager(gameManager);

        InitializeEnemyType(enemyType);
    }

    public void InitializeEnemyType(EnemyType enemyType)
    {
        EnemyPropertiesData enemyPropertiesData = null;

        foreach (EnemyPropertiesData propertiesData in _enemiesPropertiesDatas)
        {
            if (propertiesData.EnemyType == enemyType)
            {
                enemyPropertiesData = propertiesData;

                break;
            }
        }

        if (enemyPropertiesData != null)
        {
            Entity.InitiateRevival(enemyPropertiesData.MaxHealth);
            EntityMovement.SetMovementSpeed(enemyPropertiesData.MovementSpeed);
            EntityRenderer.SetAnimatorController(enemyPropertiesData.RuntimeAnimatorController);

            (Entity as Enemy).SetDamage(enemyPropertiesData.Damage);
        }
    }
}