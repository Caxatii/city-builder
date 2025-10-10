using ContractsInterfaces.Repositories;
using Domain.Gameplay.Models.Camera;

namespace ContractsInterfaces.FactoriesApplication
{
    public interface ICameraSpeedFactory : IModelFactory<CameraSpeedModel, ICameraSpeedRepository> { }
}