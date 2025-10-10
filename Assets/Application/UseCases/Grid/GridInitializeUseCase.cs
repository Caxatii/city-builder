using ContractsInterfaces.Repositories;
using ContractsInterfaces.ServicesApplication;
using ContractsInterfaces.UseCasesApplication;
using Domain.Gameplay.Models.Grid;
using Presentation.Gameplay.Views.Grid;
using VContainer;
using VContainer.Unity;

namespace Application.UseCases.Grid
{
    public class GridInitializeUseCase : IUseCase, IPostInitializable
    {
        [Inject] private GridView _view;
        [Inject] private ISaveLoadService _saveLoadService;
        [Inject] private IGridRepository _gridRepository;
        [Inject] private CellView _cellView;

        private GridModel _gridModel;
        
        public void Initialize() { }

        public void PostInitialize()
        {
            _gridModel = 
                _saveLoadService.Load<GridModel, IGridRepository>(nameof(GridModel), _gridRepository);
            
            _view.Initialize(_cellView, _gridRepository);
        }

        public void Dispose() { }
    }
}