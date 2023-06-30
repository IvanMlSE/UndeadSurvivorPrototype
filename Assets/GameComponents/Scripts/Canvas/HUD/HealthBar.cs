using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private float _criticalHealthImportance;
    [SerializeField] private Slider _slider;
    [SerializeField] private Animator _healthBarFillAnimator;
    [SerializeField] private Player _player;

    private void Awake()
    {
        _slider.value = _slider.maxValue;
    }

    private void OnEnable()
    {
        _player.Hited += OnHealthChanged;
        _player.HealthIncreased += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.Hited -= OnHealthChanged;
        _player.HealthIncreased -= OnHealthChanged;
    }

    private void OnHealthChanged()
    {
        _slider.value = _player.CurrentHealth / _player.MaxHealth;

        _healthBarFillAnimator.SetBool(HealthBarFillAnimatorParameters.IsFlashing, false);

        if (_slider.value <= _criticalHealthImportance)
        {
            _healthBarFillAnimator.SetBool(HealthBarFillAnimatorParameters.IsFlashing, true);
        }
    }

    private abstract class HealthBarFillAnimatorParameters
    {
        public const string IsFlashing = nameof(IsFlashing);
    }
}