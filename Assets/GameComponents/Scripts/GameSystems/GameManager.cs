using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private StartGame _startGame;
    [SerializeField] private EndGame _endGame;
    [SerializeField] private Player _player;
    [SerializeField] private Text _timer;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private GameData _gameData;
    [SerializeField] private GameAmbient _gameAmbient;
    [SerializeField] private PlayerSurvivalTimeData[] _playersSurvivalTime;
    [SerializeField] private PlayerProgressionLevelsData[] _playersProgressionLevelsData;

    PlayerSurvivalTimeData _playerSurvivalTimeData;
    PlayerProgressionLevelsData _playerProgressionLevelsData;
    private PlayerType[] _playerTypes;
    private PlayerType _currentPlayerType;
    private int _killsCounter;
    private int _levelCounter;
    private int _levelProgression;
    private int _nextPlayerProgressionLevel;

    private float _remainingSurvivalTimeInSeconds;
    private float _timeInSeconds;
    private GameTime _remainingSurvivalTime;
    private GameTime _survivalTime;
    private Coroutine _coroutine;

    public bool IsGameOver { get; private set; } = true;

    private const int NumberOfSecondsPerMinute = 60;

    public event UnityAction<int> IncreasedKillsCounter;
    public event UnityAction<float> IncreasedLevelProgression;
    public event UnityAction<int> IncreasedLevelCounter;

    private void OnEnable()
    {
        _startGame.PlayerSelected += OnPlayerSelected;
    }

    private void OnDisable()
    {
        _startGame.PlayerSelected -= OnPlayerSelected;
    }

    private void Start()
    {
        _playerTypes = (PlayerType[])System.Enum.GetValues(typeof(PlayerType));

        AddPlayerSelectionButtons();

        _gameAmbient.PlaySound(GameAmbient.SoundType.StartingGame);
    }

    public void AddPlayerSelectionButtons()
    {
        for (int playerTypeIndex = 0; playerTypeIndex < (int)_gameData.LastAvailablePlayer + 1; playerTypeIndex++)
        {
            _startGame.AddPlayerSelectionButton(_playerTypes[playerTypeIndex]);
        }
    }

    private void Update()
    {
        CheckEndGameConditions();
    }

    private void CheckEndGameConditions()
    {
        if (!IsGameOver)
        {
            if (_player.IsLife)
            {
                if (_remainingSurvivalTime.Minutes == 0 && _remainingSurvivalTime.Seconds == 0 || _spawner.TotalEnemiesNumber == _killsCounter)
                {
                    IsGameOver = true;

                    _endGame.ShowGameResult(true);

                    int LastAvailablePlayerIndex = (int)_currentPlayerType + 1;
                    LastAvailablePlayerIndex = Mathf.Clamp(LastAvailablePlayerIndex, 0, _playerTypes.Length - 1);
                    _gameData.LastAvailablePlayer = _playerTypes[LastAvailablePlayerIndex];

                    _gameAmbient.PlaySound(GameAmbient.SoundType.EndingGame);
                }
            }
            else
            {
                IsGameOver = true;

                _endGame.ShowGameResult(false);

                _gameAmbient.PlaySound(GameAmbient.SoundType.EndingGame);
            }
        }
    }

    private void ChoosePlayerSurvivalTimeData(PlayerType playerType)
    {
        foreach (PlayerSurvivalTimeData playerSurvivalTimeData in _playersSurvivalTime)
        {
            if (playerSurvivalTimeData.PlayerType == playerType)
            {
                _playerSurvivalTimeData = playerSurvivalTimeData;

                break;
            }
        }
    }

    private void ChoosePlayerProgressionLevelsData(PlayerType playerType)
    {
        foreach (PlayerProgressionLevelsData playerProgressionLevelsData in _playersProgressionLevelsData)
        {
            if (playerProgressionLevelsData.PlayerType == playerType)
            {
                _playerProgressionLevelsData = playerProgressionLevelsData;

                break;
            }
        }
    }

    private void OnPlayerSelected(PlayerType playerType)
    {
        IsGameOver = false;

        _gameAmbient.PlaySound(GameAmbient.SoundType.Ambient);

        _killsCounter = 0;
        _levelCounter = 0;
        _levelProgression = 0;

        IncreasedKillsCounter?.Invoke(_killsCounter);
        IncreasedLevelProgression?.Invoke(_levelProgression);
        IncreasedLevelCounter?.Invoke(_levelCounter);

        _currentPlayerType = playerType;

        ChoosePlayerSurvivalTimeData(_currentPlayerType);
        ChoosePlayerProgressionLevelsData(_currentPlayerType);
        _player.GetComponent<PlayerProperties>().InitializePlayerType(_currentPlayerType, this);
        _spawner.ChooseEnemiesSpawnerData(_currentPlayerType);
        CountGameTime();
        _spawner.ResetSpawnLevel();
        _spawner.SpawnEnemy();
    }

    public void IncrementKillsCounter()
    {
        if (_player.IsLife)
        {
            _killsCounter++;

            IncreasedKillsCounter?.Invoke(_killsCounter);

            IncrementPlayerLevel();
        }
    }

    private void IncrementPlayerLevel()
    {
        _levelProgression++;

        _nextPlayerProgressionLevel = _playerProgressionLevelsData.GetProgressionLevel(Mathf.Min(_levelCounter, _playerProgressionLevelsData.ProgressionLevelsLength - 1));

        IncreasedLevelProgression?.Invoke((float)_levelProgression / _nextPlayerProgressionLevel);

        if (_levelProgression >= _nextPlayerProgressionLevel)
        {
            _levelProgression = 0;
            _levelCounter++;

            IncreasedLevelCounter?.Invoke(_levelCounter);
        }
    }

    private void CountGameTime()
    {
        _timeInSeconds = 0;
        _remainingSurvivalTimeInSeconds = _playerSurvivalTimeData.GameTime.Minutes * NumberOfSecondsPerMinute + _playerSurvivalTimeData.GameTime.Seconds;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(CountGameTimeCoroutine());
    }

    private IEnumerator CountGameTimeCoroutine()
    {
        while (_player.IsLife && !IsGameOver)
        {
            _timeInSeconds += Time.deltaTime;

            _survivalTime.Minutes = (int)_timeInSeconds / NumberOfSecondsPerMinute;
            _survivalTime.Seconds = (int)_timeInSeconds % NumberOfSecondsPerMinute;

            _remainingSurvivalTime.Minutes = (int)(_remainingSurvivalTimeInSeconds - _timeInSeconds) / NumberOfSecondsPerMinute;
            _remainingSurvivalTime.Seconds = (int)(_remainingSurvivalTimeInSeconds - _timeInSeconds) % NumberOfSecondsPerMinute;

            _timer.text = string.Format("{0:D2}:{1:D2}", _remainingSurvivalTime.Minutes, _remainingSurvivalTime.Seconds);

            yield return null;
        }
    }

    [System.Serializable]
    public struct GameTime
    {
        public int Minutes;
        public int Seconds;

        public GameTime(int minutes, int seconds)
        {
            Minutes = minutes;
            Seconds = seconds;
        }
    }
}