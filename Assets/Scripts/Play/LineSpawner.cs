using System;
using Play;
using UnityEngine;
using UnityEngine.InputSystem;
using UtilityToolkit.Runtime;
using Random = UnityEngine.Random;

public class LineSpawner : MonoBehaviour
{
    [SerializeField] private HeadCollisionHandler head;
    [SerializeField] private Line linePrefab;
    [SerializeField] private Color color;
    
    private Line _currentLine;

    private CountdownTimer _timer;
    private void Start() => StartLine();
    private void Update() => _timer.Tick();

    private void StartLine()
    {
        _currentLine = Instantiate(linePrefab);
        _currentLine.color = color;
        _currentLine.head = head.transform;
        head.IgnoreCollider(_currentLine.Neck);
        head.SetHeadColliderActive(true);

        var lineLifespan = Random.Range(1f, 3f);
        _timer = new CountdownTimer(lineLifespan);
        _timer.OnTimerEnded += EndLine;
    }
    
    private void EndLine()
    {
        _currentLine.End();
        head.SetHeadColliderActive(false);
        _timer = new CountdownTimer(0.2f);
        _timer.OnTimerEnded += StartLine;
    }
}
