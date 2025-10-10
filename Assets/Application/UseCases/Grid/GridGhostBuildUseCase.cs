using System.Collections.Generic;
using System.Linq;
using Application.Services;
using ContractsInterfaces.Repositories;
using ContractsInterfaces.ServicesApplication;
using ContractsInterfaces.UseCasesApplication;
using Core;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.Currency;
using MessagePipe;
using Presentation.Gameplay.Views.Buildings;
using Presentation.Gameplay.Views.Grid;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Application.UseCases.Grid
{
    public class GridGhostBuildUseCase : IUseCase, IPostInitializable, IMessageHandler<BuildingButtonClickedDTO>
    {
        [Inject] private IGameplayBuildingsRepository _buildingsRepository;
        [Inject] private ISaveLoadService _saveLoadService;
        [Inject] private GridView _gridView;
        
        [Inject] private ISubscriber<BuildingButtonClickedDTO> _subscriber;
        [Inject] private IPublisher<NotEnoughResources> _notEnoughPublisher;
        [Inject] private IPublisher<TryPlaceDTO> _tryPlacePublisher;

        private CurrencyModel _currencyModel;
        private BuildingView _view;
        private CellView _currentCell;
        private IBuildingRepository _currentRepository;
        
        public void Initialize()
        {
            _subscriber.Subscribe(this);
            
            _gridView.PointerEnter += OnPointerEnter;
            _gridView.Clicked += OnClicked;
        }

        public void PostInitialize()
        {
            _currencyModel = _saveLoadService.Load<CurrencyModel, ICurrencyRepository>(nameof(CurrencyType.Gold));
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
            
            _view = Object.Instantiate(building.Prefab);
            _view.transform.position = _currentCell.transform.position;
            
            _currentRepository = building;
        }

        private void OnClicked(CellView view)
        {
            if(_view == null)
                return;
            
            _tryPlacePublisher.Publish(new TryPlaceDTO(_currentRepository.Name, view.Position.AsDomain()));
            
            Object.Destroy(_view.gameObject);
            _view = null;
        }

        private void OnPointerEnter(CellView view)
        {
            _currentCell = view;
            
            if(_view == null)
                return;
            
            _view.transform.position = view.transform.position;
        }

        public void Dispose()
        {
            _gridView.PointerEnter -= OnPointerEnter;
            _gridView.Clicked -= OnClicked;
        }
    }
}