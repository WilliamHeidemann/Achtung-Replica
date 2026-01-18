using UnityEngine;
using UtilityToolkit.Editor;

[CreateAssetMenu(fileName = "DevTools", menuName = "ScriptableObjects/DevTools", order = 0)]
public class DevTools : ScriptableObject
{
    [SerializeField] private Player[] players;

    [Button]
    public void ResetPlayers()
    {
        foreach (var player in players)
        {
            player.playerName = "-";
            player.leftKey = "-";
            player.rightKey = "-";
        }
    }
}