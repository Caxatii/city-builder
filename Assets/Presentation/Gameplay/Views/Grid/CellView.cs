using System;
using TriInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Presentation.Gameplay.Views.Grid
{
    public class CellView : MonoBehaviour, ICellView, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField, Required] private MeshRenderer _renderer;

        private Color _defaultColor;
        
        private ICellSelectionAction[] _selectionActions;

        public event Action<CellView> Clicked;
        public event Action<CellView> PointerEntered;
        public event Action<CellView> PointerExit;

        public Vector2Int Position { get; private set; }
        
        private void Awake()
        {
            _selectionActions = GetComponents<ICellSelectionAction>();
            _defaultColor = _renderer.material.color;
        }

        public void Initialize(Vector2Int position)
        {
            Position = position;
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