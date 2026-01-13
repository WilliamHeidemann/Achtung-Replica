using UnityEngine;

public class GameStartDebug : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    private void Start()
    {
        gameManager.StartGame();
    }
    
    private void Update()
    {
        
    }
}
