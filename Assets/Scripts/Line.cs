using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private EdgeCollider2D neckCollider;
    [SerializeField] private EdgeCollider2D tailCollider;
    [SerializeField] private int pointsInNeckTarget = 5;
    
    private List<Vector2> _points;

    private async void Start()
    {
        lineRenderer.positionCount = 0;
        neckCollider.Reset();
        tailCollider.Reset();
        _points = new List<Vector2>();
        await AddPoint();
    }

    private void Update()
    {
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, head.position);
    }

    private async Task AddPoint()
    {
        while (true)
        {
            _points.Add(head.position);
            
            lineRenderer.positionCount = _points.Count;
            lineRenderer.SetPositions(_points.To3D());
            
            var pointsInNeck = Mathf.Min(pointsInNeckTarget, _points.Count);
            neckCollider.SetPoints(_points.GetRange(_points.Count - pointsInNeck, pointsInNeck));
            tailCollider.SetPoints(_points.GetRange(0, _points.Count - pointsInNeck));
            await Awaitable.WaitForSecondsAsync(.1f);
        }
    }
    
    private void OnDrawGizmos()
    {
        if (_points == null) return;
        foreach (var point in _points)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(point, 0.1f);
        }
    }
}

public static class Extensions
{
    public static Vector3[] To3D(this List<Vector2> points)
    {
        var array = new Vector3[points.Count];
        for (var i = 0; i < points.Count; i++)
        {
            var vector2 = points[i];
            array[i] = new Vector3(vector2.x, vector2.y, 0);
        }
        return array;
    }
}
