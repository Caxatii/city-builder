using Domain.Gameplay.Models.Grid;

namespace Domain.Gameplay.MessagesDTO
{
    public readonly struct TryPlaceDTO
    {
        public readonly string BuildingName;
        public readonly GridPosition Position;
        public readonly Vector3 WorldPosition;

        public TryPlaceDTO(string buildingName, GridPosition position, Vector3 worldPosition)
        {
            BuildingName = buildingName;
            Position = position;
            WorldPosition = worldPosition;
        }
    }
}