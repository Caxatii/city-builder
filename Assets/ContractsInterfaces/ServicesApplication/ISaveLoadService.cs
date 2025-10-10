using ContractsInterfaces.Repositories;

namespace ContractsInterfaces.ServicesApplication
{
    public interface ISaveLoadService : IService
    {
        public TResult Load<TResult, TConfig>(string key, TConfig config) where TConfig : IRepository;
        public void Save();
    }
}