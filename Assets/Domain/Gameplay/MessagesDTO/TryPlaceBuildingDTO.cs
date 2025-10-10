using Domain.Gameplay.Models.Grid;

namespace Domain.Gameplay.MessagesDTO
{
    public readonly struct TryPlaceBuildingDTO
    {
        public readonly string BuildingName;
        public readonly GridPosition Position;

        public TryPlaceBuildingDTO(string buildingName, GridPosition position)
        {
            BuildingName = buildingName;
            Position = position;
        }
    }
}