using Domain.Gameplay.Models.Grid;

namespace Domain.Gameplay.MessagesDTO
{
    public readonly struct SpawnCellViewDTO
    {
        public readonly Vector3 WorldPosition;
        public readonly Vector3 Scale;
        public readonly GridPosition GridPosition;

        public SpawnCellViewDTO(Vector3 worldPosition, Vector3 scale, GridPosition gridPosition)
        {
            WorldPosition = worldPosition;
            GridPosition = gridPosition;
            Scale = scale;
        }
    }
}