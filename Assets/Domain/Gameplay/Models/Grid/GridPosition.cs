using System;

namespace Domain.Gameplay.Models.Grid
{
    [Serializable]
    public readonly struct GridPosition
    {
        public readonly int Width;
        public readonly int Height;

        public GridPosition(int width, int height)
        {
            if (width < 0 || height < 0)
                throw new ArgumentOutOfRangeException("Position cannot be negative");
            
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"Width: {Width}, Height: {Height}";
        }
    }
}