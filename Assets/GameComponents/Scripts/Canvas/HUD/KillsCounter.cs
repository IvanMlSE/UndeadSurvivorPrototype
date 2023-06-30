using UnityEngine;
using UnityEngine.UI;

public class KillsCounter : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private Animator _killsCounterImageAnimator;
    [SerializeField] private GameManager _gameManager;

    private void Awake()
    {
        _text.text = "0";
    }

    private void OnEnable()
    {
        _gameManager.IncreasedKillsCounter += OnIncreasedKillsCounter;
    }

    private void OnDisable()
    {
        _gameManager.IncreasedKillsCounter -= OnIncreasedKillsCounter;
    }

    private void OnIncreasedKillsCounter(int value)
    {
        _text.text = value.ToString();

        _killsCounterImageAnimator.SetTrigger(KillsCounterImageAnimatorParameters.Increased);
    }

    private abstract class KillsCounterImageAnimatorParameters
    {
        public const string Increased = nameof(Increased);
    }
}