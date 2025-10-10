namespace Domain.Gameplay.MessagesDTO
{
    public readonly struct ScrollChangedDTO
    {
        public readonly float Value;

        public ScrollChangedDTO(float value)
        {
            Value = value;
        }
    }
}