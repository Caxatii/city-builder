using ContractsInterfaces.Repositories;
using Infrastructure.Repositories.Gameplay.Buildings.Effects;
using Presentation.Gameplay.Views.Buildings;
using TriInspector;
using UnityEngine;

namespace Infrastructure.Repositories.Gameplay.Buildings
{
    [CreateAssetMenu(fileName = "BuildingRepository", 
        menuName = "Gameplay/Building Repository")]
    public class BuildingRepository : RepositoryBase, IBuildingRepository
    {
        [SerializeField] private int _price;
        [SerializeField] private int _level;
        [SerializeField] private string _name;

        [SerializeField, PreviewObject] private Sprite _preview;
        [SerializeField] private BuildingView _prefab;
        [SerializeField] private BuildingEffectRepositoryBase _effect;

        public int Price => _price;

        public int Level => _level;

        public string Name => _name;

        public Sprite Preview => _preview;

        public IBuildingView Prefab => _prefab;
        
        public IBuildingEffectRepository Effect => _effect;
    }
}