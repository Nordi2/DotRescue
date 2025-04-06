using _Project.Scripts.Data;

namespace _Project.Scripts.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}