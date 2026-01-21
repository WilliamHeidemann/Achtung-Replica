using System;
using UnityEngine;
using UtilityToolkit.Runtime;

[CreateAssetMenu(fileName = "Lobby", menuName = "ScriptableObjects/Lobby", order = 0)]
public class Lobby : ScriptableObject
{
    [SerializeField] private Player[] players;
    [SerializeField] private Rebinder rebinder;
    
    private Option<Player> _selectedPlayer;

    public event Action<Player, Rebinder.Key> OnListenForKey;
    public event Action<Player, Rebinder.Key> OnStopListenForKey;

    public void ListenForLeftKey()
    {
        if (!_selectedPlayer.IsSome(out var player))
        {
            return;
        }
        
        rebinder.Rebind(Rebinder.Key.Left, player);
        OnListenForKey?.Invoke(player, Rebinder.Key.Left);
    }

    public void ListenForRightKey()
    {
        if (!_selectedPlayer.IsSome(out var player))
        {
            return;
        }
        
        rebinder.Rebind(Rebinder.Key.Right, player);
        OnStopListenForKey?.Invoke(player, Rebinder.Key.Left);
        OnListenForKey?.Invoke(player, Rebinder.Key.Right);
    }

    public void DeselectPlayer()
    {
        if (!_selectedPlayer.IsSome(out var player))
        {
            return;
        }
        
        OnStopListenForKey?.Invoke(player, Rebinder.Key.Left);
        OnStopListenForKey?.Invoke(player, Rebinder.Key.Right);
        
        _selectedPlayer = Option<Player>.None;
    }

    private void OnNameSet(Player player)
    {
        _selectedPlayer = Option<Player>.Some(player);

        ListenForLeftKey();
    }

    public void SetName(int index, string playerName)
    {
        if (index < 0 || index >= players.Length)
        {
            Debug.LogError($"Player index {index} is out of range");
            return;
        }

        var player = players[index];
        player.playerName = playerName;

        if (playerName == "-")
        {
            player.leftKey = "-";
            player.rightKey = "-";
        }
        else
        {
            OnNameSet(player);
        }
    }
}