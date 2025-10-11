using Application.Services;
using Domain.Gameplay.MessagesDTO;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Installers
{
    public class TimersInstaller : InstallerCommand
    {
        [SerializeField] private float _rewardDelay;
        [SerializeField] private float _autoSaveDelay;
        
        public override void Configure(IContainerBuilder builder)
        {
            RegisterTimer<AccrueRemunerationDTO>(builder, _rewardDelay);
            RegisterTimer<SaveGameDTO>(builder, _autoSaveDelay);
        }
        
        private void RegisterTimer<TMessage>(IContainerBuilder builder, float delay) where TMessage : new()
        {
            builder.RegisterEntryPoint(resolver => 
                new TimerService<TMessage>(
                    delay,
                    resolver.Resolve<IPublisher<TMessage>>()), Lifetime.Singleton);
        }
    }
}