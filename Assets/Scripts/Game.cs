using System;
using System.Collections.Generic;
using System.Linq;

public class Game
{
    private Dictionary<Player, int> ScoreBoard { get; }
    private readonly HashSet<Player> _deadPlayers = new();
    public bool HasRoundEnded => _deadPlayers.Count == ScoreBoard.Count - 1;
    
    public event Action<Dictionary<Player, int>> OnScoreboardUpdated;
    
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
        foreach (var player in ScoreBoard.Keys.Where(player => !_deadPlayers.Contains(player)).ToList())
        {
            ScoreBoard[player]++;
        }
        
        OnScoreboardUpdated?.Invoke(ScoreBoard);
    }
}