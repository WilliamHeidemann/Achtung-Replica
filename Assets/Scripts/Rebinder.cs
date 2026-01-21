using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Rebinder", menuName = "ScriptableObjects/Rebinder", order = 0)]
public class Rebinder : ScriptableObject
{
    public enum Key { Left, Right }

    [SerializeField] private Lobby lobby;
    
    public void Rebind(Key key, Player player)
    {
        var action = key switch
        {
            Key.Left => player.LeftInputAction,
            Key.Right => player.RightInputAction,
            _ => throw new ArgumentOutOfRangeException(nameof(key), key, null)
        };
        
        action.Disable();

        action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation =>
            {
                action.Enable();
                operation.Dispose();
                if (key == Key.Left)
                {
                    player.leftKey = GetBindingText(key, player);
                    lobby.ListenForRightKey();
                }
                else
                {
                    player.rightKey = GetBindingText(key, player);
                    lobby.DeselectPlayer();
                }
            })
            .Start();
    }
    
    public string GetBindingText(Key key, Player player)
    {
        InputAction action = key switch
        {
            Key.Left => player.LeftInputAction,
            Key.Right => player.RightInputAction,
            _ => throw new ArgumentOutOfRangeException(nameof(key), key, null)
        };
        
        return InputControlPath.ToHumanReadableString(
            action.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice
        );
    }
}