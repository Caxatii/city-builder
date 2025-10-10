using ContractsInterfaces.ServicesApplication;
using ContractsInterfaces.UseCasesApplication;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.Grid;
using MessagePipe;
using Presentation.Gameplay.Views.Grid;
using VContainer.Unity;

namespace Application.UseCases.Grid
{
    public class GridRemoveBuildUseCase : IUseCase, IPostInitializable, IMessageHandler<RemoveBuildDTO>
    {
        private ISaveLoadService _saveLoadService;
        
        private GridView _view;
        private GridModel _model;

        private ISubscriber<RemoveBuildDTO> _subscriber;
        
        public void Initialize()
        {
            _subscriber.Subscribe(this);
        }

        public void PostInitialize()
        {
            
        }

        public void Handle(RemoveBuildDTO message)
        {
            
        }

        public void Dispose()
        {
        }
    }
}