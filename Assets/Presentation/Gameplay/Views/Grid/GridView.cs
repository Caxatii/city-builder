using System;
using System.Collections.Generic;
using ContractsInterfaces.Repositories;
using Domain.Gameplay.MessagesDTO;
using MessagePipe;
using UnityEngine;
using VContainer;

namespace Presentation.Gameplay.Views.Grid
{
    public class GridView : MonoBehaviour
    {
        [Inject] private IPublisher<PointerEnteredDTO> _enterPublisher;

        private List<CellView> _cellViews = new();

        public event Action<CellView> Clicked;
        public event Action<CellView> PointerEnter;
        public event Action<CellView> PointerExit;
        
        private void OnDisable()
        {
            foreach (CellView view in _cellViews)
            {
                view.Clicked -= OnClicked;
                view.PointerEntered -= OnPointerEntered;
                view.PointerExit -= OnPointerExit;
            }
        }

        public void Initialize(CellView cellView, IGridRepository repository)
        {
            Vector3 position = transform.position;
                    
            for (int i = 1; i <= repository.GridSize.x; i++)
            {
                for (int j = 1; j <= repository.GridSize.y; j++)
                {
                    CellView view = Instantiate(cellView);
                    view.transform.localScale = Vector3.one * repository.CellSize;

                    Vector3 viewPosition = position;
                    viewPosition.x += i * repository.CellSize;
                    viewPosition.z += j * repository.CellSize;
                    
                    view.transform.position = viewPosition;
                    view.Initialize(new Vector2Int(i, j));

                    view.Clicked += OnClicked;
                    view.PointerEntered += OnPointerEntered;
                    view.PointerExit += OnPointerExit;
                    
                    _cellViews.Add(view);
                }
            }
        }

        private void OnPointerEntered(CellView view)
        {
            PointerEnter?.Invoke(view);
        }

        private void OnClicked(CellView view)
        {
            Clicked?.Invoke(view);
        }

        private void OnPointerExit(CellView view)
        {
            PointerExit?.Invoke(view);
        }
    }
}