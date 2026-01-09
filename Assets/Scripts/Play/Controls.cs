using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private float moveSpeed = 2f;
    
    [SerializeField] private InputActionReference left;
    [SerializeField] private InputActionReference right;

    private void OnEnable()
    {
        left.action.Enable();
        right.action.Enable();
    }

    private void OnDisable()
    {
        left.action.Disable();
        right.action.Disable();
    }

    private void Update()
    {
        if (left.action.IsPressed())
        {
            head.Rotate(Vector3.forward, Time.deltaTime * turnSpeed);
        }

        if (right.action.IsPressed())
        {
            head.Rotate(-Vector3.forward, Time.deltaTime * turnSpeed);
        }
        
        head.Translate(head.up * (Time.deltaTime * moveSpeed));
    }
}