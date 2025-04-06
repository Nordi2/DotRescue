using _Project.Scripts.Data;

namespace _Project.Scripts.Infrastructure.Services.PersistentProgress
{
    public class PersistentProgressService : 
        IPersistentProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}