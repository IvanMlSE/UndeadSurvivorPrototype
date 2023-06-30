using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSurvivalTimeData", menuName = "ScriptableObjects/GameSystems/PlayerSurvivalTimeData", order = 51)]

public class PlayerSurvivalTimeData : ScriptableObject
{
    [field: SerializeField] public PlayerType PlayerType { get; private set; }
    [field: SerializeField] public GameManager.GameTime GameTime { get; private set; }
}