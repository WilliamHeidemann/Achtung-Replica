using UnityEngine;
using UnityEngine.InputSystem;

public class Player : ScriptableObject
{
    public string playerName;
    public Color color;
    public bool isActive;
    public InputActionReference left;
    public InputActionReference right;
}