using System;

namespace _Project.Scripts.Infrastructure.Services.Input
{
    public class InputService :
        IInputService
    {
        public event Action OnClickLeftMouseButton;

        public void Update(float deltaTime)
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
                OnClickLeftMouseButton?.Invoke();
        }
    }
}