using System;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] private Player[] players;

    private void Start()
    {
        ResetPlayers();
    }

    private void ResetPlayers()
    {
        foreach (var player in players)
        {
            player.playerName = "-";
            player.LeftInputAction.Disable();
            player.RightInputAction.Disable();
            player.leftKey = "-";
            player.rightKey = "-";
        }
    }
}
