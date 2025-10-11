using Application.UseCases.Camera;
using ContractsInterfaces.Installers;
using ContractsInterfaces.Repositories;
using ContractsInterfaces.ServicesApplication;
using Infrastructure.Repositories.Application.Camera;
using Presentation.Gameplay.Views;
using TriInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Installers
{
    [RequireComponent(typeof(ISaveLoadInstaller))]
    public class CameraInstaller : InstallerCommand
    {
        [SerializeField, Required] private CameraView _camera;
        [SerializeField, Required] private CameraRepository _cameraRepository;

        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_camera);
            
            builder.RegisterInstance<ICameraSpeedRepository>(_cameraRepository);
            builder.RegisterInstance<ICameraZoomRepository>(_cameraRepository);

            builder.RegisterEntryPoint<CameraMoveUseCase>();
            builder.RegisterEntryPoint<CameraZoomUseCase>();
        }
    }
}