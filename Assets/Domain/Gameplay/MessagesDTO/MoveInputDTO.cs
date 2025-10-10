namespace Domain.Gameplay.MessagesDTO
{
    public readonly struct RawMoveInputDTO
    {
        public readonly Vector3 Direction;

        public RawMoveInputDTO(Vector3 direction)
        {
            Direction = direction;
        }
    }
}