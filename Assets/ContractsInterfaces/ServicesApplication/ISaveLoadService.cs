using ContractsInterfaces.Repositories;
using Domain.Gameplay.MessagesDTO;
using MessagePipe;

namespace ContractsInterfaces.ServicesApplication
{
    public interface ISaveLoadService : IService, IMessageHandler<SaveGameDTO>
    {
        public TResult Load<TResult, TConfig>(string key, TConfig config) where TConfig : IRepository;
        
        public TResult Load<TResult, TConfig>(string key) where TConfig : IRepository;
        
        public void AddConfig<TConfig>(TConfig configSample) where TConfig : IRepository;
        
        public void Save();
    }
}