using ContractsInterfaces.ServicesApplication;
using ContractsInterfaces.UseCasesApplication;
using Core;
using Domain.Gameplay.Models.Buildings;
using Presentation.Gameplay.Views.Grid;
using VContainer;
using VContainer.Unity;

namespace Application.UseCases.Grid
{
    public class GridSelectionUseCase : IUseCase, IPostInitializable
    {
        [Inject] private IGridView _gridView;
        [Inject] private ISaveLoadService _saveLoadService;
        
        private SelectedCellModel _selectedCellModel;
        
        public void Initialize()
        {
            _gridView.PointerEntered += OnPointerEntered;
            _gridView.PointerExit += OnPointerExit;
        }

        public void PostInitialize()
        {
            _selectedCellModel = _saveLoadService.Load<SelectedCellModel>();
        }

        private void OnPointerEntered(ICellView view)
        {
            _selectedCellModel.Position = view.Position.AsDomain();
        }

        private void OnPointerExit(ICellView view)
        {
            _selectedCellModel.Deselect();
        }

        public void Dispose()
        {
            _gridView.PointerEntered -= OnPointerEntered;
            _gridView.PointerExit -= OnPointerExit;
        }
    }
}