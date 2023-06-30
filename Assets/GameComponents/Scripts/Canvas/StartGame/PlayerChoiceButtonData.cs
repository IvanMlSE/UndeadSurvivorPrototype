using UnityEngine;

[CreateAssetMenu(fileName = "PlayerChoiceButtonData", menuName = "ScriptableObjects/PlayerChoiceButtonData", order = 51)]

public class PlayerChoiceButtonData : ScriptableObject
{
    [field: SerializeField] public PlayerType PlayerType { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
}