using Infrastructure.Repositories.Buildings.Effects;
using Presentation.Gameplay.Views.Buildings;
using TriInspector;
using UnityEngine;

namespace Infrastructure.Repositories.Buildings
{
    [CreateAssetMenu(fileName = "BuildingRepository", 
        menuName = "Gameplay/Building Repository")]
    public class BuildingRepository : ScriptableObject
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

        public BuildingView Prefab => _prefab;
        
        public BuildingEffectRepositoryBase Effect => _effect;
    }
}