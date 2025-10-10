using ContractsInterfaces.Repositories;

namespace ContractsInterfaces.FactoriesApplication
{
    public interface IModelFactoryService
    {
        public void Add<TConfig, TFactory>(TFactory factory)
            where TFactory : IModelFactory<TConfig> where TConfig : IRepository;
        public TResult Create<TResult, TConfig>(TConfig config) where TConfig : IRepository;
        public TResult Create<TResult>() where TResult : new();
    }
}