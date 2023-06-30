using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private SpriteRenderer _leftHandSprite;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private MeleeWeaponData[] _dataOnMeleeWeapons;

    private List<Bullet> _bullets;
    private Coroutine _coroutine;

    private void Awake()
    {
        _bullets = new List<Bullet>();
    }

    private void OnEnable()
    {
        _player.Died += RemoveBullets;
        _player.Revived += RemoveBullets;
    }

    private void OnDisable()
    {
        _player.Died -= RemoveBullets;
        _player.Revived -= RemoveBullets;
    }

    private void AddBullet(float damage, float dropForce, float rotationRadius, Sprite bulletSprite, GameManager gameManager)
    {
        Bullet bullet = Instantiate(_bullet, transform);
        bullet.Initialize(damage, dropForce, bulletSprite, gameManager);

        _bullets.Add(bullet);

        ArrangeBulletsInCircle(rotationRadius);
    }

    private void AddBullets(int bulletsNumber, float damage, float dropForce, float rotationRadius, Sprite bulletSprite, GameManager gameManager)
    {
        for (int i = 0; i < bulletsNumber; i++)
        {
            AddBullet(damage, dropForce, rotationRadius, bulletSprite, gameManager);
        }
    }

    private void RemoveBullets()
    {
        if (_bullets.Count > 0)
        {
            for (int i = 0; i < _bullets.Count; i++)
            {
                Destroy(_bullets[i].gameObject);
            }

            _bullets.Clear();
        }
    }

    private void ArrangeBulletsInCircle(float rotationRadius)
    {
        const int FullCircleAngle = 360;

        for (int bulletIndex = 0; bulletIndex < _bullets.Count; bulletIndex++)
        {
            _bullets[bulletIndex].transform.rotation = Quaternion.identity;
            _bullets[bulletIndex].transform.Rotate(Vector3.forward * FullCircleAngle * bulletIndex / _bullets.Count);

            _bullets[bulletIndex].transform.position = transform.position;
            _bullets[bulletIndex].transform.Translate(Vector3.up * rotationRadius);
        }
    }

    public void StopRotateBase()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private void RotateBase(float rotationSpeed)
    {
        StopRotateBase();

        _coroutine = StartCoroutine(RotateBaseCoroutine(rotationSpeed));
    }

    private IEnumerator RotateBaseCoroutine(float rotationSpeed)
    {
        while (_player.IsLife)
        {
            transform.Rotate(-1f * Vector3.forward * rotationSpeed * Time.deltaTime);

            yield return null;
        }
    }

    public void InitializeType(Type type, GameManager gameManager)
    {
        MeleeWeaponData meleeWeaponData = null;

        foreach (MeleeWeaponData weaponData in _dataOnMeleeWeapons)
        {
            if (weaponData.MeleeWeaponType == type)
            {
                meleeWeaponData = weaponData;

                break;
            }
        }

        if (meleeWeaponData != null)
        {
            _leftHandSprite.gameObject.SetActive(true);
            _leftHandSprite.sprite = meleeWeaponData.HandSprite;

            AddBullets(meleeWeaponData.BulletsNumber, meleeWeaponData.Damage, meleeWeaponData.DropForce, meleeWeaponData.RotationRadius, meleeWeaponData.BulletSprite, gameManager);
            RotateBase(meleeWeaponData.RotationSpeed);
        }
    }

    public enum Type
    {
        Shovel,
        Pitchfork,
        Hoe
    }
}