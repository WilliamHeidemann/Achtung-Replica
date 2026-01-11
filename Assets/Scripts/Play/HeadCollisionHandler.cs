using System;
using UnityEngine;

public class HeadCollisionHandler : MonoBehaviour
{
    [SerializeField] private Controls controls;
    [SerializeField] private Line line;
    [SerializeField] private Rigidbody2D rigidBody;
    
    [SerializeField] private Collider2D headCollider;
    [SerializeField] private Collider2D neckCollider;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private Player player;
    
    private void Start()
    {
        Physics2D.IgnoreCollision(headCollider, neckCollider);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        controls.enabled = false;
        line.enabled = false;
        this.enabled = false;
        rigidBody.simulated = false;
        gameManager.OnPlayerDied(player);
    }
}