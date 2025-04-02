using NaughtyAttributes;
using UnityEngine;

namespace _Project.Scripts.StaticData
{
    [CreateAssetMenu(
        fileName = "ObstacleConfig",
        menuName = "Configs/ObstacleConfig")]
    public class ObstacleConfig : ScriptableObject
    {
        [BoxGroup("ObstacleConfig")]
        [SerializeField,MinMaxSlider(1f,250f)] private Vector2 _minMaxRotationSpeed;
        [BoxGroup("ObstacleConfig")]
        [SerializeField,MinMaxSlider(0.5f,10f)] private Vector2 _minMaxRotateTime;
        
        public float GetRandomRotationSpeed() => 
            Random.Range(_minMaxRotationSpeed.x, _minMaxRotationSpeed.y);

        public float GetRandomRotateTime() => 
            Random.Range(_minMaxRotateTime.x, _minMaxRotateTime.y);
    }
}
