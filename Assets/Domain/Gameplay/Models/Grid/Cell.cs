using Domain.Gameplay.Models.Buildings;

namespace Domain.Gameplay.Models.Grid
{
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