namespace Domain.Gameplay.MessagesDTO
{
    public readonly struct HotKeyPressedDTO
    {
        public readonly int Key;

        public HotKeyPressedDTO(int key)
        {
            Key = key;
        }
    }
}