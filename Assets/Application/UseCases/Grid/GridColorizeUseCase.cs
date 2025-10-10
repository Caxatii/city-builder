using ContractsInterfaces.Repositories;
using ContractsInterfaces.ServicesApplication;
using ContractsInterfaces.UseCasesApplication;
using Core;
using Domain.Gameplay.Models.Buildings;
using Domain.Gameplay.Models.Grid;
using Presentation.Gameplay.Views.Grid;
using VContainer;
using VContainer.Unity;

namespace Application.UseCases.Grid
{
    public class GridColorizeUseCase : IUseCase, IPostInitializable
    {
        [Inject] private ICellColorizeRepository _colorizeRepository;
        [Inject] private ISaveLoadService _saveLoadService;
        [Inject] private GridView _gridView;

        private SelectedCellModel _selectedCell;
        private GridModel _gridModel;
        
        public void Initialize() { }

        public void PostInitialize()
        {
            _selectedCell = _saveLoadService.Load<SelectedCellModel>();
            _gridModel = _saveLoadService.Load<GridModel, IGridRepository>();
            
            _selectedCell.Changed += OnSelectedCellChanged;
            _selectedCell.Deselected += OnSelectedCellDeselected;
        }

        private void OnSelectedCellDeselected()
        {
            _gridView.GetCell(_selectedCell.Position.AsUnity()).SetDefaultColor();
        }

        private void OnSelectedCellChanged()
        {
            _gridView.GetCell(_selectedCell.Position.AsUnity())
                .SetColor(_gridModel.IsEmpty(_selectedCell.Position) ? 
                    _colorizeRepository.Accept : 
                    _colorizeRepository.Discard);
        }

        public void Dispose()
        {
            _selectedCell.Changed -= OnSelectedCellChanged;
            _selectedCell.Deselected -= OnSelectedCellDeselected;
        }
    }
}