using System;
using Domain.Gameplay.Models.Grid;

namespace Domain.Gameplay.Models.Buildings
{
    public class SelectedCellModel
    {
        private GridPosition _position;

        public event Action Changed;

        public event Action Deselected; 
        
        public GridPosition Position
        {
            get => _position;
            set
            {
                _position = value;
                Changed?.Invoke();
            }
        }

        public void Deselect()
        {
            Deselected?.Invoke();
        }
    }
}