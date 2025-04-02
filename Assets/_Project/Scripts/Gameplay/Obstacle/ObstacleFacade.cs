using _Project.Scripts.Gameplay.Interfaces;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Obstacle
{
    public class ObstacleFacade : MonoBehaviour
    {
        [SerializeField] private DiebleObserver _diebleObserver;
        
        public Transform ObstacleTransform => transform;

        private void OnEnable() => 
            _diebleObserver.OnEnterTrigger += TriggerEnter;

        private void OnDisable() => 
            _diebleObserver.OnEnterTrigger -= TriggerEnter;

        private void TriggerEnter(IDieble diebleObj) => 
            diebleObj.Die();
    }
}