using ContractsInterfaces.FactoriesApplication;
using ContractsInterfaces.Repositories;
using Domain.Gameplay.Models.Camera;

namespace Application.Services.Factories
{
    public class CameraSpeedFactory : ICameraSpeedFactory
    {
        public CameraSpeedModel Create(ICameraSpeedRepository config)
        {
            return new CameraSpeedModel{MaxSpeed = config.MaxSpeed, MinSpeed = config.MinSpeed};
        }
    }
}