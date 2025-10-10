using ContractsInterfaces.Repositories;
using ContractsInterfaces.ServicesApplication;
using ContractsInterfaces.UseCasesApplication;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.Camera;
using MessagePipe;
using Presentation.Gameplay.Views;
using VContainer;
using VContainer.Unity;

namespace Application.UseCases.Camera
{
    public class CameraZoomUseCase : IUseCase, IPostInitializable, IMessageHandler<ScrollChangedDTO>
    {
        [Inject] private CameraView _cameraView;
        [Inject] private ISubscriber<ScrollChangedDTO> _subscriber;
        [Inject] private ICameraZoomRepository _zoomRepository;
        [Inject] private ISaveLoadService _saveLoadService;
        
        private CameraZoomModel _zoomModel;

        public void Initialize()
        {
            _subscriber.Subscribe(this);
        }

        public void PostInitialize()
        {
            _zoomModel =
                _saveLoadService.Load<CameraZoomModel, ICameraZoomRepository>(typeof(CameraZoomModel), _zoomRepository);

            _cameraView.Camera.orthographicSize = _zoomModel.CurrentProjectionSize;
        }

        public void Handle(ScrollChangedDTO message)
        {
            _zoomModel.CurrentProjectionSize -= message.Value;

            _cameraView.Camera.orthographicSize = _zoomModel.CurrentProjectionSize;
        }

        public void Dispose() { }
    }
}