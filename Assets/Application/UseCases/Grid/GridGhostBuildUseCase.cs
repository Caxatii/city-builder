using System;
using ContractsInterfaces.Repositories;
using ContractsInterfaces.ServicesApplication;
using ContractsInterfaces.UseCasesApplication;
using Core;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.Buildings;
using Domain.Gameplay.Models.Currency;
using MessagePipe;
using Presentation.Gameplay.Views.Buildings;
using Presentation.Gameplay.Views.Grid;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Application.UseCases.Grid
{
    public class GridGhostBuildUseCase : IUseCase, IPostInitializable, IMessageHandler<BuildingButtonClickedDTO>
    {
        [Inject] private IGameplayBuildingsRepository _buildingsRepository;
        [Inject] private ISaveLoadService _saveLoadService;
        [Inject] private IGridView _gridView;
        
        [Inject] private Func<IBuildingView, BuildingView> _viewFactory;
        
        [Inject] private ISubscriber<BuildingButtonClickedDTO> _subscriber;
        [Inject] private IPublisher<NotEnoughResources> _notEnoughPublisher;
        [Inject] private IPublisher<TryPlaceBuildingDTO> _tryPlacePublisher;

        private CurrencyModel _currencyModel;
        private BuildingView _view;
        private SelectedCellModel _selectedCellModel;
        private IBuildingRepository _currentRepository;
        
        public void Initialize()
        {
            _subscriber.Subscribe(this);
            _gridView.Clicked += OnClicked;
        }

        public void PostInitialize()
        {
            _currencyModel = _saveLoadService.Load<CurrencyModel, ICurrencyRepository>(nameof(CurrencyType.Gold));
            _selectedCellModel = _saveLoadService.Load<SelectedCellModel>();
            
            _selectedCellModel.Changed += OnSelectedCellChanged;
        }

        private void OnSelectedCellChanged()
        {
            if(_view == null)
                return;
            
            _view.transform.position = GetCellViewPosition();
        }

        public void Handle(BuildingButtonClickedDTO message)
        {
            IBuildingRepository building = _buildingsRepository.Get(message.Name);

            if (_currencyModel.IsEnough(building.Price) == false)
            {
                _notEnoughPublisher.Publish(new NotEnoughResources());
                return;
            }

            if(_view)
                Object.Destroy(_view.gameObject);
            
            _view = _viewFactory(building.Prefab);
            _view.transform.position = GetCellViewPosition();
            
            _currentRepository = building;
        }

        private Vector3 GetCellViewPosition()
        {
            return _gridView.GetCell(_selectedCellModel.Position.AsUnity()).transform.position;
        }

        private void OnClicked(ICellView view)
        {
            if(_view == null)
                return;
            
            _tryPlacePublisher.Publish(new TryPlaceBuildingDTO(_currentRepository.Name, view.Position.AsDomain()));
            
            Object.Destroy(_view.gameObject);
            _view = null;
            _currentRepository = null;
        }

        public void Dispose()
        {
            _gridView.Clicked -= OnClicked;
        }
    }
}