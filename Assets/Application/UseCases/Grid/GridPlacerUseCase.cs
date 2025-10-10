using System.Collections.Generic;
using System.Linq;
using ContractsInterfaces.FactoriesApplication;
using ContractsInterfaces.Repositories;
using ContractsInterfaces.ServicesApplication;
using ContractsInterfaces.UseCasesApplication;
using Core;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.Buildings;
using Domain.Gameplay.Models.Currency;
using Domain.Gameplay.Models.Grid;
using MessagePipe;
using Presentation.Gameplay.Views.Buildings;
using Presentation.Gameplay.Views.Grid;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Application.UseCases.Grid
{
    public class GridPlacerUseCase : IUseCase, IPostInitializable, IMessageHandler<TryPlaceDTO>
    {
        [Inject] private IModelFactoryService _factoryService;
        [Inject] private ISaveLoadService _saveLoadService;
        [Inject] private IGameplayBuildingsRepository _buildings;
        [Inject] private IGridRepository _gridRepository;
        [Inject] private ICurrencyRepository _currencyRepository;
        [Inject] private ISubscriber<TryPlaceDTO> _subscriber;
        
        [Inject] private GridView _gridView;

        private GridModel _gridModel;
        private CurrencyModel _currencyModel;

        private Dictionary<string, IBuildingRepository> _bindRepositories;

        public void Initialize()
        {
            _subscriber.Subscribe(this);
            
            _bindRepositories = _buildings.Repositories.
                ToDictionary(key => key.Name, value => value);
        }

        public void PostInitialize()
        {
            _gridModel = 
                _saveLoadService.Load<GridModel, IGridRepository>(nameof(GridModel), _gridRepository);

            _currencyModel =
                _saveLoadService.Load<CurrencyModel, ICurrencyRepository>(nameof(CurrencyType.Gold), _currencyRepository);
        }

        public void Handle(TryPlaceDTO message)
        {
            IBuildingRepository repository = _bindRepositories[message.BuildingName];
            
            if (_currencyModel.IsEnough(repository.Price) == false)
                return;
            
            if(_gridModel.IsEmpty(message.Position) == false)
                return;

            BuildingView prefab = Object.Instantiate(repository.Prefab);
            prefab.transform.position = message.WorldPosition.AsUnity();
            
            _gridModel.Place(_factoryService.Create<Building, IBuildingRepository>(repository), message.Position);

            _currencyModel.TrySpend(repository.Price);
        }

        public void Dispose()
        {
            
        }
    }
}