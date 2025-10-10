using System.Collections.Generic;
using System.Linq;
using ContractsInterfaces.Repositories;
using ContractsInterfaces.UseCasesApplication;
using Domain.Gameplay.MessagesDTO;
using MessagePipe;
using Presentation.Gameplay.Views.UI;
using VContainer;

namespace Application.UseCases.UI
{
    public class SidePanelInitializeUseCase : IUseCase, IMessageHandler<HotKeyPressedDTO>
    {
        [Inject] private IGameplayBuildingsRepository _repositories;
        [Inject] private SidePanelView _view;
        
        [Inject] private IPublisher<BuildingButtonClickedDTO> _publisher;
        [Inject] private ISubscriber<HotKeyPressedDTO> _subscriber;

        private Dictionary<ButtonView, IBuildingRepository> _bindedRepositories = new();
        
        public void Initialize()
        {
            _view.Initialize();

            _subscriber.Subscribe(this);
            
            int index = 0;
            
            foreach (ButtonView view in _view.Views)
            {
                var repository = _repositories.Repositories[index++];
                view.Sprite = repository.Preview;
                view.Text = repository.Price.ToString();
                
                view.Clicked += OnClicked;
                _bindedRepositories.Add(view, repository);
            }
        }

        private void OnClicked(ButtonView view)
        {
            _publisher.Publish(new BuildingButtonClickedDTO(_bindedRepositories[view].Name));
        }

        public void Handle(HotKeyPressedDTO message)
        {
            if(_bindedRepositories.Count < message.Key)
                return;
            
            OnClicked(_bindedRepositories.Keys.ElementAt(message.Key));
        }

        public void Dispose()
        {
            foreach (ButtonView view in _bindedRepositories.Keys) 
                view.Clicked -= OnClicked;
        }
    }
}