using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _volume;
    [SerializeField] private AudioClip[] _impactSounds;

    private float _damage;
    private float _dropForce;
    private int _penetrationDegree;
    private Enemy _attackTarget;
    private Type _bulletType;
    private SpriteRenderer _spriteRenderer;
    private CapsuleCollider2D _capsuleCollider2D;
    private Rigidbody2D _rigidbody2D;
    private AudioSource _audioSource;
    private GameManager _gameManager;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _audioSource = gameObject.AddComponent<AudioSource>();

        _audioSource.volume = _volume;
        _audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out _attackTarget) && _attackTarget.IsLife && !_gameManager.IsGameOver)
        {
            _attackTarget.GetComponent<EnemyMovement>().SetDropForce(_dropForce);
            _attackTarget.GetDamage(_damage);

            if (_bulletType == Type.Melee)
            {
                _audioSource.Stop();
                _audioSource.clip = _impactSounds[Random.Range(0, _impactSounds.Length)];
                _audioSource.Play();
            }

            if (_penetrationDegree > 0)
            {
                _penetrationDegree--;

                if (_penetrationDegree == 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void Initialize(float damage, float dropForce, Sprite sprite, GameManager gameManager, int penetrationDegree = 0, float destructionTime = 0, Type bulletType = Type.Melee)
    {
        _capsuleCollider2D = gameObject.AddComponent<CapsuleCollider2D>();
        _capsuleCollider2D.isTrigger = true;

        if (damage > 0)
        {
            _damage = damage;
        }
        
        if (dropForce > 0)
        {
            _dropForce = dropForce;
        }

        _spriteRenderer.sprite = sprite;

        _gameManager = gameManager;

        _bulletType = bulletType;

        if (bulletType == Type.Ranged)
        {
            _rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
            _rigidbody2D.gravityScale = 0;

            if (penetrationDegree > 0)
            {
                _penetrationDegree = penetrationDegree;
            }

            if (destructionTime > 0)
            {
                Destroy(gameObject, destructionTime);
            }
        }
    }

    public void AddImpulseForce(Vector2 bulletDirection, float impulseForce)
    {
        _rigidbody2D.AddForce(bulletDirection * impulseForce, ForceMode2D.Impulse);
    }

    public enum Type
    {
        Melee,
        Ranged
    }
}