using ContractsInterfaces.ServicesApplication;
using Core;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models;
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
        [Inject] private IPublisher<HotKeyPressedDTO> _hotKeyPublisher;
        [Inject] private IPublisher<RemoveBuildDTO> _removePublisher;

        private InputSystemActions _actions;
        
        public void Initialize()
        {
            _actions = new InputSystemActions();
            _actions.Player.Enable();
            
            _actions.Player.Scroll.performed += OnScrollPerformed;
            
            _actions.Player.HotKey1.performed += OnHotKey1Performed;
            _actions.Player.HotKey2.performed += OnHotKey2Performed;
            _actions.Player.HotKey3.performed += OnHotKey3Performed;
            
            _actions.Player.Delete.performed += OnDeletePerformed;
        }

        private void OnDeletePerformed(InputAction.CallbackContext context) => 
            _removePublisher.Publish(new RemoveBuildDTO());

        private void OnHotKey1Performed(InputAction.CallbackContext context) => 
            SendHotKey(0);

        private void OnHotKey2Performed(InputAction.CallbackContext context) => 
            SendHotKey(1);

        private void OnHotKey3Performed(InputAction.CallbackContext context) => 
            SendHotKey(2);

        private void SendHotKey(int key) => 
            _hotKeyPublisher.Publish(new HotKeyPressedDTO(key));

        private void OnScrollPerformed(InputAction.CallbackContext context) => 
            _scrollPublisher.Publish(new ScrollChangedDTO(context.ReadValue<Vector2>().y));

        public void Tick()
        {
            Vector2 direction = _actions.Player.Move.ReadValue<Vector2>() * Time.deltaTime;
            
            if(direction.magnitude > 0)
                _movePublisher.Publish(new RawMoveInputDTO(direction.AsDomain()));
        }

        public void Dispose()
        {
            _actions.Player.Scroll.performed -= OnScrollPerformed;
            
            _actions.Player.HotKey1.performed -= OnHotKey1Performed;
            _actions.Player.HotKey2.performed -= OnHotKey2Performed;
            _actions.Player.HotKey3.performed -= OnHotKey3Performed;
            
            _actions.Player.Delete.performed -= OnDeletePerformed;
            
            _actions?.Dispose();
        }
    }
}