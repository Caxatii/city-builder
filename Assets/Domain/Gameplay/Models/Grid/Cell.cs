using System;
using Domain.Gameplay.Models.Buildings;

namespace Domain.Gameplay.Models.Grid
{
    [Serializable]
    public struct Cell
    {
        public Cell(Building building)
        {
            Building = building;
        }

        public bool IsEmpty => Building == null;
        
        public Building Building { get; }
    }
}