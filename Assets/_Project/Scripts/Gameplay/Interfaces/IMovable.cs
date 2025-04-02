namespace _Project.Scripts.Gameplay.Interfaces
{
    public interface IMovable
    {
        void Update(float deltaTime);
        void SwitchSides();
        void SetRotationSpeed(float rotationSpeed);
    }
}