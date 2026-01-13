using System;
using System.Collections.Generic;
using UnityEngine;

namespace Play
{
    public class Line : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private EdgeCollider2D neck;
        [SerializeField] private EdgeCollider2D tail;
        [SerializeField] private float timeBetweenPoints;
        
        [Header("Initialize On Spawn")]
        public Transform head;
        public Color color;

        public EdgeCollider2D Neck => neck;
        private float _timeAtLastPoint;
        private bool _shouldAddPoints = true;
        private readonly List<Vector2> _points = new();
        private const int PointsInNeckTarget = 5;

        private void Start()
        {
            AddPoint();
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
        }

        private void Update()
        {
            if (!_shouldAddPoints)
            {
                return;
            }
            
            if (Time.time > _timeAtLastPoint + timeBetweenPoints)
            {
                _timeAtLastPoint = Time.time + timeBetweenPoints;
                AddPoint();
            }
            
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, head.position);
        }

        private void AddPoint()
        {
            _points.Add(head.position);
            
            lineRenderer.positionCount = _points.Count;
            lineRenderer.SetPositions(_points.To3D());
            
            var pointsInNeck = Mathf.Min(PointsInNeckTarget, _points.Count);
            neck.SetPoints(_points.GetRange(_points.Count - pointsInNeck, pointsInNeck));
            tail.SetPoints(_points.GetRange(0, _points.Count - pointsInNeck));
        }

        public void End()
        {
            neck.SetPoints(new List<Vector2>());
            tail.SetPoints(_points);
            enabled = false;
        }

        public void Pause()
        {
            _shouldAddPoints = false;
        }

        public void Resume()
        {
            _shouldAddPoints = true;
        }
    }
}