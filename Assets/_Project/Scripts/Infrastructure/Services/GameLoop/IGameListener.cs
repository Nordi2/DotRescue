namespace _Project.Scripts.Infrastructure.Services.GameLoop
{
    public interface IGameListener
    {
    }
    
    public interface IGameUpdateListener :
        IGameListener
    {
        void Update(float deltaTime);
    }

    public interface IGameFixedListener :
        IGameListener
    {
        void FixedUpdate(float fixedDeltaTime);
    }

    public interface IGameLateListener :
        IGameListener
    {
        void LateUpdate(float deltaTime);
    }
}