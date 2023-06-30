using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] private MeleeWeapon _meleeWeapon;
    [SerializeField] private RangedWeapon _rangedWeapon;
    [SerializeField] private SpriteRenderer _leftHandSprite;
    [SerializeField] private SpriteRenderer _rightHandSprite;

    public void Initialize(WeaponVariant weaponVariant, GameManager gameManager)
    {
        _leftHandSprite.gameObject.SetActive(false);
        _rightHandSprite.gameObject.SetActive(false);

        _meleeWeapon.StopRotateBase();
        _rangedWeapon.StopShooting();

        SetWeapon(weaponVariant, gameManager);
    }

    public void SetWeapon(WeaponVariant weaponVariant, GameManager gameManager)
    {
        switch (weaponVariant)
        {
            case WeaponVariant.Shovel:
                _meleeWeapon.InitializeType(MeleeWeapon.Type.Shovel, gameManager);
                break;

            case WeaponVariant.Pitchfork:
                _meleeWeapon.InitializeType(MeleeWeapon.Type.Pitchfork, gameManager);
                break;

            case WeaponVariant.Hoe:
                _meleeWeapon.InitializeType(MeleeWeapon.Type.Hoe, gameManager);
                break;

            case WeaponVariant.SingleShot:
                _rangedWeapon.InitializeType(RangedWeapon.Type.SingleShot, gameManager);
                break;

            case WeaponVariant.Fraction:
                _rangedWeapon.InitializeType(RangedWeapon.Type.Fraction, gameManager);
                break;

            case WeaponVariant.Automatic:
                _rangedWeapon.InitializeType(RangedWeapon.Type.Automatic, gameManager);
                break;
        }
    }
}