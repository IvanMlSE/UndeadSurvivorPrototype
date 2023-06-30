using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/EntityPropertiesData/EnemyPropertiesData", order = 51)]

public class EnemyPropertiesData : ScriptableObject
{
    [field: SerializeField] public EnemyType EnemyType { get; private set; }
    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public RuntimeAnimatorController RuntimeAnimatorController { get; private set; }
}