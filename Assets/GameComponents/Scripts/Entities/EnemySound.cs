using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class EnemySound : MonoBehaviour
{
    [SerializeField] private float _volume;
    [SerializeField] private AudioClip[] _hitSounds;
    [SerializeField] private AudioClip _deathSound;

    private AudioSource _audioSource;
    private Enemy _enemy;
    private GameManager _gameManager;

    private void Awake()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();

        _audioSource.playOnAwake = false;
        _audioSource.volume = _volume;

        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _enemy.Hited += OnEntityHited;
        _enemy.Died += OnEntityDied;
    }

    private void OnDisable()
    {
        _enemy.Hited -= OnEntityHited;
        _enemy.Died -= OnEntityDied;
    }

    private void OnEntityHited()
    {
        _audioSource.Stop();

        if (!_gameManager.IsGameOver)
        {
            _audioSource.clip = _hitSounds[Random.Range(0, _hitSounds.Length)];
            _audioSource.Play();
        }
    }

    private void OnEntityDied()
    {
        _audioSource.Stop();

        if (!_gameManager.IsGameOver)
        {
            _audioSource.clip = _deathSound;
            _audioSource.Play();
        }
    }

    public void SetGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
}