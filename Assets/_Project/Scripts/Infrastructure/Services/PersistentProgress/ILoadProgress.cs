using _Project.Scripts.Data;

namespace _Project.Scripts.Infrastructure.Services.PersistentProgress
{
    public interface ILoadProgress
    {
        void LoadProgress(PlayerProgress progress);
    }
}