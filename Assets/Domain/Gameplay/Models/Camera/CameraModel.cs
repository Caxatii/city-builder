using System;

namespace Domain.Gameplay.Models.Camera
{
    public class CameraZoomModel
    {
        private float _currentProjectionSize;

        public CameraZoomModel(float minProjectionSize, float maxProjectionSize, float currentProjectionSize = 0)
        {
            ValidateMinMaxSize(minProjectionSize, maxProjectionSize);
            
            MinProjectionSize = minProjectionSize;
            MaxProjectionSize = maxProjectionSize;
            
            CurrentProjectionSize = currentProjectionSize == 0 ? 
                maxProjectionSize :  
                Math.Clamp(currentProjectionSize, MinProjectionSize, MaxProjectionSize);
        }

        public float CurrentProjectionSize
        {
            get => _currentProjectionSize; 
            set => SetSize(value);
        }

        public float MaxProjectionSize { get; }

        public float MinProjectionSize { get; }

        public void SetSize(float value)
        {
            _currentProjectionSize = Math.Clamp(value, MinProjectionSize, MaxProjectionSize);
        }

        private void ValidateMinMaxSize(float minProjectionSize, float maxProjectionSize)
        {
            if (minProjectionSize > maxProjectionSize)
                throw new ArgumentException($"Minimal projection size cannot be bigger than maximal." +
                                            $" Min = {minProjectionSize}, Max = {maxProjectionSize}");
        }
    }
}