using Application.Core.Attributes;
using ContractsInterfaces.Repositories;
using UnityEngine;

namespace Infrastructure.Repositories.Application.Camera
{
    [RepositoryType(typeof(ICameraSpeedRepository), typeof(ICameraZoomRepository))]
    [CreateAssetMenu(fileName = "CameraRepository", 
        menuName = "Gameplay/Settings/Camera Repository")]
    public class CameraRepository : RepositoryBase, ICameraSpeedRepository, ICameraZoomRepository
    {
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _minProjectionSize;
        [SerializeField] private float _maxProjectionSize;
        [SerializeField] private float _startProjectionSize;
        
        public float MinSpeed => _minSpeed;
        public float MaxSpeed => _maxSpeed;
        public float CurrentProjectionSize => _startProjectionSize;
        public float MinProjectionSize => _minProjectionSize;
        public float MaxProjectionSize => _maxProjectionSize;
    }
}