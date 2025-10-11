using System;
using System.Collections.Generic;
using ContractsInterfaces.Repositories;
using Presentation.Gameplay.Views.Buildings;
using UnityEngine;

namespace Presentation.Gameplay.Views.Grid
{
    public class GridView : MonoBehaviour, IGridView 
    {
        private Dictionary<Vector2Int, CellView> _cellViews = new();

        public event Action<ICellView> Clicked;
        public event Action<ICellView> PointerEntered;
        public event Action<ICellView> PointerExit;
        
        private void OnDisable()
        {
            foreach (CellView view in _cellViews.Values)
            {
                view.Clicked -= OnClicked;
                view.PointerEntered -= OnPointerEntered;
                view.PointerExit -= OnPointerExit;
            }
        }

        public void Initialize(ICellView cellView, IGridRepository repository)
        {
            Vector3 position = transform.position;
                    
            for (int i = 1; i <= repository.GridSize.x; i++)
            {
                for (int j = 1; j <= repository.GridSize.y; j++)
                {
                    Vector2Int gridPosition = new Vector2Int(i, j);
                    CellView view = Instantiate(cellView as CellView);
                    
                    view.transform.localScale = Vector3.one * repository.CellSize;

                    Vector3 viewPosition = position;
                    viewPosition.x += i * repository.CellSize;
                    viewPosition.z += j * repository.CellSize;
                    
                    view.transform.position = viewPosition;
                    view.Initialize(gridPosition);

                    view.Clicked += OnClicked;
                    view.PointerEntered += OnPointerEntered;
                    view.PointerExit += OnPointerExit;
                    
                    _cellViews.Add(gridPosition, view);
                }
            }
        }

        public void Place(IBuildingView view, Vector2Int position)
        {
            if(_cellViews.TryGetValue(position, out CellView cell) == false)
                return;
            
            view.transform.position = cell.transform.position;
            cell.Place(view);
        }

        public void Remove(Vector2Int position)
        {
            if(_cellViews.TryGetValue(position, out CellView cell) == false)
                return;
            
            cell.Remove();
        }

        public ICellView GetCell(Vector2Int position)
        {
            _cellViews.TryGetValue(position, out CellView cell);
            return cell;
        }
        
        private void OnPointerEntered(ICellView view)
        {
            PointerEntered?.Invoke(view);
        }

        private void OnClicked(ICellView view)
        {
            Clicked?.Invoke(view);
        }

        private void OnPointerExit(ICellView view)
        {
            PointerExit?.Invoke(view);
        }
    }
}