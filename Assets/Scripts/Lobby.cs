using System;
using UnityEngine;
using UtilityToolkit.Runtime;

[CreateAssetMenu(fileName = "Lobby", menuName = "ScriptableObjects/Lobby", order = 0)]
public class Lobby : ScriptableObject
{
    [SerializeField] private Player[] players;
    
    private Option<Player> _selectedPlayer;
    private State _state = State.None;

    public enum State
    {
        None,
        Name,
        LeftKey,
        RightKey
    }

    public void AdvanceState()
    {
        var nextState = _state switch
        {
            State.None => State.Name,
            State.Name => State.LeftKey,
            State.LeftKey => State.RightKey,
            State.RightKey => State.None,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        
    }
    
    public void OnSelectPlayer(int index)
    {
        if (index < 0 || index >= players.Length)
        {
            Debug.LogError($"Player index {index} is out of range");
            return;
        }
        
        _selectedPlayer = Option<Player>.Some(players[index]);

        AdvanceState();
    }

    public void SetName(int index, string playerName)
    {
        if (index < 0 || index >= players.Length)
        {
            Debug.LogError($"Player index {index} is out of range");
            return;
        }

        players[index].playerName = playerName;
    }
}