using System.Collections;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    [SerializeField] private float _shotVolume;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private Player _player;
    [SerializeField] private EnemyScanner _enemyScanner;
    [SerializeField] private SpriteRenderer _rightHandSprite;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private RangedWeaponData[] _dataOnRangedWeapons;

    private AudioSource _audioSource;
    private GameManager _gameManager;
    private Coroutine _coroutine;

    private void Awake()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();

        _audioSource.volume = _shotVolume;
        _audioSource.clip = _audioClip;
        _audioSource.playOnAwake = false;
    }

    public void StopShooting()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private void Shoot(float damage, float impulseForce, float dropForce, int penetrationDegree, int bulletsNumberPerShot, float aimingLineLength,
        float shotsDelay, float delayAfterShots, float destructionTime, Sprite bulletSprite, GameManager gameManager)
    {
        StopShooting();

        _coroutine = StartCoroutine(ShootCoroutine(damage, impulseForce, dropForce, penetrationDegree, bulletsNumberPerShot, aimingLineLength,
            shotsDelay, delayAfterShots, destructionTime, bulletSprite, gameManager));
    }

    private IEnumerator ShootCoroutine(float damage, float impulseForce, float dropForce, int penetrationDegree, int bulletsNumberPerShot, float aimingLineLength,
        float shotsDelay, float delayAfterShots, float destructionTime, Sprite bulletSprite, GameManager gameManager)
    {
        Transform target;
        WaitForSeconds shotsDelayForSeconds = new WaitForSeconds(shotsDelay);
        WaitForSeconds delayAfterShotsForSeconds = new WaitForSeconds(delayAfterShots);

        while (_player.IsLife && !_gameManager.IsGameOver)
        {
            target = _enemyScanner.GetNearestEnemy();

            if (target != null)
            {
                Vector2 targetPosition = target.position;
                Vector2 position;
                Vector2 bulletDirection;
                Vector2 aimingLine;
                Quaternion quaternionRotation;

                for (int i = 0; i < bulletsNumberPerShot; i++)
                {
                    position = transform.position;
                    bulletDirection = (targetPosition - position).normalized;
                    aimingLine = Vector2.Perpendicular(bulletDirection) * Random.Range(-1f * aimingLineLength, aimingLineLength);

                    bulletDirection += aimingLine;

                    quaternionRotation = Quaternion.FromToRotation(Vector2.up, bulletDirection);

                    Bullet bullet = Instantiate(_bullet, position, quaternionRotation, transform);
                    bullet.Initialize(damage, dropForce, bulletSprite, gameManager, penetrationDegree, destructionTime, Bullet.Type.Ranged);
                    bullet.AddImpulseForce(bulletDirection, impulseForce);

                    _audioSource.Play();

                    yield return shotsDelayForSeconds;
                }
            }

            yield return delayAfterShotsForSeconds;
        }
    }

    public void InitializeType(Type type, GameManager gameManager)
    {
        _gameManager = gameManager;

        RangedWeaponData rangedWeaponData = null;

        foreach (RangedWeaponData weaponData in _dataOnRangedWeapons)
        {
            if (weaponData.RangedWeaponType == type)
            {
                rangedWeaponData = weaponData;

                break;
            }
        }

        if (rangedWeaponData != null)
        {
            _rightHandSprite.gameObject.SetActive(true);
            _rightHandSprite.sprite = rangedWeaponData.HandSprite;

            Shoot(rangedWeaponData.Damage, rangedWeaponData.ImpulseForce, rangedWeaponData.DropForce, rangedWeaponData.PenetrationDegree,
                rangedWeaponData.BulletsNumberPerShot, rangedWeaponData.AimingLineLength, rangedWeaponData.ShotsDelay, rangedWeaponData.DelayAfterShots,
                rangedWeaponData.DestructionTime, rangedWeaponData.BulletSprite, gameManager);
        }
    }

    public enum Type
    {
        SingleShot,
        Fraction,
        Automatic
    }
}