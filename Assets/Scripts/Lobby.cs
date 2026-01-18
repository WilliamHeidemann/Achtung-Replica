using System;
using UnityEngine;
using UtilityToolkit.Runtime;

[CreateAssetMenu(fileName = "Lobby", menuName = "ScriptableObjects/Lobby", order = 0)]
public class Lobby : ScriptableObject
{
    [SerializeField] private Player[] players;
    [SerializeField] private Rebinder rebinder;
    
    private Option<Player> _selectedPlayer;

    public void ListenForLeftKey()
    {
        if (!_selectedPlayer.IsSome(out var player))
        {
            return;
        }
        
        rebinder.Rebind(Rebinder.Key.Left, player);
    }

    public void ListenForRightKey()
    {
        if (!_selectedPlayer.IsSome(out var player))
        {
            return;
        }
        
        rebinder.Rebind(Rebinder.Key.Right, player);
    }

    public void DeselectPlayer()
    {
        _selectedPlayer = Option<Player>.None;
    }
    
    public void OnNameSet(int index)
    {
        if (index < 0 || index >= players.Length)
        {
            Debug.LogError($"Player index {index} is out of range");
            return;
        }
        
        _selectedPlayer = Option<Player>.Some(players[index]);

        ListenForLeftKey();
    }

    public void SetName(int index, string playerName)
    {
        if (index < 0 || index >= players.Length)
        {
            Debug.LogError($"Player index {index} is out of range");
            return;
        }

        players[index].playerName = playerName;
        
        OnNameSet(index);
    }
}