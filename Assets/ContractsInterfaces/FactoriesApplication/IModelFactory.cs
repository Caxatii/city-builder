using ContractsInterfaces.Repositories;

namespace ContractsInterfaces.FactoriesApplication
{
    public interface IModelFactory
    {
        public object Create(object config);
    }

    public interface IModelFactory<in TConfig> : IModelFactory where TConfig : IRepository
    {
        object IModelFactory.Create(object config)
        {
            return Create((TConfig)config);
        }
        
        public object Create(TConfig config);
    }
    
    public interface IModelFactory<out TResult, in TConfig> : IModelFactory<TConfig> where TConfig : IRepository
    {
        object IModelFactory<TConfig>.Create(TConfig config)
        {
            return Create(config);
        }

        object IModelFactory.Create(object config)
        {
            return Create((TConfig)config);
        }

        public TResult Create(TConfig config);
    }
}