using UnityEngine;
using UnityEngine.UI;

public class SpawnInfo : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private Animator _animator;
    [SerializeField] private Spawner _spawner;

    private void OnEnable()
    {
        _spawner.IncreasedSpawnLevel += OnIncreasedSpawnLevel;
    }

    private void OnDisable()
    {
        _spawner.IncreasedSpawnLevel -= OnIncreasedSpawnLevel;
    }

    private void OnIncreasedSpawnLevel(int spawnLevel)
    {
        string info = $"{spawnLevel + 1} волна";

        _text.text = info;

        _animator.SetTrigger(AnimatorParameters.Flashing);
    }

    private abstract class AnimatorParameters
    {
        public const string Flashing = nameof(Flashing);
    }
}