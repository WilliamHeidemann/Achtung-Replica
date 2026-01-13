using System;
using UnityEngine;

namespace Play
{
    public class PlayerBrain : MonoBehaviour
    {
        [SerializeField] private LineSpawner lineSpawner;
        [SerializeField] private HeadCollisionHandler headCollisionHandler;
        [SerializeField] private Controls controls;
        
        private IPausable[] _pausables;
        private Player _player;

        public void Initialize(Player player)
        {
            lineSpawner.Initialize(player.color);
            headCollisionHandler.Initialize(player);
            controls.Initialize(player);
            _player = player;
        }
        
        private void Start()
        {
            _pausables = GetComponentsInChildren<IPausable>(true);
        }

        public void Pause()
        {
            foreach (var pausable in _pausables)
            {
                pausable.Pause();
            }
        }

        public void Resume()
        {
            foreach (var pausable in _pausables)
            {
                pausable.Resume();
            }
        }
    }
}