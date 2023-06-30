using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]

public class HUD : MonoBehaviour
{
    [SerializeField] private float _smoothingChangingCanvasGroupAlphaValue;

    private CanvasGroup _canvasGroup;
    private Coroutine _coroutine;

    private const float MinValueCanvasGroupAlpha = 0;
    private const float MaxValueCanvasGroupAlpha = 1f;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void ChangeSmoothlyCanvasGroupAlpha(float targetValue)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(ChangeSmoothlyCanvasGroupAlphaCoroutine(targetValue));
    }

    private IEnumerator ChangeSmoothlyCanvasGroupAlphaCoroutine(float targetValue)
    {
        while (_canvasGroup.alpha != targetValue)
        {
            _canvasGroup.alpha = Mathf.MoveTowards(_canvasGroup.alpha, targetValue, _smoothingChangingCanvasGroupAlphaValue);

            yield return null;
        }
    }

    public void ShowCanvasGroup()
    {
        ChangeSmoothlyCanvasGroupAlpha(MaxValueCanvasGroupAlpha);

        _canvasGroup.blocksRaycasts = true;
    }

    public void HideCanvasGroup()
    {
        ChangeSmoothlyCanvasGroupAlpha(MinValueCanvasGroupAlpha);

        _canvasGroup.blocksRaycasts = false;
    }
}