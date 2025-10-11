using ContractsInterfaces.FactoriesApplication;
using Domain.Gameplay.Models.Camera;

namespace ContractsInterfaces.Repositories
{
    public interface ICameraZoomFactory : IModelFactory<CameraZoomModel, ICameraZoomRepository> { }
}