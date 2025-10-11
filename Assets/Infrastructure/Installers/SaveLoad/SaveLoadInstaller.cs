using Application.Services;
using ContractsInterfaces.FactoriesApplication;
using ContractsInterfaces.Installers;
using ContractsInterfaces.ServicesApplication;
using Domain.Gameplay.MessagesDTO;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Installers
{
    [RequireComponent(typeof(IModelFactoryServiceInstaller))]
    public class SaveLoadInstaller : InstallerCommand
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint(CreateSaveLoadService, Lifetime.Singleton).As<ISaveLoadService>();
        }

        private ISaveLoadService CreateSaveLoadService(IObjectResolver resolver)
        {
            ISaveLoadService service = new SaveLoadService(resolver.Resolve<IModelFactoryService>(),
                resolver.Resolve<ISubscriber<SaveGameDTO>>());
            
            foreach (ISaveLoadConfigurator installer in GetComponents<ISaveLoadConfigurator>()) 
                installer.Configure(service);

            return service;
        }
    }
}