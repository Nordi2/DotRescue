using _Project.Scripts.Data;

namespace _Project.Scripts.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgress
    {
        void UpdateProgress(PlayerProgress progress);
    }
}