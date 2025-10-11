using ContractsInterfaces.Repositories;
using ContractsInterfaces.ServicesApplication;
using ContractsInterfaces.UseCasesApplication;
using Core;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.Buildings;
using Domain.Gameplay.Models.Grid;
using MessagePipe;
using Presentation.Gameplay.Views.Grid;
using VContainer;
using VContainer.Unity;

namespace Application.UseCases.Grid
{
    public class GridRemoveBuildUseCase : IUseCase, IPostInitializable, IMessageHandler<RemoveBuildDTO>
    {
        [Inject] private ISaveLoadService _saveLoadService;
        [Inject] private ISubscriber<RemoveBuildDTO> _subscriber;
        [Inject] private IGridView _view;
        
        private GridModel _model;
        private SelectedCellModel _selectedCell;
        private GridPosition? _currentPosition;
        
        public void Initialize()
        {
            _subscriber.Subscribe(this);
        }

        public void PostInitialize()
        {
            _model = _saveLoadService.Load<GridModel, IGridRepository>();
            _selectedCell = _saveLoadService.Load<SelectedCellModel>();
            
            _selectedCell.Changed += OnSelectedCellChanged;
            _selectedCell.Deselected += OnSelectedCellDeselected;
        }

        private void OnSelectedCellDeselected()
        {
            _currentPosition = null;
        }

        private void OnSelectedCellChanged()
        {
            _currentPosition = _selectedCell.Position;
        }

        public void Handle(RemoveBuildDTO message)
        {
            if(_currentPosition == null || _model.IsEmpty(_selectedCell.Position))
                return;
            
            _model.Remove(_currentPosition.Value);
            _view.Remove(_currentPosition.Value.AsUnity());
        }

        public void Dispose()
        {
            _selectedCell.Changed -= OnSelectedCellChanged;
            _selectedCell.Deselected -= OnSelectedCellDeselected;
        }
    }
}