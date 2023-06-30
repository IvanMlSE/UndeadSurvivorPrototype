using UnityEngine;
using UnityEngine.UI;

public class LevelCounter : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameManager _gameManager;

    private void Awake()
    {
        _text.text = "0";
    }

    private void OnEnable()
    {
        _gameManager.IncreasedLevelCounter += OnIncreasedLevel;
    }

    private void OnDisable()
    {
        _gameManager.IncreasedLevelCounter -= OnIncreasedLevel;
    }

    private void OnIncreasedLevel(int value)
    {
        _text.text = value.ToString();

        _animator.SetTrigger(LevelCounterTextAnimatorParameters.Increased);
    }

    private abstract class LevelCounterTextAnimatorParameters
    {
        public const string Increased = nameof(Increased);
    }
}