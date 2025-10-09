using UnityEngine;

namespace Infrastructure.Repositories.Buildings.Effects
{
    [CreateAssetMenu(fileName = "IncreaseGoldEffectRepository", 
        menuName = "Gameplay/Building Effects/Increase Gold Effect Repository")]
    public class IncreaseGoldEffectRepository : BuildingEffectRepositoryBase
    {
        [SerializeField] private int _value;

        public int Value => _value;
    }
}