using System;
using Play;
using UnityEngine;

public class HeadCollisionHandler : MonoBehaviour
{
    [SerializeField] private Controls controls;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private LineSpawner lineSpawner;
    
    [SerializeField] private Collider2D headCollider;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private Player player;

    public void IgnoreCollider(EdgeCollider2D neck)
    {
        Physics2D.IgnoreCollision(headCollider, neck);
    }

    public void SetHeadColliderActive(bool active)
    {
        headCollider.enabled = active;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        controls.enabled = false;
        this.enabled = false;
        rigidBody.simulated = false;
        gameManager.OnPlayerDied(player);
    }
}