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
    public class GridSelectionUseCase : IUseCase, IPostInitializable
    {
        [Inject] private GridView _gridView;
        [Inject] private ISaveLoadService _saveLoadService;
        
        private GridModel _gridModel;
        private SelectedCellModel _selectedCellModel;
        
        public void Initialize()
        {
            _gridView.PointerEnter += OnPointerEnter;
            _gridView.PointerExit += OnPointerExit;
        }

        public void PostInitialize()
        {
            _gridModel = _saveLoadService.Load<GridModel, IGridRepository>();
            _selectedCellModel = _saveLoadService.Load<SelectedCellModel>();
        }

        private void OnPointerEnter(CellView view)
        {
            _selectedCellModel.Position = view.Position.AsDomain();
        }

        private void OnPointerExit(CellView view)
        {
            _selectedCellModel.Deselect();
        }

        public void Dispose()
        {
            _gridView.PointerEnter -= OnPointerEnter;
            _gridView.PointerExit -= OnPointerExit;
        }
    }
}