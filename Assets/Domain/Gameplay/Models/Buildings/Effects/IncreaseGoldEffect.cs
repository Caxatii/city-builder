namespace Domain.Gameplay.Models.Buildings.Effects
{
    public class IncreaseGoldEffect : EffectBase
    {
        public IncreaseGoldEffect(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }
}