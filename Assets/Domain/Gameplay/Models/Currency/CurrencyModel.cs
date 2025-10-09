using System;

namespace Domain.Gameplay.Models.Currency
{
    public class CurrencyModel
    {
        public int Value { get; private set; }

        public bool IsEnough(int value) => Value >= value;

        public bool TrySpend(int value)
        {
            ValidateNegativeValue(value, nameof(TrySpend));
            
            if (IsEnough(value) == false)
                return false;

            Value -= value;
            return true;
        }

        public void Increase(int value)
        {
            ValidateNegativeValue(value, nameof(Increase));

            Value += value;
        }

        private void ValidateNegativeValue(int value, string operationName)
        {
            if (value < 0)
                throw new ArgumentException($"{operationName} - value cannot be negative. Received - {value}");
        }
    }
}