using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgression : MonoBehaviour
{
    [SerializeField] private float _smoothingChangingValue;
    [SerializeField] private Slider _slider;
    [SerializeField] private GameManager _gameManager;

    private Coroutine _coroutine;

    private void Awake()
    {
        _slider.value = _slider.minValue;
    }

    private void OnEnable()
    {
        _gameManager.IncreasedLevelProgression += OnIncreasedLevelProgression;
    }

    private void OnDisable()
    {
        _gameManager.IncreasedLevelProgression -= OnIncreasedLevelProgression;
    }

    private void OnIncreasedLevelProgression(float targetValue)
    {
        ChangeSmoothlySliderValue(targetValue);
    }

    private void ChangeSmoothlySliderValue(float targetValue)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(ChangeSmoothlySliderValueCoroutine(targetValue));
    }

    private IEnumerator ChangeSmoothlySliderValueCoroutine(float targetValue)
    {
        while (_slider.value != targetValue)
        {
            SetNewSliderValue(targetValue);

            yield return null;
        }
    }

    private void SetNewSliderValue(float targetValue)
    {
        if (targetValue < _slider.value)
        {
            _slider.value = _slider.minValue;
        }

        _slider.value = Mathf.Lerp(_slider.value, targetValue, _smoothingChangingValue);
    }
}