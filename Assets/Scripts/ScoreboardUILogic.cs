using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UtilityToolkit.Runtime;

public class ScoreboardUILogic : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private GameObject frame;
    [SerializeField] private Player[] players;
    [SerializeField] private GameManager gameManager;

    private void OnEnable()
    {
        gameManager.OnGameStarted += () => gameManager.SubscribeToScoreboardUpdated(OnScoreboardUpdated);
    }

    public void Toggle(bool show)
    {
        uiDocument.enabled = show;
        frame.SetActive(show);
    }

    public void OnScoreboardUpdated(Dictionary<Player, int> scoreBoard)
    {
        var scoreElementContainers = 
            uiDocument.rootVisualElement.Q<VisualElement>("Scoreboard").Children().ToArray();

        var scores = 
            scoreBoard.ToArray().OrderByDescending(kvp => kvp.Value).ToArray();
        
        
        for (var i = 0; i < scoreElementContainers.Length; i++)
        {
            var scoreElementContainer = scoreElementContainers[i];
            
            if (i >= scores.Length)
            {
                scoreElementContainer.Hide();
            }
            else
            {
                scoreElementContainer.Show();
            }
        }
    }
}
