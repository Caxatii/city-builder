using System;

namespace Domain.Gameplay.Models.Currency
{
    public class CurrencyModel
    {
        public CurrencyModel(int value = 0)
        {
            Value = value;
        }

        public int Value { get; private set; }

        public event Action<int> Changed; 

        public bool IsEnough(int value) => Value >= value;

        public bool TrySpend(int value)
        {
            ValidateNegativeValue(value, nameof(TrySpend));
            
            if (IsEnough(value) == false)
                return false;

            Value -= value;
            Changed?.Invoke(Value);
            return true;
        }

        public void Increase(int value)
        {
            ValidateNegativeValue(value, nameof(Increase));

            Value += value;
            Changed?.Invoke(Value);
        }

        private void ValidateNegativeValue(int value, string operationName)
        {
            if (value < 0)
                throw new ArgumentException($"{operationName} - value cannot be negative. Received - {value}");
        }
    }
}