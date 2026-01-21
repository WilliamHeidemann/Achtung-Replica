using System;
using System.Collections.Generic;
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
    [SerializeField] private ScoreboardUILogic gamePlayUI;
    [SerializeField] private Player[] players;
    
    private Button _startButton;
    private Dictionary<Player, VisualElement> _leftKeys;
    private Dictionary<Player, VisualElement> _rightKeys;

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

        SetKeyBorders();

        var dropdowns = uiDocument.rootVisualElement.Query<DropdownField>().ToList();
        for (var index = 0; index < dropdowns.Count; index++)
        {
            var dropdown = dropdowns[index];
            dropdown.choices.AddRange(_names);
            var i = index;
            dropdown.RegisterValueChangedCallback(evt => lobby.SetName(i, evt.newValue));
        }
    }

    private void Start()
    {
        gamePlayUI.Toggle(false);
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

    private void SetKeyBorders()
    {
        _leftKeys = new Dictionary<Player, VisualElement>();
        _rightKeys = new Dictionary<Player, VisualElement>();
        
        lobby.OnListenForKey += OnListenForKey;
        lobby.OnStopListenForKey += OnStopListenForKey;
        
        for (int i = 1; i <= 6; i++)
        {
            var leftName = $"Player{i}LeftKey";
            var rightName = $"Player{i}RightKey"; 
            var leftKey = Get(leftName);
            var rightKey = Get(rightName);
            var player = players[i - 1];

            _leftKeys.Add(player, leftKey);
            _rightKeys.Add(player, rightKey);
        }
        
        foreach (var (p, v) in _leftKeys) 
            v.SetBorderColor(Color.white);
        foreach (var (p, v) in _rightKeys) 
            v.SetBorderColor(Color.white);
        
        return;
        VisualElement Get(string n) => uiDocument.rootVisualElement.Query<VisualElement>(name: n);
    }

    private void OnListenForKey(Player player, Rebinder.Key key)
    {
        var dictionary = key == Rebinder.Key.Left ? _leftKeys : _rightKeys;
        var element = dictionary[player];
        element.SetBorderWidth(5);
    }
    
    private void OnStopListenForKey(Player player, Rebinder.Key key)
    {
        var dictionary = key == Rebinder.Key.Left ? _leftKeys : _rightKeys;
        var element = dictionary[player];
        element.SetBorderWidth(0);
    }

    private void OnDisable()
    {
        _startButton.UnregisterCallback<ClickEvent>(OnStartGame);
    }

    private void OnStartGame(ClickEvent _)
    {
        gameManager.StartGame();
        uiDocument.enabled = false;
        gamePlayUI.Toggle(true);
    }
}