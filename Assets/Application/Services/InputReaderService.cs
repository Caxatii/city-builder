using ContractsInterfaces.ServicesApplication;
using Core;
using Domain.Gameplay.MessagesDTO;
using MessagePipe;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Application.Services
{
    public class InputReaderService : IInputReaderService
    {
        [Inject] private IPublisher<RawMoveInputDTO> _movePublisher;
        [Inject] private IPublisher<ScrollChangedDTO> _scrollPublisher;

        private InputSystemActions _actions;
        
        public void Initialize()
        {
            _actions = new InputSystemActions();
            _actions.Player.Enable();
            
            _actions.Player.Scroll.performed += OnScrollPerformed;
        }

        private void OnScrollPerformed(InputAction.CallbackContext context)
        {
            _scrollPublisher.Publish(new ScrollChangedDTO(context.ReadValue<Vector2>().y));
        }

        public void Tick()
        {
            Vector2 direction = _actions.Player.Move.ReadValue<Vector2>() * Time.deltaTime;
            
            if(direction.magnitude > 0)
                _movePublisher.Publish(new RawMoveInputDTO(direction.AsDomain()));
        }

        public void Dispose()
        {
            _actions.Player.Scroll.performed -= OnScrollPerformed;
            _actions?.Dispose();
        }
    }
}