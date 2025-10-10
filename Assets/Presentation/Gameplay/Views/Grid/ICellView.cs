using UnityEngine;

namespace Presentation.Gameplay.Views.Grid
{
    public interface ICellView
    {
        Vector2Int Position { get; }
        void Initialize(Vector2Int position);
        void SetSelected(bool isSelected);
        void SetColor(Color color);
        void Click();
    }
}