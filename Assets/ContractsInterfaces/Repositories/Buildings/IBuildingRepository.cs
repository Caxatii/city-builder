using UnityEngine;

namespace ContractsInterfaces.Repositories
{
    public interface IBuildingRepository : IRepository
    {
        public int Level { get; }
        public int Price { get; }
        public string Name { get; }
        public IBuildingEffectRepository Effect { get; }
        
        public Sprite Preview { get; }
        public IBuildingView Prefab { get; }
    }
}