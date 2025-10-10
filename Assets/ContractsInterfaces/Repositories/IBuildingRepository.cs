using Infrastructure.Repositories.Gameplay.Buildings.Effects;
using Presentation.Gameplay.Views.Buildings;
using UnityEngine;

namespace ContractsInterfaces.Repositories
{
    public interface IBuildingRepository : IRepository
    {
        public int Level { get; }
        public int Price { get; }
        public string Name { get; }
        public BuildingEffectRepositoryBase Effect { get; }
        
        public Sprite Preview { get; }
        public BuildingView Prefab { get; }
    }
}