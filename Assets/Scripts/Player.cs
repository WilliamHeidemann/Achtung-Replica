using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player")]
public class Player : ScriptableObject
{
    public string playerName;
    public bool isActive;
    public string leftKey = "-";
    public string rightKey = "-";
    public InputActionReference left;
    public InputActionReference right;
}