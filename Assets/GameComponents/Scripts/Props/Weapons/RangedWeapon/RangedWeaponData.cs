using UnityEngine;

[CreateAssetMenu(fileName = "RangedWeapon", menuName = "ScriptableObjects/WeaponData/RangedWeaponData", order = 51)]

public class RangedWeaponData : ScriptableObject
{
    [field: SerializeField] public RangedWeapon.Type RangedWeaponType { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float ImpulseForce { get; private set; }
    [field: SerializeField] public float DropForce { get; private set; }
    [field: SerializeField] public int PenetrationDegree { get; private set; }
    [field: SerializeField] public int BulletsNumberPerShot { get; private set; }
    [field: SerializeField] public float AimingLineLength { get; private set; }
    [field: SerializeField] public float ShotsDelay { get; private set; }
    [field: SerializeField] public float DelayAfterShots { get; private set; }
    [field: SerializeField] public float DestructionTime { get; private set; }
    [field: SerializeField] public Sprite BulletSprite { get; private set; }
    [field: SerializeField] public Sprite HandSprite { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
}