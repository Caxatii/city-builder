using Presentation.Gameplay.Views.Buildings;
using UnityEngine;

namespace Infrastructure.Configurations
{
    public readonly struct BuildingViewConfig
    {
        public readonly BuildingView Prefab;
        public readonly Vector3 SpawnPosition;
        
        public BuildingViewConfig(Vector3 spawnPosition, BuildingView prefab)
        {
            Prefab = prefab;
            SpawnPosition = spawnPosition;
        }
    }
}