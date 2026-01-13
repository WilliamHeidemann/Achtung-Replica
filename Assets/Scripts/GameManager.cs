using System.Collections.Generic;
using System.Linq;
using Play;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "GameManager", menuName = "ScriptableObjects/GameManager")]
public class GameManager : ScriptableObject
{
    [SerializeField] private Player[] players;
    [SerializeField] private PlayerBrain brainPrefab;
    [SerializeField] private InputActionAsset asset;

    private Game _game;
    private bool _isPaused = false;
    private List<PlayerBrain> _brains;

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
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        _brains = new List<PlayerBrain>();
        foreach (var player in players.Where(player => player.isActive))
        {
            var brain = Instantiate(brainPrefab);
            brain.Initialize(player);
            _brains.Add(brain);
        }
    }

    public void TogglePause()
    {
        _isPaused = !_isPaused;
        
        if (_isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }
    
    private void PauseGame()
    {
        if (_brains == null) return;
        
        foreach (var brain in _brains)
        {
            brain.Pause();
        }
    }

    private void ResumeGame()
    {
        if (_brains == null) return;
        
        foreach (var brain in _brains)
        {
            brain.Resume();
        }
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