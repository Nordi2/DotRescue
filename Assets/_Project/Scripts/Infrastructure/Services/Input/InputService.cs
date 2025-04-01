using System;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Services.Input
{
    public class InputService : MonoBehaviour,
        IInputService
    {
        public event Action OnClickLeftMouseButton;
        
        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
                OnClickLeftMouseButton?.Invoke();
        }
    }
}