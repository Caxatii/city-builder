using Domain.Gameplay.Models.Grid;

namespace Domain.Gameplay.MessagesDTO
{
    public readonly struct RemoveBuildDTO
    {
        public readonly GridPosition Position;

        public RemoveBuildDTO(GridPosition position)
        {
            Position = position;
        }
    }
}