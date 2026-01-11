using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GameManager", menuName = "ScriptableObjects/GameManager")]
public class GameManager : ScriptableObject
{
    [SerializeField] private Player[] players;

    private Game _game;

    public void StartGame()
    {
        var activePlayers = players.Where(player => player.isActive).ToArray();
        if (activePlayers.Length <= 1)
        {
            Debug.LogError("Not enough players");
            return;
        }
        
        _game = new Game(activePlayers);
        _game.StartRound();
    }

    public void OnPlayerDied(Player player)
    {
        if (_game == null)
        {
            Debug.LogWarning("Game is not initialized.");
            return;
        }
        _game.PlayerDied(player);
        if (_game.HasRoundEnded)
        {
            OnRoundEnded();
        }
    }

    private void OnRoundEnded()
    {
    }

    public void ReturnToMainMenu()
    {
    }
}