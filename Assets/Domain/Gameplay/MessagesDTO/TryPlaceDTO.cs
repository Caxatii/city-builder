using Domain.Gameplay.Models.Grid;

namespace Domain.Gameplay.MessagesDTO
{
    public readonly struct TryPlaceDTO
    {
        public readonly string BuildingName;
        public readonly GridPosition Position;

        public TryPlaceDTO(string buildingName, GridPosition position)
        {
            BuildingName = buildingName;
            Position = position;
        }
    }
}