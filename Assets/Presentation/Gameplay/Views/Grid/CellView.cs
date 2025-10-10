using System;
using Presentation.Gameplay.Views.Buildings;
using TriInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Presentation.Gameplay.Views.Grid
{
    public class CellView : MonoBehaviour, ICellView, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField, Required] private MeshRenderer _renderer;

        private Color _defaultColor;
        private BuildingView _buildingView;
        private ICellSelectionAction[] _selectionActions;
        
        public Vector2Int Position { get; private set; } 

        public event Action<CellView> Clicked;
        public event Action<CellView> PointerEntered;
        public event Action<CellView> PointerExit;
        
        private void Awake()
        {
            _selectionActions = GetComponents<ICellSelectionAction>();
            _defaultColor = _renderer.material.color;
        }

        public void Initialize(Vector2Int position)
        {
            Position = position;
        }

        public void Place(BuildingView view)
        {
            _buildingView = view;
        }

        public void Remove()
        {
            if(_buildingView == null)
                return;
            
            Destroy(_buildingView.gameObject);
            _buildingView = null;
        }
        
        public void SetSelected(bool isSelected)
        {
            foreach (ICellSelectionAction action in _selectionActions) 
                action.SetSelected(isSelected);
        }

        public void SetColor(Color color)
        {
            _renderer.material.color = color;
        }

        public void Click() => 
            Clicked?.Invoke(this);

        public void OnPointerClick(PointerEventData eventData) => 
            Click();

        public void OnPointerEnter(PointerEventData eventData) => 
            PointerEntered?.Invoke(this);

        public void OnPointerExit(PointerEventData eventData) => 
            PointerExit?.Invoke(this);

        public void SetDefaultColor()
        {
            SetColor(_defaultColor);
        }
    }
}