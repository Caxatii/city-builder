using Domain.Gameplay.Models.Buildings;

namespace Domain.Gameplay.MessagesDTO
{
    public readonly struct SpawnBuildingViewDTO
    {
        public readonly Building Building;

        public SpawnBuildingViewDTO(Building building)
        {
            Building = building;
        }
    }
}