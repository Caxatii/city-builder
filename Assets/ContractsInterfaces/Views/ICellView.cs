using System;
using ContractsInterfaces.Repositories;
using UnityEngine;

namespace Presentation.Gameplay.Views.Grid
{
    public interface ICellView
    {
        public event Action<ICellView> Clicked;
        public event Action<ICellView> PointerEntered;
        public event Action<ICellView> PointerExit;
        
        Vector2Int Position { get; }
        Transform transform { get; } 
        void Initialize(Vector2Int position);
        void SetSelected(bool isSelected);
        void SetColor(Color color);
        void Click();
        void Place(IBuildingView view);
        void Remove();
        void SetDefaultColor();
    }
}