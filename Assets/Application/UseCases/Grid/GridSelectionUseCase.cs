using ContractsInterfaces.Repositories;
using ContractsInterfaces.ServicesApplication;
using ContractsInterfaces.UseCasesApplication;
using Core;
using Domain.Gameplay.Models.Grid;
using Presentation.Gameplay.Views.Grid;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Application.UseCases.Grid
{
    public class GridSelectionUseCase : IUseCase, IPostInitializable
    {
        [Inject] private GridView _gridView;
        [Inject] private ISaveLoadService _saveLoadService;
        [Inject] private IGridRepository _gridRepository;

        private Color _green = Color.green;
        private Color _red = Color.red;
        
        private GridModel _gridModel;
        
        public void Initialize()
        {
            _gridView.PointerEnter += OnPointerEnter;
            _gridView.PointerExit += OnPointerExit;

            _green.a = .2f;
            _red.a = .2f;
        }

        public void PostInitialize()
        {
            _gridModel = 
                _saveLoadService.Load<GridModel, IGridRepository>(nameof(GridModel), _gridRepository);
        }

        private void OnPointerEnter(CellView view)
        {
            view.SetColor(_gridModel.IsEmpty(view.Position.AsDomain()) ? _green : _red);
        }

        private void OnPointerExit(CellView view)
        {
            view.SetDefaultColor();
        }

        public void Dispose()
        {
            _gridView.PointerEnter -= OnPointerEnter;
            _gridView.PointerExit -= OnPointerExit;
        }
    }
}