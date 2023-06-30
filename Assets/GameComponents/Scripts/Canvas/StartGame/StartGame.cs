using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]

public class StartGame : MonoBehaviour
{
    [SerializeField] private float _selectingDelay;
    [SerializeField] private float _smoothingChangingCanvasGroupAlphaValue;
    [SerializeField] private GameAmbient _gameAmbient;
    [SerializeField] private GridLayoutGroup _playersChoice;
    [SerializeField] private PlayerChoiceButton _playerChoiceButtonPrefab;
    [SerializeField] private HUD _hud;

    private List<PlayerChoiceButton> _playerChoiceButtons = new List<PlayerChoiceButton>();
    private CanvasGroup _canvasGroup;
    private Coroutine _coroutine;

    public event UnityAction<PlayerType> PlayerSelected;

    private const float MinValueCanvasGroupAlpha = 0;
    private const float MaxValueCanvasGroupAlpha = 1f;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        ShowCanvasGroup();
    }

    public void AddPlayerSelectionButton(PlayerType playerType)
    {
        bool isMatch = false;

        if (_playerChoiceButtons.Count > 0)
        {
            foreach (PlayerChoiceButton playerChoiceButton in _playerChoiceButtons)
            {
                if (playerType == playerChoiceButton.PlayerType)
                {
                    isMatch = true;

                    break;
                }
            }
        }

        if (isMatch == false)
        {
            PlayerChoiceButton playerChoiceButton = Instantiate(_playerChoiceButtonPrefab, _playersChoice.transform);
            playerChoiceButton.Initialize(playerType, this);

            _playerChoiceButtons.Add(playerChoiceButton);
        }
    }

    public void SelectPlayer(PlayerType playerType)
    {
        HideCanvasGroup();
        InitiateSelectingDelay(playerType);
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

        _hud.HideCanvasGroup();

        _gameAmbient.PlaySound(GameAmbient.SoundType.StartingGame);
    }

    private void HideCanvasGroup()
    {
        ChangeSmoothlyCanvasGroupAlpha(MinValueCanvasGroupAlpha);

        _canvasGroup.blocksRaycasts = false;

        _hud.ShowCanvasGroup();
    }

    private void InitiateSelectingDelay(PlayerType playerType)
    {
        StartCoroutine(InitiateSelectingDelayCoroutine(playerType));
    }

    private IEnumerator InitiateSelectingDelayCoroutine(PlayerType playerType)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_selectingDelay);

        yield return waitForSeconds;

        PlayerSelected?.Invoke(playerType);
    }
}