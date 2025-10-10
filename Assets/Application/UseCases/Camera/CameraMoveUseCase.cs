using ContractsInterfaces.Repositories;
using ContractsInterfaces.ServicesApplication;
using ContractsInterfaces.UseCasesApplication;
using Core;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.Camera;
using MessagePipe;
using Presentation.Gameplay.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Application.UseCases.Camera
{
    public class CameraMoveUseCase : ICameraMoveUseCase, IPostInitializable
    {
        [Inject] private ISaveLoadService _saveLoadService;
        [Inject] private ISubscriber<RawMoveInputDTO> _subscriber;
        [Inject] private CameraView _camera;

        private CameraSpeedModel _cameraSpeedModel;
        private CameraZoomModel _cameraZoomModel;

        public void Initialize()
        {
            _subscriber.Subscribe(this);
        }

        public void PostInitialize()
        {
            _cameraSpeedModel = _saveLoadService.Load<CameraSpeedModel, ICameraSpeedRepository>();
            _cameraZoomModel = _saveLoadService.Load<CameraZoomModel, ICameraZoomRepository>();
        }

        public void Handle(RawMoveInputDTO message)
        {
            Vector3 direction = message.Direction.AsUnity();
            direction.Normalize();
            direction *= GetSpeed();

            _camera.transform.position += direction.x * _camera.transform.right + direction.y * _camera.transform.forward;
        }

        private float GetSpeed()
        {
            return Mathf.Lerp(
                _cameraSpeedModel.MinSpeed,
                _cameraSpeedModel.MaxSpeed,
                _cameraZoomModel.CurrentProjectionSize / _cameraZoomModel.MaxProjectionSize);
        }

        public void Dispose() { }
    }
}