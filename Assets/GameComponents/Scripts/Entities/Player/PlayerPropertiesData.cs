using UnityEngine;

[CreateAssetMenu(fileName = "Farmer", menuName = "ScriptableObjects/EntityPropertiesData/PlayerPropertiesData", order = 51)]

public class PlayerPropertiesData : ScriptableObject
{
    [field: SerializeField] public PlayerType PlayerType { get; private set; }
    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public WeaponVariant WeaponVariant { get; private set; }
    [field: SerializeField] public RuntimeAnimatorController RuntimeAnimatorController { get; private set; }
}