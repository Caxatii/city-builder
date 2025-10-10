using System.Collections.Generic;
using System.Linq;
using ContractsInterfaces.Repositories;
using UnityEngine;

namespace Infrastructure.Repositories.Gameplay.Buildings
{
    [CreateAssetMenu(fileName = "GameplayBuildingsRepository", 
        menuName = "Gameplay/Gameplay Buildings Repository")]
    public class GameplayBuildingsRepository : ScriptableObject, IGameplayBuildingsRepository
    {
        [SerializeField] private BuildingRepository[] _repositories;

        public IReadOnlyList<IBuildingRepository> Repositories => _repositories;
        public IBuildingRepository Get(string name)
        {
            return _repositories.FirstOrDefault(r => r.name == name);
        }
    }
}