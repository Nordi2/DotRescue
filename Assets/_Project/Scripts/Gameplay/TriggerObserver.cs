using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class TriggerObserver<T> : MonoBehaviour
    {
        public event Action<T> OnEnterTrigger;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponentInParent<T>() is { } component) 
                OnEnterTrigger?.Invoke(component);
        }
    }
}