using System;
using ContractsInterfaces.Repositories;
using UnityEngine;

namespace Presentation.Gameplay.Views.Grid
{
    public interface IGridView
    {
        public event Action<ICellView> Clicked;
        public event Action<ICellView> PointerEntered;
        public event Action<ICellView> PointerExit;

        public void Initialize(ICellView cellView, IGridRepository repository);
        public void Place(IBuildingView view, Vector2Int position);
        public void Remove(Vector2Int position);

        public ICellView GetCell(Vector2Int position);
    }
}