using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player")]
public class Player : ScriptableObject
{
    public string playerName;
    public bool isActive => playerName != "-";
    public string leftKey = "-";
    public string rightKey = "-";
    public Color color;
    public Vector2 startPosition;
    public InputActionAsset inputActionAsset;
    public int id;
    public InputAction LeftInputAction => inputActionAsset.FindActionMap("LeftRight").FindAction($"Left{id}");

    public InputAction RightInputAction => inputActionAsset.FindActionMap("LeftRight").FindAction($"Right{id}");
}