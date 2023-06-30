using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]

public class EndGame : MonoBehaviour
{
    [SerializeField] private float _smoothingChangingCanvasGroupAlphaValue;
    [SerializeField] private Image VictoryTitle;
    [SerializeField] private Image DefeatTitle;
    [SerializeField] private StartGame _startGame;
    [SerializeField] private HUD _hud;

    private CanvasGroup _canvasGroup;
    private Coroutine _coroutine;

    private const float MinValueCanvasGroupAlpha = 0;
    private const float MaxValueCanvasGroupAlpha = 1f;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowGameResult(bool isVictory)
    {
        ShowCanvasGroup();

        if (isVictory)
        {
            VictoryTitle.gameObject.SetActive(true);
            DefeatTitle.gameObject.SetActive(false);
        }
        else
        {
            DefeatTitle.gameObject.SetActive(true);
            VictoryTitle.gameObject.SetActive(false);
        }
    }

    private void ShowCanvasGroup()
    {
        ChangeSmoothlyCanvasGroupAlpha(MaxValueCanvasGroupAlpha);

        _canvasGroup.blocksRaycasts = true;

        _hud.HideCanvasGroup();
    }

    public void HideCanvasGroup()
    {
        ChangeSmoothlyCanvasGroupAlpha(MinValueCanvasGroupAlpha);

        _canvasGroup.blocksRaycasts = false;

        _startGame.ShowCanvasGroup();
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

    public void Exit()
    {
        Application.Quit();
    }
}