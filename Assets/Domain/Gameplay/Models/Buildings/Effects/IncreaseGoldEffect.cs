namespace Domain.Gameplay.Models.Buildings.Effects
{
    public class IncreaseGoldEffect : EffectBase
    {
        public IncreaseGoldEffect(int valuePerMinute)
        {
            ValuePerMinute = valuePerMinute;
        }

        public int ValuePerMinute { get; }
    }
}