using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game
{
    public Dictionary<Player, int> ScoreBoard { get; private set; }
    private readonly HashSet<Player> _deadPlayers;
    public bool HasRoundEnded => _deadPlayers.Count == ScoreBoard.Count - 1;
    
    public Game(Player[] players)
    {
        ScoreBoard = new Dictionary<Player, int>();
        foreach (var player in players)
        {
            ScoreBoard.Add(player, 0);
        }
    }

    public void StartRound()
    {
        _deadPlayers.Clear();
    }

    public void PlayerDied(Player player)
    {
        _deadPlayers.Add(player);
        AwardPoints();
    }

    private void AwardPoints()
    {
        foreach (var player in ScoreBoard.Keys)
        {
            if (_deadPlayers.Contains(player)) continue;
            ScoreBoard[player]++;
        }
    }
}