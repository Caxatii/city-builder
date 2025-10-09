using Domain.Gameplay.Models.Buildings.Effects;

namespace Domain.Gameplay.Models.Buildings
{
    public class Building
    {
        public Building(int level, string name, EffectBase effect)
        {
            Level = level;
            Effect = effect;
            Name = name;
        }
        
        public int Level { get; }
        
        public string Name { get; }
        
        public EffectBase Effect { get; }
    }
}