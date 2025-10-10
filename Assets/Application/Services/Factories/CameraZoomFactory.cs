using ContractsInterfaces.Repositories;
using Domain.Gameplay.Models.Camera;

namespace Application.Services.Factories
{
    public class CameraZoomFactory : ICameraZoomFactory
    {
        public CameraZoomModel Create(ICameraZoomRepository config)
        {
            return new CameraZoomModel(config.MinProjectionSize,
                config.MaxProjectionSize,
                config.CurrentProjectionSize);
        }
    }
}