namespace _Project.Scripts.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : 
        IService
    {
        void LoadStaticData();

        T GetData<T>() where T : class;
    }
}