using Domain.Gameplay.Models.Grid;

namespace Domain.Gameplay.MessagesDTO
{
    public readonly struct PointerEnteredDTO
    {
        public readonly GridPosition Position;

        public PointerEnteredDTO(GridPosition position)
        {
            Position = position;
        }
    }
}