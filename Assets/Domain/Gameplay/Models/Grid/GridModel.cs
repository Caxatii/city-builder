using System;
using System.Collections.Generic;
using Domain.Gameplay.Models.Buildings;

namespace Domain.Gameplay.Models.Grid
{
    public class GridModel
    {
        private Cell[,] _cells;

        public GridModel(int width, int height)
        {
            _cells = new Cell[width, height];
        }

        public bool IsEmpty(GridPosition position) => 
            IsInRange(position) && _cells[position.Width, position.Height].IsEmpty;

        public bool IsInRange(GridPosition position) =>
            position.Width < _cells.GetLength(0) && 
            position.Height < _cells.GetLength(1);

        public IEnumerable<Building> EnumerateBuildings()
        {
            foreach (Cell cell in _cells)
                if (cell.IsEmpty == false)
                    yield return cell.Building;
        }

        public void Place(Building building, GridPosition position)
        {
            ValidateGridPosition(position);

            ref Cell cell = ref _cells[position.Width, position.Height];
            
            if (cell.IsEmpty == false)
                throw new ArgumentException($"Cannot place building in occupied cell. Position - {position}");

            cell = new Cell(building);
        }

        public void Remove(GridPosition position)
        {
            ValidateGridPosition(position);

            _cells[position.Width, position.Height] = new Cell();
        }

        private void ValidateGridPosition(GridPosition position)
        {
            if (IsInRange(position))
                return;

            GridPosition selfGrid = 
                new GridPosition(_cells.GetLength(0) - 1, _cells.GetLength(1) - 1);
            
            throw new ArgumentOutOfRangeException($"Position out of range. \n Max - {selfGrid} \n Received - {position}");
        }
    }
}