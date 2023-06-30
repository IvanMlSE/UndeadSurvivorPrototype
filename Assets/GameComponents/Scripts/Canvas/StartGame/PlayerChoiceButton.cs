using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class PlayerChoiceButton : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _name;
    [SerializeField] private Text _description;
    [SerializeField] private PlayerChoiceButtonData[] _playersChoiceButtonsData;

    private Button _button;

    public PlayerType PlayerType { get; private set; }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void Initialize(PlayerType playerType, StartGame startGame)
    {
        foreach (PlayerChoiceButtonData playerChoiceButtonData in _playersChoiceButtonsData)
        {
            if (playerChoiceButtonData.PlayerType == playerType)
            {
                PlayerType = playerType;

                _icon.sprite = playerChoiceButtonData.Icon;
                _name.text = playerChoiceButtonData.Name;
                _description.text = playerChoiceButtonData.Description;

                _button.onClick.AddListener(() =>
                {
                    startGame.SelectPlayer(playerType);
                });

                break;
            }
        }
    }
}