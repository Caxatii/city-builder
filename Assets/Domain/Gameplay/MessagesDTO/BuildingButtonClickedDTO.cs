namespace Domain.Gameplay.MessagesDTO
{
    public readonly struct BuildingButtonClickedDTO
    {
        public readonly string Name;

        public BuildingButtonClickedDTO(string name)
        {
            Name = name;
        }
    }
}