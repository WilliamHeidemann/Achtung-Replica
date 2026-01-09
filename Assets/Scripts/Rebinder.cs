using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rebinder : ScriptableObject
{
    public enum Key { Left, Right }
    
    public void Rebind(Key key, Player player)
    {
        var action = key switch
        {
            Key.Left => player.left.action,
            Key.Right => player.right.action,
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
                Debug.Log("Left bound to: " + GetBindingText(key, player));
            })
            .Start();
    }
    
    public string GetBindingText(Key key, Player player)
    {
        var action = key switch
        {
            Key.Left => player.left.action,
            Key.Right => player.right.action,
            _ => throw new ArgumentOutOfRangeException(nameof(key), key, null)
        };
        
        return InputControlPath.ToHumanReadableString(
            action.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice
        );
    }
}
