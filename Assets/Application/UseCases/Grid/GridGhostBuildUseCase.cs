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
        [Inject] private ICurrencyRepository _currencyRepository;
        [Inject] private ISaveLoadService _saveLoadService;
        [Inject] private ISubscriber<BuildingButtonClickedDTO> _subscriber;
        [Inject] private IPublisher<NotEnoughResources> _notEnoughPublisher;
        [Inject] private IPublisher<TryPlaceDTO> _tryPlacePublisher;
        [Inject] private GridView _gridView;

        private CurrencyModel _currencyModel;
        private BuildingView _view;
        private IBuildingRepository _currentRepository;
        
        public void Initialize()
        {
            _subscriber.Subscribe(this);
            
            _gridView.PointerEnter += OnPointerEnter;
            _gridView.Clicked += OnClicked;
        }

        public void PostInitialize()
        {
            _currencyModel =
                _saveLoadService.Load<CurrencyModel, ICurrencyRepository>(nameof(CurrencyType.Gold), _currencyRepository);
        }

        public void Handle(BuildingButtonClickedDTO message)
        {
            IBuildingRepository building = _buildingsRepository.Get(message.Name);

            if (_currencyModel.IsEnough(building.Price) == false)
            {
                _notEnoughPublisher.Publish(new NotEnoughResources());
                return;
            }

            _view = Object.Instantiate(building.Prefab);
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