using System;
using Play;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Controls : MonoBehaviour, IPausable
{
    [SerializeField] private Transform head;
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private float moveSpeed = 2f;
    
    private InputAction _left;
    private InputAction _right;

    private bool _isPaused = false;

    public void Initialize(Player player)
    {
        _left = player.LeftInputAction;
        _right = player.RightInputAction;
        _left.Enable();
        _right.Enable();
        
        var x = Random.Range(-3, 3);
        var y = Random.Range(-3, 3);
        var angle = Random.Range(0f, 360f);
        head.Rotate(Vector3.forward, angle);
        head.position = new Vector3(x, y);
    }

    private void OnDisable()
    {
        _left.Disable();
        _right.Disable();
    }

    private void Update()
    {
        if (_isPaused)
        {
            return;
        }
        
        if (_left.IsPressed())
        {
            head.Rotate(Vector3.forward, Time.deltaTime * turnSpeed);
        }

        if (_right.IsPressed())
        {
            head.Rotate(-Vector3.forward, Time.deltaTime * turnSpeed);
        }
        
        head.Translate(head.up * (Time.deltaTime * moveSpeed));
    }

    public void Pause()
    {
        _isPaused = true;
    }

    public void Resume()
    {
        _isPaused = false;
    }
}