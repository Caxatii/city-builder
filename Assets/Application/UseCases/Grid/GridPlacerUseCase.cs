using System;
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
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Application.UseCases.Grid
{
    public class GridPlacerUseCase : IUseCase, IPostInitializable, IMessageHandler<TryPlaceBuildingDTO>
    {
        [Inject] private IModelFactoryService _factoryService;
        [Inject] private ISaveLoadService _saveLoadService;
        [Inject] private IGameplayBuildingsRepository _buildings;
        [Inject] private IGridView _gridView;

        [Inject] private ISubscriber<TryPlaceBuildingDTO> _subscriber;
        
        [Inject] private Func<IBuildingView, BuildingView> _viewFactory;
        
        private GridModel _gridModel;
        private CurrencyModel _currencyModel;
        
        public void Initialize()
        {
            _subscriber.Subscribe(this);
        }

        public void PostInitialize()
        {
            _gridModel = _saveLoadService.Load<GridModel, IGridRepository>();

            _currencyModel = _saveLoadService.Load<CurrencyModel, ICurrencyRepository>(nameof(CurrencyType.Gold));
        }

        public void Handle(TryPlaceBuildingDTO message)
        {
            IBuildingRepository repository = _buildings.Get(message.BuildingName);
            
            if (_currencyModel.IsEnough(repository.Price) == false)
                return;
            
            if(_gridModel.IsEmpty(message.Position) == false)
                return;

            BuildingView prefab = _viewFactory(repository.Prefab);
            _gridView.Place(prefab, message.Position.AsUnity());
            
            _gridModel.Place(_factoryService.Create<Building, IBuildingRepository>(repository),
                message.Position);

            _currencyModel.TrySpend(repository.Price);
        }

        public void Dispose() { }
    }
}