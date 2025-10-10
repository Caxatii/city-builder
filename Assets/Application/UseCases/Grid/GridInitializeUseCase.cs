using ContractsInterfaces.Repositories;
using ContractsInterfaces.ServicesApplication;
using ContractsInterfaces.UseCasesApplication;
using Core;
using Domain.Gameplay.Models.Grid;
using Presentation.Gameplay.Views.Buildings;
using Presentation.Gameplay.Views.Grid;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Application.UseCases.Grid
{
    public class GridInitializeUseCase : IUseCase, IPostInitializable
    {
        [Inject] private GridView _view;
        [Inject] private CellView _cellView;
        
        [Inject] private ISaveLoadService _saveLoadService;
        [Inject] private IGridRepository _gridRepository;
        [Inject] private IGameplayBuildingsRepository _buildingsRepository;

        private GridModel _gridModel;
        
        public void Initialize() { }

        public void PostInitialize()
        {
            _gridModel = _saveLoadService.Load<GridModel, IGridRepository>(_gridRepository);
            
            _view.Initialize(_cellView, _gridRepository);
            
            foreach (var cell in _gridModel.EnumerateBuildings())
            {
                _view.Place(CreateBuild(cell.building.Name), cell.position.AsUnity());
            }
        }

        private BuildingView CreateBuild(string buildingName)
        {
            return Object.Instantiate(_buildingsRepository.Get(buildingName).Prefab);
        }

        public void Dispose() { }
    }
}