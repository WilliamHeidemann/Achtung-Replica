using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Random = UnityEngine.Random;

public class Controls : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private float moveSpeed = 2f;
    
    [SerializeField] private InputActionReference input;

    private void Update()
    {
        if (Keyboard.current.aKey.isPressed)
        {
            head.Rotate(Vector3.forward, Time.deltaTime * turnSpeed);
        }

        if (Keyboard.current.dKey.isPressed)
        {
            head.Rotate(-Vector3.forward, Time.deltaTime * turnSpeed);
        }
        
        head.Translate(head.up * (Time.deltaTime * moveSpeed));
    }
}