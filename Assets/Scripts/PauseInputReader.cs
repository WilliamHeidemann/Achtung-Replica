using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseInputReader : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            gameManager.TogglePause();
        }
    }
}
