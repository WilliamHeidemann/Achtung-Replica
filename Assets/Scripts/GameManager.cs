using System;
using System.Collections.Generic;
using System.Linq;
using Play;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "GameManager", menuName = "ScriptableObjects/GameManager")]
public class GameManager : ScriptableObject
{
    [SerializeField] private Player[] players;
    [SerializeField] private PlayerBrain brainPrefab;
    [SerializeField] private InputActionAsset asset;
    public event Action OnGameStarted;

    private Game _game;
    private bool _isPaused;
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
        OnGameStarted?.Invoke();
    }

    private void SpawnPlayers()
    {
        _brains = new List<PlayerBrain>();
        var activePlayers = players.Where(player => player.isActive).ToList();
        var positions = GetRandomPositions(playerCount: activePlayers.Count);
        
        for (var i = 0; i < activePlayers.Count; i++)
        {
            var player = activePlayers[i];
            player.startPosition = positions[i];
            var brain = Instantiate(brainPrefab);
            brain.Initialize(player);
            _brains.Add(brain);
        }
    }

    private List<Vector2> GetRandomPositions(int playerCount)
    {
        // divide [-3,3] into 9 squares
        // each of the 9 squares has a width of 1.75
        // space these squares 
        // each player spawns in a square
        
        var positions = new List<Vector2>();

        const float width = 1f;
        
        var a1 = -3f;
        var b1 = a1 + width;

        var a2 = 0f - width / 2f;
        var b2 = a2 + width;

        var a3 = 3f - width;
        var b3 = 3f;

        var bounds = new[]
        {
            (a1,b1), (a2, b2), (a3, b3)
        };

        foreach (var (lowX, highX) in bounds)
        {
            foreach (var (lowY, highY) in bounds)
            {
                var x = Random.Range(lowX, highX);
                var y = Random.Range(lowY, highY);
                var position  = new Vector2(x, y);
                positions.Add(position);
            }
        }
        
        return positions.OrderBy(_ => Random.value).Take(playerCount).ToList();
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

    public void SubscribeToScoreboardUpdated(Action<Dictionary<Player, int>> onScoreboardUpdated)
    {
        _game.OnScoreboardUpdated += onScoreboardUpdated;
    }
}