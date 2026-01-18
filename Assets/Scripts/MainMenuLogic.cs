using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UtilityToolkit.Runtime;

public class MainMenuLogic : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Lobby lobby;
    
    private Button _startButton;

    [SerializeField] private Player player1;
    [SerializeField] private Player player2;
    [SerializeField] private Player player3;
    [SerializeField] private Player player4;
    [SerializeField] private Player player5;
    [SerializeField] private Player player6;

    private Option<Player> _selectedPlayer;

    private readonly string[] _names = 
    {
        "-", "GL1", "GL2", "GL3", "GL4", "GL5", "GL6", "GL7", "GL8",
        "ML2", "ML3", "ML4", "ML5", "ML6", "ML7", "ML8", 
        "NY2", "NY3", "NY4", "NY5", "NY6", "NY7", "NY8"
    };
    
    private void OnEnable()
    {
        _startButton = uiDocument.rootVisualElement.Q<Button>("Start");
        _startButton.RegisterCallback<ClickEvent>(OnStartGame);

        SetSizes();

        var dropdowns = uiDocument.rootVisualElement.Query<DropdownField>().ToList();
        for (var index = 0; index < dropdowns.Count; index++)
        {
            var dropdown = dropdowns[index];
            dropdown.choices.AddRange(_names);
            var i = index;
            dropdown.RegisterValueChangedCallback(evt => lobby.SetName(i, evt.newValue));
        }

        Keyboard.current.onTextInput += OnTextInput;
        OnPlayer1Selected();
    }

    private void SetSizes()
    {
        var contents = uiDocument.rootVisualElement.Q<VisualElement>("Contents");
        foreach (var row in contents.Children())
        {
            var list = row.Children().ToList();
            for (var index = 0; index < list.Count; index++)
            {
                var element = list[index];

                var size = index switch
                {
                    0 => 15,
                    1 => 55,
                    2 => 20,
                    3 => 20,
                    _ => throw new ArgumentOutOfRangeException()
                };
                
                var length = new StyleLength(new Length(size, LengthUnit.Percent));
                element.style.minWidth = length;
                element.style.maxWidth = length;
            }
        }
    }


    private void OnTextInput(char c)
    {
        if (!_selectedPlayer.IsSome(out var player))
            return;
        
        
    }


    private void OnDisable()
    {
        _startButton.UnregisterCallback<ClickEvent>(OnStartGame);
    }

    private void OnStartGame(ClickEvent _)
    {
        gameManager.StartGame();
    }

    public void OnPlayer1Selected() => OnPlayerSelected(player1);

    public void OnPlayerSelected(Player player) => _selectedPlayer = Option<Player>.Some(player);
    public void OnPlayerDeselected() => _selectedPlayer = Option<Player>.None;
}