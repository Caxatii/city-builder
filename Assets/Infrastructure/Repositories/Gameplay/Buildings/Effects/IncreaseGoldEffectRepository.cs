using ContractsInterfaces.Repositories;
using UnityEngine;

namespace Infrastructure.Repositories.Gameplay.Buildings.Effects
{
    [CreateAssetMenu(fileName = "IncreaseGoldEffectRepository", 
        menuName = "Gameplay/Building Effects/Increase Gold Effect Repository")]
    public class IncreaseGoldEffectRepository : BuildingEffectRepositoryBase, IIncreaseGoldEffectRepository
    {
        [SerializeField] private int _value;

        public int Value => _value;
    }
}