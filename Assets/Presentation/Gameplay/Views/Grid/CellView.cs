using System;
using TriInspector;
using UnityEngine;

namespace Presentation.Gameplay.Views.Grid
{
    public class CellView : MonoBehaviour
    {
        [SerializeField, Required] private MeshRenderer _renderer;
        
        private ICellSelectionAction[] _selectionActions;

        public Action Clicked;

        public Vector2Int Position { get; private set; }
        
        private void Awake()
        {
            _selectionActions = GetComponents<ICellSelectionAction>();
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

        public void Click()
        {
            Clicked?.Invoke();
        }
    }
}