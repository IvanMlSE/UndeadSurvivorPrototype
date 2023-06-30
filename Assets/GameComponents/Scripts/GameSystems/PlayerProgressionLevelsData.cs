using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerProgressionLevelsData", menuName = "ScriptableObjects/GameSystems/PlayerProgressionLevelsData", order = 51)]

public class PlayerProgressionLevelsData : ScriptableObject
{
    [SerializeField] private PlayerType _playerType;
    [SerializeField] private int[] _progressionLevels = new int[1];

    public PlayerType PlayerType => _playerType;
    public int ProgressionLevelsLength => _progressionLevels.Length;

    public int GetProgressionLevel(int index)
    {
        return _progressionLevels[index];
    }
}