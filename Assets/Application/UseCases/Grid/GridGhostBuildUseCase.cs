using System.Collections.Generic;
using System.Linq;
using Application.Services;
using ContractsInterfaces.Repositories;
using ContractsInterfaces.ServicesApplication;
using ContractsInterfaces.UseCasesApplication;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.Currency;
using MessagePipe;
using Presentation.Gameplay.Views.Buildings;
using VContainer;
using VContainer.Unity;

namespace Application.UseCases.Grid
{
    public class GridGhostBuildUseCase : IUseCase, ITickable, IPostInitializable, IMessageHandler<BuildingButtonClickedDTO>
    {
        [Inject] private IGameplayBuildingsRepository _buildingsRepository;
        [Inject] private ICurrencyRepository _currencyRepository;
        [Inject] private ISaveLoadService _saveLoadService;
        [Inject] private ISubscriber<BuildingButtonClickedDTO> _subscriber;
        [Inject] private IPublisher<NotEnoughResources> _publisher;

        private CurrencyModel _currencyModel;
        private BuildingView _view;
        private Dictionary<string, IBuildingRepository> _repositories;
        
        public void Initialize()
        {
            _subscriber.Subscribe(this);

            _repositories = _buildingsRepository.Repositories.
                ToDictionary(key => key.Name, value => value);
        }

        public void PostInitialize()
        {
            _currencyModel = 
                _saveLoadService.Load<CurrencyModel, ICurrencyRepository>(CurrencyType.Gold, _currencyRepository)
        }

        public void Handle(BuildingButtonClickedDTO message)
        {
            IBuildingRepository building = _repositories[message.Name];

            if (_currencyModel.IsEnough(building.Price) == false)
            {
                _publisher.Publish(new NotEnoughResources());
                return;
            }
            
            
        }

        public void Tick()
        {
            if(_view == null)
                return;
        }

        public void Dispose()
        {
            
        }
    }
}