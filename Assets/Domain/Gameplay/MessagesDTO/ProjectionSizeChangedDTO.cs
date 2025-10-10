namespace Domain.Gameplay.MessagesDTO
{
    public readonly struct ProjectionSizeChangedDTO
    {
        public readonly float Value;

        public ProjectionSizeChangedDTO(float value)
        {
            Value = value;
        }
    }
}