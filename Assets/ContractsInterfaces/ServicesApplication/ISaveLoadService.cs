using ContractsInterfaces.Repositories;
using Domain.Gameplay.MessagesDTO;
using MessagePipe;

namespace ContractsInterfaces.ServicesApplication
{
    public interface ISaveLoadService : IService, IMessageHandler<SaveGameDTO>
    {
        public TResult Load<TResult, TConfig>(TConfig config, string key = null) where TConfig : IRepository;
        
        public TResult Load<TResult, TConfig>(string key = null) where TConfig : IRepository;

        public TResult Load<TResult>(string key = null) where TResult : new();
        
        public void AddConfig<TConfig>(TConfig configSample) where TConfig : IRepository;
        
        public void Save();
    }
}