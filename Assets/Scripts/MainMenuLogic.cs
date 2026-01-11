using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UtilityToolkit.Runtime;

public class MainMenuLogic : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private GameManager gameManager;
    private Button _startButton;

    [SerializeField] private Player player1;
    [SerializeField] private Player player2;
    [SerializeField] private Player player3;
    [SerializeField] private Player player4;
    [SerializeField] private Player player5;
    [SerializeField] private Player player6;
    [SerializeField] private Player player7;
    [SerializeField] private Player player8;

    private Option<Player> _selectedPlayer;
    
    private void OnEnable()
    {
        _startButton = uiDocument.rootVisualElement.Q<Button>("Start");
        _startButton.RegisterCallback<ClickEvent>(OnStartGame);

        Keyboard.current.onTextInput += OnTextInput;
        OnPlayer1Selected();
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