using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameSystems/GameData", order = 51)]

public class GameData : ScriptableObject
{
    [field: SerializeField] public PlayerType LastAvailablePlayer { get; set; }
}