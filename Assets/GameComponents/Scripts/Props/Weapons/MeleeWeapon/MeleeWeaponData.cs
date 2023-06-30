using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeapon", menuName = "ScriptableObjects/WeaponData/MeleeWeaponData", order = 51)]

public class MeleeWeaponData : ScriptableObject
{
    [field: SerializeField] public MeleeWeapon.Type MeleeWeaponType { get; private set; }
    [field: SerializeField] public int BulletsNumber { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float DropForce { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; private set; }
    [field: SerializeField] public float RotationRadius { get; private set; }
    [field: SerializeField] public Sprite BulletSprite { get; private set; }
    [field: SerializeField] public Sprite HandSprite { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
}