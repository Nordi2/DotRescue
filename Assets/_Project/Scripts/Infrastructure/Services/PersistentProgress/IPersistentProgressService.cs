using _Project.Scripts.Data;

namespace _Project.Scripts.Infrastructure.Services.PersistentProgress
{
    public interface IPersistentProgressService :
        IService
    {
        PlayerProgress Progress { get; set; }
    }
}